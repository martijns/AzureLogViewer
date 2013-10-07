using Ms.Azure.Logging.Fetcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureLogViewerGui
{
    public enum OrderBy
    {
        New_to_Old,
        Old_to_New
    }

    public partial class MainForm : BaseForm
    {
        private static readonly string Version = "v" + AzureLogViewerGui.Version.GetVersion();

        public MainForm()
        {
            InitializeComponent();
            Text += " " + Version;
            DateTime from = DateTime.UtcNow;
            fromDate.Value = new DateTime(from.Year, from.Month, from.Day);
            toDate.Value = fromDate.Value.AddDays(1);
            fromDate.ValueChanged += (o, e) =>
            {
                if (toDate.Value < fromDate.Value)
                    toDate.Value = fromDate.Value.AddDays(1);
            };
            dataGridView1.CellDoubleClick += HandleCellDoubleClick;
            typeof(DataGridView).InvokeMember(
               "DoubleBuffered",
               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
               null,
               dataGridView1,
               new object[] { true });
            UpdateAccountSelection();

            // OrderBy vullen
            foreach (var enumname in Enum.GetNames(typeof(OrderBy)))
                orderByCombo.Items.Add(enumname);
            orderByCombo.SelectedIndex = 0;

            // Filter textbox
            filterTextBox.GotFocus += HandleFilterGotFocus;
            filterTextBox.LostFocus += HandleFilterLostFocus;
            filterTextBox.KeyUp += HandleFilterKeyup;
            HandleFilterLostFocus(this, EventArgs.Empty);

            // New version check
            AzureLogViewerGui.Version.CheckForUpdateAsync();
        }

        #region Filter textbox

        private static string FilterText = "type to filter";

        void HandleFilterGotFocus(object sender, EventArgs e)
        {
            if (filterTextBox.Text == FilterText)
            {
                filterTextBox.Text = "";
                //filterTextBox.Font = new Font(TextBox.DefaultFont, FontStyle.Regular);
                filterTextBox.ForeColor = TextBox.DefaultForeColor;
            }
        }

        void HandleFilterLostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filterTextBox.Text))
            {
                filterTextBox.Text = FilterText;
                //filterTextBox.Font = new Font(TextBox.DefaultFont, FontStyle.Regular);
                filterTextBox.ForeColor = Color.Gray;
            }
        }

        string GetFilterText()
        {
            if (!string.IsNullOrEmpty(filterTextBox.Text) && filterTextBox.Text != FilterText)
                return filterTextBox.Text;
            return null;
        }

        void HandleFilterKeyup(object sender, EventArgs e)
        {
            if (dataGridView1 == null || dataGridView1.RowCount == 0)
                return;

            // Get the selected filter text
            string filterText = GetFilterText();

            // Temporarily suspend datasource binding, preventing this exception:
            // "Row associated with the currency manager's position cannot be made invisible"
            CurrencyManager cm = (CurrencyManager)dataGridView1.BindingContext[dataGridView1.DataSource];
            cm.SuspendBinding();
            foreach (var row in dataGridView1.Rows.OfType<DataGridViewRow>())
            {
                // If there's no filtertext, make every row visible
                if (filterText == null)
                {
                    if (!row.Visible)
                        row.Visible = true;
                }
                // Otherwise we check each cell, and if we don't find a match, we hide the row
                else
                {
                    var found = false;
                    foreach (var cell in row.Cells.OfType<DataGridViewCell>())
                    {
                        if ((cell.Value + "").ToLower().Contains(filterText.ToLower()))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (row.Visible != found)
                        row.Visible = found;
                }
            }
            cm.ResumeBinding();
        }

        #endregion

        private void HandleCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (row == null)
                return;
            var cell = row.Cells.Cast<DataGridViewCell>().LastOrDefault();
            if (cell == null || cell.Value == null)
                return;
            MessageBox.Show(this, cell.Value.ToString().Replace("\\r", "").Replace("\\n", Environment.NewLine), "Logmessage");
        }

        private void HandleFetchButton(object sender, EventArgs e)
        {
            string accountName = GetSelectedAccountName();
            string accountKey = GetSelectedAccountKey();
            if (accountName == null)
            {
                MessageBox.Show(this, "Please select an account to fetch first...");
                return;
            }
            string table = GetSelectedTableName();
            if (table == null)
            {
                MessageBox.Show(this, "Please select a table to fetch first...");
                return;
            }
            DateTime from = fromDate.Value;
            DateTime to = toDate.Value;

            OrderBy order = (OrderBy)Enum.Parse(typeof(OrderBy), orderByCombo.SelectedItem + "");

            IList<WadTableEntity> entities = null;
            object datasource = null;
            PerformBG(this, () =>
            {
                // Find results for this period in time
                entities = new LogFetcher(accountName, accountKey).FetchLogs(table, from, to);
                switch (order)
                {
                    case OrderBy.New_to_Old:
                        entities = entities.OrderByDescending(i => i.EventTickCount).ThenByDescending(i => i.Timestamp).ToList();
                        break;
                    case OrderBy.Old_to_New:
                        entities = entities.OrderBy(i => i.EventTickCount).ThenBy(i => i.Timestamp).ToList();
                        break;
                }

                // If there are no entities at all, add a dummy "no results" entity
                if (entities == null || entities.Count == 0)
                {
                    entities = new List<WadTableEntity>();
                    var entity = new WadTableEntity {
                        PartitionKey = "No results found. Try extending the from/to period."
                    };
                    entity.Properties.Add("PartitionKey", entity.PartitionKey);
                    entities.Add(entity);
                }

                // Determine the available property names by checking each entity
                string[] propertyNames = entities.SelectMany(entity => entity.Properties).Select(p => p.Key).Distinct().ToArray();

                // If certain propertynames are available, we assume this is a Windows Azure Diagnostics table and we leave out some info
                if (propertyNames.Contains("EventTickCount") &&
                    propertyNames.Contains("RoleInstance") &&
                    propertyNames.Contains("Level") &&
                    propertyNames.Contains("Message"))
                {
                    datasource = (from i in entities select new { RoleInstance = i.RoleInstance, Message = i.Message }).ToArray();
                    return;
                }
                else
                {
                    // Make a custom table with the properties as columns and values in the correct rows
                    DataTable dt = new DataTable();
                    foreach (var propname in propertyNames)
                    {
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = propname;
                        dc.Caption = propname;
                        dc.DataType = typeof(string);
                        dt.Columns.Add(dc);
                    }

                    foreach (var entity in entities)
                    {
                        DataRow row = dt.NewRow();
                        foreach (var property in entity.Properties)
                        {
                            row[property.Key] = property.Value;
                        }
                        dt.Rows.Add(row);
                    }
                    datasource = dt;
                }
            },
            () =>
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.SuspendLayout();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.DataSource = datasource;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (column.Name != "Message")
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
                dataGridView1.ResumeLayout();

                if (GetFilterText() != null)
                    HandleFilterKeyup(this, EventArgs.Empty);
            });
        }

        private void HandleAccountAdd(object sender, EventArgs e)
        {
            var form = new AccountKeyForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (Configuration.Instance.Accounts.ContainsKey(form.AccountName))
                    Configuration.Instance.Accounts[form.AccountName] = form.AccountKey;
                else
                    Configuration.Instance.Accounts.Add(form.AccountName, form.AccountKey);
            }
            UpdateAccountSelection();
        }

        private void HandleAccountEdit(object sender, EventArgs e)
        {
            var form = new AccountKeyForm();
            form.AccountName = GetSelectedAccountName();
            form.AccountKey = GetSelectedAccountKey();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (Configuration.Instance.Accounts.ContainsKey(form.AccountName))
                    Configuration.Instance.Accounts[form.AccountName] = form.AccountKey;
                else
                    Configuration.Instance.Accounts.Add(form.AccountName, form.AccountKey);
            }
            UpdateAccountSelection();
        }

        private void HandleAccountRemove(object sender, EventArgs e)
        {
            string msg = "Are you sure you want to remove the key for account '..'?";
            if (MessageBox.Show(msg, "Remove account?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Configuration.Instance.Accounts.ContainsKey(GetSelectedAccountName()))
                    Configuration.Instance.Accounts.Remove(GetSelectedAccountName());
            }
            UpdateAccountSelection();
        }

        private void HandleAccountSelected(object sender, EventArgs e)
        {
            string accountname = GetSelectedAccountName();
            string accountkey = GetSelectedAccountKey();
            if (accountkey == null)
                return;
            IEnumerable<string> tableNames = null;

            PerformBG(this, () =>
            {
                tableNames = new LogFetcher(accountname, accountkey).FetchTables();
            },
            () =>
            {
                tableSelection.Items.Clear();
                tableSelection.Items.Add("-- select table --");
                foreach (var tablename in tableNames)
                    tableSelection.Items.Add(tablename);
                tableSelection.SelectedIndex = 0;
                UpdateAccountSelection();
            }, (ex) =>
            {
                if (MessageBox.Show(this, "Failed to retrieve tables for this storage account. Do you want to remove this account?", "Error", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (Configuration.Instance.Accounts.ContainsKey(GetSelectedAccountName()))
                        Configuration.Instance.Accounts.Remove(GetSelectedAccountName());
                }
                UpdateAccountSelection();
            });
        }

        private void UpdateAccountSelection()
        {
            // Save selection
            string selectedAccount = GetSelectedAccountName();

            // Fill the combobox again
            if (accountSelection.Items.Count == 0 ||
                accountSelection.Items.OfType<string>().Where(a => !a.StartsWith("--")).Except(Configuration.Instance.Accounts.Select(a => a.Key)).Any() ||
                Configuration.Instance.Accounts.Select(a => a.Key).Except(accountSelection.Items.OfType<string>().Where(a => !a.StartsWith("--"))).Any())
            {
                // 
                accountSelection.Items.Clear();
                accountSelection.Items.Add("-- select account --");
                foreach (var account in Configuration.Instance.Accounts.OrderBy(k => k.Key))
                {
                    accountSelection.Items.Add(account.Key);
                }

                // Reselect old selection (if it still exists)
                if (selectedAccount != null)
                {
                    foreach (var item in accountSelection.Items)
                    {
                        if (item.ToString() == selectedAccount)
                        {
                            accountSelection.SelectedItem = item;
                            break;
                        }
                    }
                }
                if (accountSelection.SelectedIndex == -1 || accountSelection.SelectedItem == null)
                {
                    accountSelection.SelectedIndex = 0;
                }

                // Clear the table combobox
                tableSelection.Items.Clear();
                tableSelection.Items.Add("-- select table --");
                tableSelection.SelectedIndex = 0;
            }


            // Enable/disable menu items
            editMenuItem.Enabled = accountSelection.SelectedIndex != 0;
            removeMenuItem.Enabled = accountSelection.SelectedIndex != 0;

            // Save configuration
            Configuration.Instance.Save();
        }

        private void HandleExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void HandleAboutClick(object sender, EventArgs e)
        {
            MessageBox.Show("AzureLogViewer " + Version + " by Martijn Stolk");
        }

        private string GetSelectedAccountName()
        {
            if (accountSelection.SelectedItem == null)
                return null;
            string name = accountSelection.SelectedItem.ToString();
            if (name.StartsWith("--") && name.EndsWith("--"))
                return null;
            return name;
        }

        private string GetSelectedAccountKey()
        {
            return Configuration.Instance.Accounts.Where(k => k.Key == GetSelectedAccountName()).Select(k => k.Value).FirstOrDefault();
        }

        private string GetSelectedTableName()
        {
            string name = tableSelection.SelectedItem.ToString();
            if (name.StartsWith("--") && name.EndsWith("--"))
                return null;
            return name;
        }

        private void PerformBG(BaseForm control, Action bgwork, Action fgwork, Action<Exception> customErrorHandler = null)
        {
            control.IsBusy = true;
            new Thread(() =>
            {
                Exception exception = null;
                try
                {
                    bgwork();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                control.Invoke((Action)(() =>
                {
                    if (exception != null)
                    {
                        if (customErrorHandler != null)
                            customErrorHandler(exception);
                        else
                            MessageBox.Show(control, "An error occurred executing a background task:\r\n\r\n" + exception.ToString());
                        IsBusy = false;
                        return;
                    }
                    try
                    {
                        fgwork();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(control, "An error occurred exucuting a foreground task:\r\n\r\n" + ex.ToString());
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }));
            }).Start();
        }
    }
}
