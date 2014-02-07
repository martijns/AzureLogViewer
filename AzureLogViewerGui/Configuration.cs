using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace AzureLogViewerGui
{
    [Serializable]
    public class Configuration
    {
        private static string configPath;

        private static Random random = new Random();

        private static Configuration _instance;
        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                    _instance = LoadConfiguration();
                return _instance;
            }
        }

        private Configuration()
        {
            Accounts = new SerializableDictionary<string, string>();
        }

        private static string GetConfigFilePath()
        {
            if (configPath != null)
                return configPath;

            string[] locations = new string[]
            {
                //Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "AzureLogViewer.xml"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create), "AzureLogViewer.xml"),
                Path.Combine(Path.GetTempPath(), "AzureLogViewer.xml")
            };
            
            // See if we already have a config file
            string path = locations.Where(l => File.Exists(l)).FirstOrDefault();
            if (path != null)
            {
                configPath = path;
                return configPath;
            }

            // See if we can use one of these paths
            path = locations.Where(l => HasWriteAccessToDir(Path.GetDirectoryName(l))).FirstOrDefault();
            if (path != null)
            {
                configPath = path;
                return configPath;
            }

            throw new ApplicationException("Cannot find a directory in which to save the configuration file. Attempted directories: " + string.Join(", ", locations));
        }

        private static bool HasWriteAccessToDir(string dirname)
        {
            if (string.IsNullOrEmpty(dirname) || !Directory.Exists(dirname))
                return false;

            try
            {
                // User probably doesn't have ACL rights on the folder, check by creating a temp file
                string randomfilename = Path.Combine(dirname, Path.GetRandomFileName());
                File.WriteAllText(randomfilename, "AzureLogViewer - Dummy file to check file system access");
                File.Delete(randomfilename);
                return true;
            }
            catch (Exception fex)
            {
                return false;
            }
            //try
            //{
            //    DirectorySecurity security = Directory.GetAccessControl(dirname);
            //    SecurityIdentifier users = new SecurityIdentifier(WellKnownSidType., null);
            //    foreach (AuthorizationRule rule in security.GetAccessRules(true, true, typeof(SecurityIdentifier)))
            //    {
            //        if (rule.IdentityReference == users)
            //        {
            //            FileSystemAccessRule rights = ((FileSystemAccessRule)rule);
            //            if (rights.AccessControlType == AccessControlType.Allow)
            //            {
            //                if (rights.FileSystemRights == (rights.FileSystemRights | FileSystemRights.Modify))
            //                    return true;
            //            }
            //        }
            //    }
            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    try
            //    {
            //        // User probably doesn't have ACL rights on the folder, check by creating a temp file
            //        string randomfilename = Path.Combine(dirname, Path.GetRandomFileName());
            //        File.WriteAllText(randomfilename, "SvclogViewer - Dummy file to check file system access");
            //        File.Delete(randomfilename);
            //        return true;
            //    }
            //    catch (Exception fex)
            //    {
            //        return false;
            //    }
            //}
        }

        private static Configuration LoadConfiguration()
        {
            FileInfo file = new FileInfo(GetConfigFilePath());
            if (file.Exists)
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Configuration));
                    using (FileStream fs = file.OpenRead())
                    {
                        object obj = ser.Deserialize(fs);
                        if (obj is Configuration)
                            return obj as Configuration;
                    }
                }
                catch (Exception ex)
                {
                    // loading filed, start anew
                }
            }
            return new Configuration();
        }

        private static void SaveConfiguration(Configuration config)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    FileInfo file = new FileInfo(GetConfigFilePath());
                    XmlSerializer ser = new XmlSerializer(typeof(Configuration));
                    using (FileStream fs = file.Create())
                    {
                        ser.Serialize(fs, config);
                    }
                }
                catch (IOException)
                {
                    // Can happen when the application is grouped in the taskbar and the whole group is closed
                    Thread.Sleep(random.Next(50, 150));
                    continue;
                }
                break;
            }
        }

        public void Save()
        {
            SaveConfiguration(this);
        }

        /// <summary>
        /// The list of accounts
        /// </summary>
        [XmlElement]
        public SerializableDictionary<string, string> Accounts { get; set; }

        /// <summary>
        /// As we want to default 'true' we use a negative setting, as a non-existing xml element will default to its default value (false). The public
        /// property is used to negate the private one, so that it is more convenient in its use.
        /// </summary>
        [XmlElement]
        protected bool DisableWADPerformanceOptimization { get; set; }
        public bool UseWADPerformanceOptimization
        {
            get { return !DisableWADPerformanceOptimization; }
            set { DisableWADPerformanceOptimization = !value; }
        }

        [XmlElement]
        protected bool DisableConvertEventTickCount { get; set; }
        public bool ConvertEventTickCount
        {
            get { return !DisableConvertEventTickCount; }
            set { DisableConvertEventTickCount = !value; }
        }
    }
}
