using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AzureLogViewerGui.Adapters
{
    public class CloudberryExplorerAdapter
    {
        private string _cloudberryClientPath;

        public CloudberryExplorerAdapter(string cloudberryClientPath)
        {
            _cloudberryClientPath = cloudberryClientPath;
        }

        public int Export()
        {
            // Read existing accounts in the current configuration file
            string[] existingAccounts = GetExistingAccounts();

            // Get a list of accounts to add that aren't already in AzureStorageExplorer
            var accountsToAdd = Configuration.Instance.Accounts.Where(a => !existingAccounts.Contains(a.Key.Trim().ToLower())).ToList();

            // Check if file exists
            string config = GetConfigurationFileLocation();
            if (!File.Exists(config))
                throw new ApplicationException("Cannot export to CloudBerry Explorer as the configuration file was not found. Did you start CloudBerry at least once? Checked location: " + config);

            // Create or open the file for writing
            XDocument doc = XDocument.Load(config);
            int count = 0;
            foreach (var kvp in accountsToAdd)
            {
                var settingsEl = new XElement("Settings");
                settingsEl.SetAttributeValue(XName.Get("type", "http://www.w3.org/2001/XMLSchema-instance"), "AzureSettings");
                settingsEl.Add(new XElement(XName.Get("Name")) { Value = kvp.Key });
                settingsEl.Add(new XElement(XName.Get("ID")) { Value = Guid.NewGuid().ToString("D") });
                settingsEl.Add(new XElement(XName.Get("ContainerSettingsCollection")));
                if (kvp.Key.StartsWith("devstoreaccount"))
                    settingsEl.Add(new XElement(XName.Get("IsDevStoreAccount")) { Value = "true" });
                else
                    settingsEl.Add(new XElement(XName.Get("IsDevStoreAccount")) { Value = "false" });
                settingsEl.Add(new XElement(XName.Get("Account")) { Value = kvp.Key });
                string sharedkey = Encrypt(kvp.Value);
                settingsEl.Add(new XElement(XName.Get("SharedKey")) { Value = sharedkey });
                if (kvp.Key.StartsWith("devstoreaccount"))
                    settingsEl.Add(new XElement(XName.Get("UseSSL")) { Value = "false" });
                else
                    settingsEl.Add(new XElement(XName.Get("UseSSL")) { Value = "true" });
                settingsEl.Add(new XElement(XName.Get("RequestStyle")) { Value = "Path" });
                XElement collection = doc.Descendants(XName.Get("SettingsCollection")).FirstOrDefault();
                if (collection == null)
                    throw new ApplicationException("CloudBerry configuration does not have the SettingsCollection XML node, which was expected. It might be neccesary to add at least one storage account in CloudBerry itself before this is created. Config file: " + config);
                collection.Add(settingsEl);
                count++;
            }
            doc.Save(config);

            // Done
            return count;
        }

        private string Encrypt(string sharedkey)
        {
            try
            {
                Assembly aClient = Assembly.LoadFrom(@"C:\Program Files (x86)\CloudBerryLab\CloudBerry Explorer for Azure Blob Storage\CloudBerryLab.Client.dll");
                Type settingsType = aClient.GetType("CloudBerryLab.Client.Options.Settings");
                string encrypted = settingsType.InvokeMember("Encrypt", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, new object[] { sharedkey }) as string;
                return encrypted;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error loading an external CloudBerry Explorer library", ex);
            }
        }

        private string GetConfigurationFileLocation()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "CloudBerry Explorer for Azure Blob Storage", "settings.list");
        }

        private string[] GetExistingAccounts()
        {
            string configFile = GetConfigurationFileLocation();
            if (!File.Exists(configFile))
                return new string[0];

            List<string> accounts = new List<string>();
            string[] lines = File.ReadAllLines(configFile);
            foreach (string line in lines)
            {
                var match = Regex.Match(line, "<Account>(.*?)</Account>");
                if (match.Success)
                {
                    string accountname = match.Groups[1].Value;
                    accounts.Add(accountname.Trim().ToLower());
                }
            }
            return accounts.ToArray();
        }
    }
}
