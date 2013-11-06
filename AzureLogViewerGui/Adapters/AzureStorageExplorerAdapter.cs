using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureLogViewerGui.Adapters
{
    public class AzureStorageExplorerAdapter
    {
        public int Export()
        {
            // Read existing accounts in the current configuration file
            string[] existingAccounts = GetExistingAccounts();

            // Get a list of accounts to add that aren't already in AzureStorageExplorer
            var accountsToAdd = Configuration.Instance.Accounts.Where(a => !existingAccounts.Contains(a.Key.Trim().ToLower())).ToList();

            // Create or open the file for writing
            int count = 0;
            using (var stream = new StreamWriter(GetConfigurationFileLocation(), true))
            {
                foreach (var kvp in accountsToAdd)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("[Account]");
                    sb.AppendLine("Name=" + kvp.Key);
                    sb.AppendLine("ConnectionString=" + kvp.Value);
                    sb.AppendLine("UseHttps=1");
                    sb.AppendLine("BlobContainersUpgraded=1");
                    stream.Write(sb.ToString());
                    count++;
                }
            }

            // Done
            return count;
        }

        private string GetConfigurationFileLocation()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create), "AzureStorageExplorer", "AzureStorageExplorer.config");
        }

        private string[] GetExistingAccounts()
        {
            string configFile = GetConfigurationFileLocation();
            if (!File.Exists(configFile))
                return new string[0];

            List<string> accounts = new List<string>();
            string[] lines = File.ReadAllLines(configFile);
            bool accountSectionFound = false;
            foreach (string line in lines)
            {
                if (line.StartsWith("[Account]"))
                {
                    accountSectionFound = true;
                }
                else if (line.StartsWith("["))
                {
                    accountSectionFound = false;
                }
                else if (accountSectionFound && line.StartsWith("Name="))
                {
                    string[] kvp = line.Split('=');
                    accounts.Add(kvp[1].Trim().ToLower());
                }
            }
            return accounts.ToArray();
        }
    }
}
