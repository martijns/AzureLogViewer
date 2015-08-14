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
        private CloudTableClient _client;

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

        public IList<WadTableEntity> FetchLogs(string tableName, DateTime start, DateTime end)
        {
            var context = _client.GetTableServiceContext();
            context.MergeOption = MergeOption.NoTracking;
            context.ReadingEntity += HandleReadEntity;

            // Fetch all records. Internally uses continuation tokens to retrieve all pages.
            TableServiceQuery<WadTableEntity> query;
            if (UseWADPerformanceOptimization && tableName.StartsWith("WAD"))
            {
                // Use a better performing query for WAD tables, as we know how the partitionkey is built
                query = (from record in context.CreateQuery<WadTableEntity>(tableName)
                         where string.Compare(record.PartitionKey, start.Ticks.ToString("D19")) >= 0 && string.Compare(record.PartitionKey, end.Ticks.ToString("D19")) <= 0
                         where record.Timestamp >= start && record.Timestamp <= end
                         select record).AsTableServiceQuery<WadTableEntity>(context);
            }
            else
            {
                // Use timestamp querying on everything else
                query = (from record in context.CreateQuery<WadTableEntity>(tableName)
                         where record.Timestamp >= start && record.Timestamp <= end
                         select record).AsTableServiceQuery<WadTableEntity>(context);

            }
            var items = query.Execute().ToList();
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
