using Ms.Azure.Logging.Fetcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureLogViewerGui
{
    public partial class AccountKeyForm : BaseForm
    {
        public AccountKeyForm()
        {
            InitializeComponent();
        }

        public string AccountName
        {
            get { return accountName.Text; }
            set
            {
                accountName.Text = value;
                if (value != null && value.StartsWith("devstoreaccount") && !devStorageCheckbox.Checked)
                    devStorageCheckbox.Checked = true;
            }
        }

        public string AccountKey
        {
            get { return accountKey.Text; }
            set { accountKey.Text = value; }
        }

        private void HandleCancelClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void HandleSaveClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void HandleValidateClicked(object sender, EventArgs e)
        {
            IsBusy = true;
            validationStatus.Text = "Validating...";
            string accountName = AccountName;
            string accountKey = AccountKey;
            new Thread(() =>
            {
                string result = "";
                try
                {
                    if (new LogFetcher(accountName, accountKey).ValidateCredentials())
                    {
                        result = "success";
                    }
                    else
                    {
                        result = "invalid credentials";
                    }
                }
                catch (Exception ex)
                {
                    result = "error: " + ex.Message;
                }
                Invoke((Action)(() =>
                {
                    validationStatus.Text = result;
                    IsBusy = false;
                }));
            }).Start();
        }

        private void HandleDeveloperStorageCheckedChanged(object sender, EventArgs e)
        {
            if (devStorageCheckbox.Checked)
            {
                accountName.Text = "devstoreaccount";
                accountKey.Text = "";
                accountName.Enabled = false;
                accountKey.Enabled = false;
            }
            else
            {
                accountName.Enabled = true;
                accountKey.Enabled = true;
            }
        }
    }
}
