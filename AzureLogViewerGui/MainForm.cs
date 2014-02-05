using AzureLogViewerGui.Adapters;
using AzureLogViewerGui.Scrapers;
using Ms.Azure.Logging.Fetcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

        private string[] _loadedColumns = null;
        private string[][] _loadedRows = null;
        private string[][] _filteredRows = null;

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
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            dataGridView1.MultiSelect = true;
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

            // Extra opties
            useOptimizedQueriesForWADTablesToolStripMenuItem.Checked = Configuration.Instance.UseWADPerformanceOptimization;
            useOptimizedQueriesForWADTablesToolStripMenuItem.Click += (src, evt) =>
            {
                Configuration.Instance.UseWADPerformanceOptimization = !Configuration.Instance.UseWADPerformanceOptimization;
                useOptimizedQueriesForWADTablesToolStripMenuItem.Checked = Configuration.Instance.UseWADPerformanceOptimization;
                Configuration.Instance.Save();
            };

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
            if (filterText != null)
                filterText = filterText.ToLower();

            // Determine the filteredrows
            if (filterText == null)
            {
                _filteredRows = _loadedRows;
            }
            else
            {
                string[] searchterms = filterText.Split(new [] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                _filteredRows = (from row in _loadedRows
                                 //where FilterContains(row, searchterms)
                                 where searchterms.Where(s => !s.StartsWith("!")).All(s => row.Any(r => r.ToLower().Contains(s))) &&
                                       searchterms.Where(s => s.StartsWith("!")).All(s => row.All(r => !r.ToLower().Contains(s.TrimStart('!'))))
                                 //where searchterms.All(s => row.Any(r => s.StartsWith("!") ? !s.ToLower().Contains(s.TrimStart('!')) : s.ToLower().Contains(s)))
                                 //from item in row
                                 //where item != null && item.ToLower().Contains(filterText)
                                 select row).ToArray();
            }

            // Always show at least one record. When there are no results, add a dummy. If we don't do this, the
            // grid won't be usable anymore.
            if (_filteredRows.Length == 0)
                _filteredRows = new string[][] { Enumerable.Repeat("No result", _loadedColumns.Length).ToArray() };

            // Only update the rowcount if it changes, as this is a fairly expensive operation
            if (_filteredRows.Length != dataGridView1.RowCount)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.RowCount = _filteredRows.Length;
            }
        }

        bool FilterContains(string[] items, string[] searchterms)
        {
            // Keep a list of searchterms to find
            List<string> searchtermsRemaining = new List<string>(searchterms);

            // Loop over each item (i.e. column values)
            foreach (var item in items)
            {
                // Loop over each remaining searchterm
                foreach (var searchterm in searchtermsRemaining)
                {
                    // If a searchterm is found, remove it from the 'remaining' list
                    if (item.ToLower().Contains(searchterm))
                    {
                        searchtermsRemaining = new List<string>(searchtermsRemaining);
                        searchtermsRemaining.Remove(searchterm);
                    }
                }

                // If all searchterms are found, return early (for performance)
                if (searchtermsRemaining.Count == 0)
                    return true;
            }

            // All search terms found?
            return searchtermsRemaining.Count == 0;
        }

        #endregion

        private void HandleCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (row == null)
                return;
            //var cell = row.Cells.Cast<DataGridViewCell>().LastOrDefault();
            var cell = row.Cells[e.ColumnIndex];
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
            PerformBG(this, () =>
            {
                // Find results for this period in time
                entities = new LogFetcher(accountName, accountKey) { UseWADPerformanceOptimization = Configuration.Instance.UseWADPerformanceOptimization }.FetchLogs(table, from, to);
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
                        PartitionKey = "No results found. Try extending the from/to period. If you were expecting results, you could try disabling 'Use optimized queries for WAD tables'."
                    };
                    entity.Properties.Add("PartitionKey", entity.PartitionKey);
                    entities.Add(entity);
                }

                // Determine the available property names by checking each entity
                string[] propertyNames = entities.SelectMany(entity => entity.Properties).Select(p => p.Key).Distinct().ToArray();

                // If certain propertynames are available, we assume this is a Windows Azure Diagnostics table and we leave out some info
                if (propertyNames.Contains("DeploymentId") &&
                    propertyNames.Contains("EventTickCount") &&
                    propertyNames.Contains("RoleInstance") &&
                    propertyNames.Contains("Level") &&
                    propertyNames.Contains("Message"))
                {
                    _loadedColumns = new string[] { "DeploymentId", "RoleInstance", "Timestamp", "Message" };
                    _loadedRows = (from i in entities select new[] { i.DeploymentId, i.RoleInstance, i.Timestamp.ToString(), i.Message }).ToArray();
                    _filteredRows = _loadedRows;
                    return;
                }
                else
                {
                    _loadedColumns = (from propname in propertyNames select propname).ToArray();
                    _loadedRows = (from entity in entities
                                   select (from prop in entity.Properties select prop.Value).ToArray()).ToArray();
                    _filteredRows = _loadedRows;
                }
            },
            () =>
            {
                dataGridView1.ReadOnly = true;
                if (!dataGridView1.VirtualMode)
                {
                    dataGridView1.VirtualMode = true;
                    dataGridView1.CellValueNeeded += HandleCellValueNeeded;
                }
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToResizeColumns = true; // Resizen mag wel
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                foreach (var column in _loadedColumns)
                {
                    dataGridView1.Columns.Add(column, column);
                }

                // Vind van elke kolom de langste waarde
                string[] dummyvals = Enumerable.Repeat("", _loadedColumns.Length).ToArray();
                for (int i = 0; i < _loadedColumns.Length; i++)
                {
                    string longest = (from item in _loadedRows select item[i]).Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
                    //longest = new string('#', longest.Length);
                    dummyvals[i] = longest;
                }

                // Voeg een dummy record toe
                dataGridView1.VirtualMode = false;
                dataGridView1.Rows.Add(dummyvals);

                // Autosize alle kolommen
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // Breedtes van kolommen vastzetten
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    int width = column.Width + 15; // Compensate character width a little, so we don't get "..." on the slightest difference
                    if (width > 65536)
                        width = 65536;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    column.Width = width;
                }

                // Haal de autosize weer weg
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                // Rowcount goed zetten
                dataGridView1.Rows.Clear();
                dataGridView1.VirtualMode = true;
                dataGridView1.RowCount = _loadedRows.Length;

                // Als er een filter is, pas het filter toe...
                if (GetFilterText() != null)
                    HandleFilterKeyup(this, EventArgs.Empty);
            });
        }

        void HandleCellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            e.Value = "";

            if (_filteredRows == null || _loadedColumns == null)
                return;

            if (e.ColumnIndex < 0 || e.ColumnIndex >= _loadedColumns.Length)
                return;

            if (e.RowIndex < 0 || e.RowIndex >= _filteredRows.Length)
                return;

            e.Value = _filteredRows[e.RowIndex][e.ColumnIndex];
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
                tableNames = new LogFetcher(accountname, accountkey) { UseWADPerformanceOptimization = Configuration.Instance.UseWADPerformanceOptimization }.FetchTables();
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

        private void HandleChangelogClicked(object sender, EventArgs e)
        {
            AzureLogViewerGui.Version.DisplayChanges();
        }

        private void HandleAboutClick(object sender, EventArgs e)
        {
            AzureLogViewerGui.Version.DisplayAbout();
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

        private void HandleScrapeForStorageAccountsClicked(object sender, EventArgs e)
        {
            var form = new StorageAccountScraperForm();
            form.ShowDialog(this);
            UpdateAccountSelection();
        }

        private void HandleExportToAzureStorageExplorerClicked(object sender, EventArgs e)
        {
            var adapter = new AzureStorageExplorerAdapter();
            try
            {
                int count = adapter.Export();
                MessageBox.Show(this, "Successfully exported " + count + " (new) accounts to Azure Storage Explorer.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to export accounts: " + ex.ToString(), "Export failed!");
            }
        }

        private void HandleExportToCloudBerryExplorerClicked(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(this, "In order to export storage accounts to CloudBerry Explorer, we'll need to load a library for this application. Please select the indicated library in the CloudBerry Explorer installation directory. For example in: c:\\Program Files (x86)\\CloudBerryLab\\CloudBerry Explorer for Azure Blob Storage\\");

                var dialog = new OpenFileDialog();
                if (Directory.Exists(@"c:\Program Files (x86)\CloudBerryLab\CloudBerry Explorer for Azure Blob Storage\"))
                    dialog.InitialDirectory = @"c:\Program Files (x86)\CloudBerryLab\CloudBerry Explorer for Azure Blob Storage\";
                if (Directory.Exists(@"c:\Program Files\CloudBerryLab\CloudBerry Explorer for Azure Blob Storage\"))
                    dialog.InitialDirectory = @"c:\Program Files\CloudBerryLab\CloudBerry Explorer for Azure Blob Storage\";
                dialog.CheckFileExists = true;
                dialog.Filter = "CloudBerryLab Client DLL|CloudBerryLab.Client.dll|All DLLs|*.dll";
                dialog.Multiselect = false;
                dialog.ReadOnlyChecked = true;
                var result = dialog.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                var adapter = new CloudberryExplorerAdapter(dialog.FileName);
                int count = adapter.Export();
                MessageBox.Show(this, "Successfully exported " + count + " (new) accounts to CloudBerry Explorer.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to export accounts: " + ex.ToString(), "Export failed!");
            }
        }

        private void CopyAllToClip()
        {
             dataGridView1.SelectAll();
             DataObject dataObj = dataGridView1.GetClipboardContent();
             Clipboard.SetDataObject(dataObj, true);
        }

        private void CopySelectionToClip()
        {
            DataObject dataObj = dataGridView1.GetClipboardContent();
            Clipboard.SetDataObject(dataObj, true);
        }

        private void exportToCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToCsv();
        }

        private void copyAllToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyAllToClip();
        }

        private void copySelectionToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectionToClip();
        }

        private void exportSelectionToCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToCsv(true);
        }

        private void ExportToCsv(Boolean selected = false)
        {
            String fileName = OpenSaveAsDialog();
            if (fileName == null)
                return;
 
            var sb = new StringBuilder();
            var headers = dataGridView1.Columns.Cast<DataGridViewColumn>();
            sb.Append(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"")));
            sb.AppendLine();
            if (selected == true)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.Append(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"")));
                    sb.AppendLine();
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.Append(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"")));
                    sb.AppendLine();
                }
            }

            using (StreamWriter outfile = new StreamWriter(fileName))
            {
                outfile.Write(sb.ToString());
            }
        }

        private static String OpenSaveAsDialog()
        {
            SaveFileDialog fdSaveAs = new SaveFileDialog();
            fdSaveAs.InitialDirectory = "c:\\";
            fdSaveAs.Filter = "txt files (*.csv)|*.csv|All files (*.*)|*.*";
            fdSaveAs.FilterIndex = 2;
            fdSaveAs.RestoreDirectory = true;
            if (fdSaveAs.ShowDialog() == DialogResult.OK)
                return fdSaveAs.FileName;
            else
                return null;
        }

        private void HandleUseWADOptimizationClicked(object sender, EventArgs e)
        {

        }
    }
}
