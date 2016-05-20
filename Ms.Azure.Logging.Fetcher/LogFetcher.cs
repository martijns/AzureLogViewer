using log4net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Ms.Azure.Logging.Fetcher
{
    public class LogFetcher
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(LogFetcher));

        private CloudTableClient _client;

        #region RetrievedPageEvent

        public event EventHandler<RetrievedPageEventArgs> RetrievedPage;

        public class RetrievedPageEventArgs : EventArgs
        {
            public int PageNr { get; set; }
        }

        public void OnRetrievedPage(int pagenr)
        {
            var handler = RetrievedPage;
            if (handler != null)
                handler(this, new RetrievedPageEventArgs { PageNr = pagenr });
        }

        #endregion


        public LogFetcher(string accountname, string accountkey)
        {
            if (accountname.StartsWith("devstoreaccount"))
            {
                _client = CloudStorageAccount.DevelopmentStorageAccount.CreateCloudTableClient();
            }
            else
            {
                var credentials = new StorageCredentials(accountname, accountkey);
                Uri uri = new Uri("https://" + accountname + ".table.core.windows.net");
                _client = new CloudTableClient(uri, credentials);
            }
        }

        public bool UseWADPerformanceOptimization { get; set; }

        public bool Interrupt { get; set; }

        public bool ValidateCredentials()
        {
            // Expect exception when credentials are incorrect
            var tables = _client.ListTables();
            return tables.Count() >= 0; // Make sure the (possibly internal) IQueryable evaluates
        }

        public IEnumerable<string> FetchTables()
        {
            return _client.ListTables().Select(t => t.Name).ToArray();
        }

        private void HandleBuildingRequest(object sender, BuildingRequestEventArgs e)
        {
            logger.Info($"Building request, uri: ${e.Method} ${e.RequestUri}");
        }

        public IList<WadTableEntity> FetchLogs(string tableName, DateTime start, DateTime end)
        {
            var table = _client.GetTableReference(tableName);
            var query = new TableQuery<WadTableEntity>();
            if (UseWADPerformanceOptimization && tableName.StartsWith("WAD"))
            {
                query = query.Where(TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThanOrEqual, start.Ticks.ToString("D19")),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.LessThanOrEqual, end.Ticks.ToString("D19"))));
            }
            else
            {
                query = query.Where(TableQuery.CombineFilters(
                    TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.GreaterThanOrEqual, start),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThanOrEqual, end)));
            }

            List<WadTableEntity> items = new List<WadTableEntity>();
            TableContinuationToken token = null;
            int count = 0;
            Interrupt = false;
            do
            {
                count++;
                OnRetrievedPage(count);
                var segment = table.ExecuteQuerySegmented(query, token);
                token = segment.ContinuationToken;
                items.AddRange(segment.Results);
            } while (token != null && !Interrupt);
            return items;
        }

        void HandleReadEntity(object sender, ReadingWritingEntityEventArgs e)
        {
            if (!(e.Entity is WadTableEntity))
                return;

            var properties = e.Data.Descendants(XName.Get("properties", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")).FirstOrDefault();
            if (properties == null)
                return;

            var entity = e.Entity as WadTableEntity;
            foreach (var property in properties.Elements())
            {
                entity.Properties.Add(property.Name.LocalName, property.Value);
            }
        }
    }
}
