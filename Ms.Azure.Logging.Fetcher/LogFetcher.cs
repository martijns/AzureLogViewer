using log4net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ms.Azure.Logging.Fetcher
{
    public class LogFetcher
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(LogFetcher));

        private CloudTableClient _client;
        private int count = 0;
        private object syncObject = new object();

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

        public bool UseKarellPartitionKey { get; set; }

        public bool UseKarellRowKey { get; set; }

        public bool Interrupt { get; set; }

        public bool ValidateCredentials()
        {
            // Throws exception if host does not exist
            Dns.GetHostEntry(_client.Credentials.AccountName + ".table.core.windows.net");

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
            if (!UseWADPerformanceOptimization)
                return FetchLogsInternal(tableName, start, end);

            var parts = RetrieveDateTimeParts(start, end, Environment.ProcessorCount);

            var allLogEntities = new List<WadTableEntity>();
            Parallel.ForEach(parts, part =>
            {
                var logs = FetchLogsInternal(tableName, part.Key, part.Value);

                lock (allLogEntities)
                    allLogEntities.AddRange(logs);
            });

            return allLogEntities;
        }

        private static Dictionary<DateTime, DateTime> RetrieveDateTimeParts(DateTime start, DateTime end, int divisor)
        {
            var span = end - start;
            var spanPart = new TimeSpan(span.Ticks / divisor);

            var parts = new Dictionary<DateTime, DateTime>();
            var index = start;
            while (index <= end)
            {
                var newEnd = index.Add(spanPart);
                parts.Add(index, newEnd);

                index = newEnd;
            }

            return parts;
        }

        public IList<WadTableEntity> FetchLogsInternal(string tableName, DateTime start, DateTime end)
        {
            var table = _client.GetTableReference(tableName);
            var query = new TableQuery<WadTableEntity>();
            if (UseKarellPartitionKey)
            {
                // Note that filter condition less/greater seems weird, but Karell uses a reverse format format and subtracts maxvalue
                DateTime startHour = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
                DateTime endHour = new DateTime(end.Year, end.Month, end.Day, end.Hour, 0, 0);
                if (end.Minute != 0 || end.Second != 0)
                    endHour = endHour.AddHours(1);
                query = query.Where(TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.LessThanOrEqual, ((DateTime.MaxValue - startHour).Ticks + 1).ToString("D19")),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThanOrEqual, ((DateTime.MaxValue - endHour).Ticks + 1).ToString("D19"))));
            }
            else if (UseKarellRowKey)
            {
                // Note that filter condition less/greater seems weird, but Karell uses a reverse format format and subtracts maxvalue
                DateTime startHour = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
                DateTime endHour = new DateTime(end.Year, end.Month, end.Day, end.Hour, 0, 0);
                if (end.Minute != 0 || end.Second != 0)
                    endHour = endHour.AddHours(1);
                start = start.AddHours(1);
                end = end.AddHours(1);
                query = query.Where(TableQuery.CombineFilters(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.LessThanOrEqual, ((DateTime.MaxValue - startHour).Ticks + 1).ToString("D19")),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThanOrEqual, ((DateTime.MaxValue - endHour).Ticks + 1).ToString("D19"))
                    ),
                    TableOperators.And,
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThanOrEqual, (DateTime.MaxValue - start).Ticks.ToString("D19")),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, (DateTime.MaxValue - end).Ticks.ToString("D19"))
                        )
                    )
                );
            }
            else if (UseWADPerformanceOptimization && tableName.StartsWith("WAD"))
            {
                query = query.Where(TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThanOrEqual, start.Ticks.ToString("D19")),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.LessThan, end.Ticks.ToString("D19"))));
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

            do
            {
                lock (syncObject)
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
