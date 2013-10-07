using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureLogViewerGui
{
    public partial class AccountKeyControl : UserControl
    {
        public AccountKeyControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> PlusClicked;

        public string AccountName
        {
            get { return accountNameText.Text; }
            set { accountNameText.Text = value; }
        }

        public string AccountKey
        {
            get { return accountKeyText.Text; }
            set { accountKeyText.Text = value; }
        }

        private void HandleValidateClicked(object sender, EventArgs e)
        {
            if (true)
            {
                validateButton.BackColor = Color.FromArgb(192, 255, 192); // green
            }
            else
            {
                validateButton.BackColor = Color.FromArgb(255, 192, 192); // red
            }
        }

        private void HandlePlusClicked(object sender, EventArgs e)
        {
            OnPlusClicked();
        }

        protected void OnPlusClicked()
        {
            var handler = PlusClicked;
            if (PlusClicked != null)
                PlusClicked(this, EventArgs.Empty);
        }
    }
}
