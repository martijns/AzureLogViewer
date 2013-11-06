using Ms.Azure.Logging.Fetcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureLogViewerGui.Scrapers
{
    public partial class StorageAccountScraperForm : Form
    {
        public StorageAccountScraperForm()
        {
            InitializeComponent();
            lblCurrentPath.Text = "";
            lblValidatingAccount.Text = "";
            lbValidated.Items.Clear();
            lbFailed.Items.Clear();
            Done();
            FormClosing += HandleFormClosing;
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            Configuration.Instance.Save();
        }

        private void HandleSelectDirClicked(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            btnSelectDir.Enabled = false;
            btnCancel.Enabled = true;

            StartScraping(dialog.SelectedPath);
        }

        private void HandleCancelClicked(object sender, EventArgs e)
        {
            if (_thread != null)
            {
                _thread.Abort();
                _thread = null;
            }
            Done();
        }

        private void UpdateCurrentPath(string newtext)
        {
            if (InvokeRequired)
            {
                Invoke(((Action<string>)UpdateCurrentPath), newtext);
                return;
            }
            lblCurrentPath.Text = newtext;
        }

        private void UpdateValidatingAccount(string newtext)
        {
            if (InvokeRequired)
            {
                Invoke(((Action<string>)UpdateValidatingAccount), newtext);
                return;
            }
            lblValidatingAccount.Text = newtext;
        }

        private void AddValidatedAccount(string name, string key)
        {
            if (InvokeRequired)
            {
                Invoke(((Action<string, string>)AddValidatedAccount), name, key);
                return;
            }
            lbValidated.Items.Add(name);
            Configuration.Instance.Accounts.Add(name, key);
        }

        private void AddFailedAccount(string name, string reason)
        {
            if (InvokeRequired)
            {
                Invoke(((Action<string, string>)AddFailedAccount), name, reason);
                return;
            }
            lbFailed.Items.Add(name + " (" + reason + ")");
        }

        private void Done()
        {
            if (InvokeRequired)
            {
                Invoke((Action)Done);
                return;
            }
            UpdateValidatingAccount("");
            UpdateCurrentPath("");
            btnSelectDir.Enabled = true;
            btnCancel.Enabled = false;
        }

        private Thread _thread;
        private void StartScraping(string dir)
        {
            Thread t = new Thread(() => {
                try
                {
                    RecursiveScrape(dir);
                }
                catch (Exception ex)
                {
                    AddFailedAccount("Error!", ex.ToString());
                }
                Done();
            });
            t.IsBackground = true;
            t.Start();
            _thread = t;
        }

        private void RecursiveScrape(string dir)
        {
            UpdateCurrentPath(dir);

            // Check *.cscfg
            string[] files = Directory.GetFiles(dir, "*.cscfg");
            foreach (string file in files)
            {
                string contents = File.ReadAllText(file);
                Regex regex = new Regex("DefaultEndpointsProtocol.*?AccountName=(.+?);.*?AccountKey=([a-zA-Z0-9/+=]+)", RegexOptions.Singleline);
                MatchCollection mc = regex.Matches(contents);
                foreach (Match m in mc)
                {
                    if (!m.Success)
                        continue;
                    TestKey(m.Groups[1].Value, m.Groups[2].Value);
                }
            }

            // Check stopbordje.storage*config
            files = Directory.GetFiles(dir, "stopbordje*.storage*config");
            foreach (string file in files)
            {
                string contents = File.ReadAllText(file);
                Regex regex = new Regex("<StorageName.*?>(.*?)</.*<StorageKey.*?>(.*?)</", RegexOptions.Singleline);
                MatchCollection mc = regex.Matches(contents);
                foreach (Match m in mc)
                {
                    if (!m.Success)
                        continue;
                    TestKey(m.Groups[1].Value, m.Groups[2].Value);
                }
            }

            // Check subdirs
            string[] subdirs = Directory.GetDirectories(dir);
            foreach (string subdir in subdirs)
            {
                RecursiveScrape(subdir);
            }
        }

        private void TestKey(string name, string key)
        {
            UpdateValidatingAccount(name);
            try
            {
                if (Configuration.Instance.Accounts.ContainsKey(name))
                {
                    AddFailedAccount(name, "Already added");
                }
                else
                {
                    if (new LogFetcher(name, key).ValidateCredentials())
                    {
                        AddValidatedAccount(name, key);
                    }
                    else
                    {
                        AddFailedAccount(name, "Incorrect credentials");
                    }
                }
            }
            catch (Exception ex)
            {
                AddFailedAccount(name, ex.ToString());
            }
        }
    }
}
