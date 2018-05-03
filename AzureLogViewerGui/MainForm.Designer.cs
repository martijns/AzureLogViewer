namespace AzureLogViewerGui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ctxMenuPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxPresets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.last30MinutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastHourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.last2HoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.last4HoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.last8HoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wholeCurrentDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todayAndYesterdayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.past7DaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.accountSelection = new System.Windows.Forms.ComboBox();
            this.tableSelection = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.toDate = new System.Windows.Forms.DateTimePicker();
            this.llPresets = new System.Windows.Forms.LinkLabel();
            this.orderByCombo = new System.Windows.Forms.ComboBox();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.fetchButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.refreshInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectionToCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectionToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.scrapeForStorageAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeUnavliableStorageAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToAzureStorageExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToCloudBerryExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useOptimizedQueriesForWADTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertEventTickCountColumnToReadableFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPerfCountersAsChartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.useKarellPartitionKey = new System.Windows.Forms.ToolStripMenuItem();
            this.useKarellRowKey = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitFeedbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.ctxMenuPopup.SuspendLayout();
            this.ctxPresets.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.resultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshInterval)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxMenuPopup
            // 
            this.ctxMenuPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.ctxMenuPopup.Name = "ctxMenuPopup";
            this.ctxMenuPopup.Size = new System.Drawing.Size(103, 70);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // ctxPresets
            // 
            this.ctxPresets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.last30MinutesToolStripMenuItem,
            this.lastHourToolStripMenuItem,
            this.last2HoursToolStripMenuItem,
            this.last4HoursToolStripMenuItem,
            this.last8HoursToolStripMenuItem,
            this.wholeCurrentDayToolStripMenuItem,
            this.todayAndYesterdayToolStripMenuItem,
            this.past7DaysToolStripMenuItem});
            this.ctxPresets.Name = "ctxPresets";
            this.ctxPresets.Size = new System.Drawing.Size(183, 180);
            // 
            // last30MinutesToolStripMenuItem
            // 
            this.last30MinutesToolStripMenuItem.Name = "last30MinutesToolStripMenuItem";
            this.last30MinutesToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.last30MinutesToolStripMenuItem.Text = "Last 30 minutes";
            this.last30MinutesToolStripMenuItem.Click += new System.EventHandler(this.HandlePresetLast30Minutes);
            // 
            // lastHourToolStripMenuItem
            // 
            this.lastHourToolStripMenuItem.Name = "lastHourToolStripMenuItem";
            this.lastHourToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.lastHourToolStripMenuItem.Text = "Last hour";
            this.lastHourToolStripMenuItem.Click += new System.EventHandler(this.HandlePresetLastHour);
            // 
            // last2HoursToolStripMenuItem
            // 
            this.last2HoursToolStripMenuItem.Name = "last2HoursToolStripMenuItem";
            this.last2HoursToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.last2HoursToolStripMenuItem.Text = "Last 2 hours";
            this.last2HoursToolStripMenuItem.Click += new System.EventHandler(this.HandlePresetLast2Hours);
            // 
            // last4HoursToolStripMenuItem
            // 
            this.last4HoursToolStripMenuItem.Name = "last4HoursToolStripMenuItem";
            this.last4HoursToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.last4HoursToolStripMenuItem.Text = "Last 4 hours";
            this.last4HoursToolStripMenuItem.Click += new System.EventHandler(this.HandlePresetLast4Hours);
            // 
            // last8HoursToolStripMenuItem
            // 
            this.last8HoursToolStripMenuItem.Name = "last8HoursToolStripMenuItem";
            this.last8HoursToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.last8HoursToolStripMenuItem.Text = "Last 8 hours";
            this.last8HoursToolStripMenuItem.Click += new System.EventHandler(this.HandlePresetLast8Hours);
            // 
            // wholeCurrentDayToolStripMenuItem
            // 
            this.wholeCurrentDayToolStripMenuItem.Name = "wholeCurrentDayToolStripMenuItem";
            this.wholeCurrentDayToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.wholeCurrentDayToolStripMenuItem.Text = "Whole current day";
            this.wholeCurrentDayToolStripMenuItem.Click += new System.EventHandler(this.HandlePresetWholeCurrentDay);
            // 
            // todayAndYesterdayToolStripMenuItem
            // 
            this.todayAndYesterdayToolStripMenuItem.Name = "todayAndYesterdayToolStripMenuItem";
            this.todayAndYesterdayToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.todayAndYesterdayToolStripMenuItem.Text = "Today and yesterday";
            this.todayAndYesterdayToolStripMenuItem.Click += new System.EventHandler(this.HandlePresetTodayAndYesterday);
            // 
            // past7DaysToolStripMenuItem
            // 
            this.past7DaysToolStripMenuItem.Name = "past7DaysToolStripMenuItem";
            this.past7DaysToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.past7DaysToolStripMenuItem.Text = "Past 7 days";
            this.past7DaysToolStripMenuItem.Click += new System.EventHandler(this.HandlePresetPast7Days);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.resultPanel);
            this.mainPanel.Controls.Add(this.flowLayoutPanel1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 24);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1627, 371);
            this.mainPanel.TabIndex = 1;
            // 
            // resultPanel
            // 
            this.resultPanel.Controls.Add(this.dataGridView1);
            this.resultPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultPanel.Location = new System.Drawing.Point(0, 42);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(1627, 329);
            this.resultPanel.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(1627, 329);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.accountSelection);
            this.flowLayoutPanel1.Controls.Add(this.tableSelection);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.fromDate);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.toDate);
            this.flowLayoutPanel1.Controls.Add(this.llPresets);
            this.flowLayoutPanel1.Controls.Add(this.orderByCombo);
            this.flowLayoutPanel1.Controls.Add(this.filterTextBox);
            this.flowLayoutPanel1.Controls.Add(this.fetchButton);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.refreshInterval);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1627, 42);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // accountSelection
            // 
            this.accountSelection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.accountSelection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.accountSelection.FormattingEnabled = true;
            this.accountSelection.Location = new System.Drawing.Point(10, 10);
            this.accountSelection.Margin = new System.Windows.Forms.Padding(2);
            this.accountSelection.Name = "accountSelection";
            this.accountSelection.Size = new System.Drawing.Size(160, 21);
            this.accountSelection.TabIndex = 5;
            this.accountSelection.SelectedValueChanged += new System.EventHandler(this.HandleAccountSelected);
            // 
            // tableSelection
            // 
            this.tableSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tableSelection.FormattingEnabled = true;
            this.tableSelection.Location = new System.Drawing.Point(174, 10);
            this.tableSelection.Margin = new System.Windows.Forms.Padding(2);
            this.tableSelection.Name = "tableSelection";
            this.tableSelection.Size = new System.Drawing.Size(160, 21);
            this.tableSelection.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From:";
            // 
            // fromDate
            // 
            this.fromDate.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            this.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate.Location = new System.Drawing.Point(379, 10);
            this.fromDate.Margin = new System.Windows.Forms.Padding(2);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(173, 20);
            this.fromDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(558, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To:";
            // 
            // toDate
            // 
            this.toDate.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            this.toDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate.Location = new System.Drawing.Point(587, 10);
            this.toDate.Margin = new System.Windows.Forms.Padding(2);
            this.toDate.Name = "toDate";
            this.toDate.Size = new System.Drawing.Size(174, 20);
            this.toDate.TabIndex = 3;
            // 
            // llPresets
            // 
            this.llPresets.AutoSize = true;
            this.llPresets.Location = new System.Drawing.Point(766, 13);
            this.llPresets.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.llPresets.Name = "llPresets";
            this.llPresets.Size = new System.Drawing.Size(42, 13);
            this.llPresets.TabIndex = 9;
            this.llPresets.TabStop = true;
            this.llPresets.Text = "Presets";
            // 
            // orderByCombo
            // 
            this.orderByCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.orderByCombo.FormattingEnabled = true;
            this.orderByCombo.Location = new System.Drawing.Point(813, 10);
            this.orderByCombo.Margin = new System.Windows.Forms.Padding(2);
            this.orderByCombo.Name = "orderByCombo";
            this.orderByCombo.Size = new System.Drawing.Size(107, 21);
            this.orderByCombo.TabIndex = 7;
            // 
            // filterTextBox
            // 
            this.filterTextBox.Location = new System.Drawing.Point(925, 11);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(100, 20);
            this.filterTextBox.TabIndex = 8;
            // 
            // fetchButton
            // 
            this.fetchButton.Location = new System.Drawing.Point(1030, 10);
            this.fetchButton.Margin = new System.Windows.Forms.Padding(2);
            this.fetchButton.Name = "fetchButton";
            this.fetchButton.Size = new System.Drawing.Size(56, 19);
            this.fetchButton.TabIndex = 4;
            this.fetchButton.Text = "Fetch";
            this.fetchButton.UseVisualStyleBackColor = true;
            this.fetchButton.Click += new System.EventHandler(this.HandleFetchButton);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1092, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fetch last preset every: ";
            // 
            // refreshInterval
            // 
            this.refreshInterval.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.refreshInterval.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.refreshInterval.Location = new System.Drawing.Point(1219, 11);
            this.refreshInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.refreshInterval.Name = "refreshInterval";
            this.refreshInterval.Size = new System.Drawing.Size(58, 20);
            this.refreshInterval.TabIndex = 10;
            this.refreshInterval.ValueChanged += new System.EventHandler(this.refreshInterval_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1284, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "s";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.accountsToolStripMenuItem,
            this.extraToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1627, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToCsvToolStripMenuItem,
            this.exportSelectionToCsvToolStripMenuItem,
            this.copyAllToClipboardToolStripMenuItem,
            this.copySelectionToClipboardToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportToCsvToolStripMenuItem
            // 
            this.exportToCsvToolStripMenuItem.Name = "exportToCsvToolStripMenuItem";
            this.exportToCsvToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.exportToCsvToolStripMenuItem.Text = "Export All to csv";
            this.exportToCsvToolStripMenuItem.Click += new System.EventHandler(this.exportToCsvToolStripMenuItem_Click);
            // 
            // exportSelectionToCsvToolStripMenuItem
            // 
            this.exportSelectionToCsvToolStripMenuItem.Name = "exportSelectionToCsvToolStripMenuItem";
            this.exportSelectionToCsvToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.exportSelectionToCsvToolStripMenuItem.Text = "Export Selection to csv";
            this.exportSelectionToCsvToolStripMenuItem.Click += new System.EventHandler(this.exportSelectionToCsvToolStripMenuItem_Click);
            // 
            // copyAllToClipboardToolStripMenuItem
            // 
            this.copyAllToClipboardToolStripMenuItem.Name = "copyAllToClipboardToolStripMenuItem";
            this.copyAllToClipboardToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.copyAllToClipboardToolStripMenuItem.Text = "Copy All to Clipboard";
            this.copyAllToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyAllToClipboardToolStripMenuItem_Click);
            // 
            // copySelectionToClipboardToolStripMenuItem
            // 
            this.copySelectionToClipboardToolStripMenuItem.Name = "copySelectionToClipboardToolStripMenuItem";
            this.copySelectionToClipboardToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.copySelectionToClipboardToolStripMenuItem.Text = "Copy Selection to Clipboard";
            this.copySelectionToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copySelectionToClipboardToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.HandleExitClick);
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addMenuItem,
            this.editMenuItem,
            this.removeMenuItem,
            this.toolStripSeparator1,
            this.scrapeForStorageAccountsToolStripMenuItem,
            this.removeUnavliableStorageAccountsToolStripMenuItem,
            this.exportToAzureStorageExplorerToolStripMenuItem,
            this.exportToCloudBerryExplorerToolStripMenuItem});
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.accountsToolStripMenuItem.Text = "Accounts";
            // 
            // addMenuItem
            // 
            this.addMenuItem.Name = "addMenuItem";
            this.addMenuItem.Size = new System.Drawing.Size(276, 22);
            this.addMenuItem.Text = "Add...";
            this.addMenuItem.Click += new System.EventHandler(this.HandleAccountAdd);
            // 
            // editMenuItem
            // 
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(276, 22);
            this.editMenuItem.Text = "Edit...";
            this.editMenuItem.Click += new System.EventHandler(this.HandleAccountEdit);
            // 
            // removeMenuItem
            // 
            this.removeMenuItem.Name = "removeMenuItem";
            this.removeMenuItem.Size = new System.Drawing.Size(276, 22);
            this.removeMenuItem.Text = "Remove...";
            this.removeMenuItem.Click += new System.EventHandler(this.HandleAccountRemove);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(273, 6);
            // 
            // scrapeForStorageAccountsToolStripMenuItem
            // 
            this.scrapeForStorageAccountsToolStripMenuItem.Name = "scrapeForStorageAccountsToolStripMenuItem";
            this.scrapeForStorageAccountsToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.scrapeForStorageAccountsToolStripMenuItem.Text = "Scrape for storage accounts...";
            this.scrapeForStorageAccountsToolStripMenuItem.Click += new System.EventHandler(this.HandleScrapeForStorageAccountsClicked);
            // 
            // removeUnavliableStorageAccountsToolStripMenuItem
            // 
            this.removeUnavliableStorageAccountsToolStripMenuItem.Name = "removeUnavliableStorageAccountsToolStripMenuItem";
            this.removeUnavliableStorageAccountsToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.removeUnavliableStorageAccountsToolStripMenuItem.Text = "Remove unavliable storage accounts...";
            this.removeUnavliableStorageAccountsToolStripMenuItem.Click += new System.EventHandler(this.removeUnavliableStorageAccountsToolStripMenuItem_Click);
            // 
            // exportToAzureStorageExplorerToolStripMenuItem
            // 
            this.exportToAzureStorageExplorerToolStripMenuItem.Name = "exportToAzureStorageExplorerToolStripMenuItem";
            this.exportToAzureStorageExplorerToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.exportToAzureStorageExplorerToolStripMenuItem.Text = "Export to Azure Storage Explorer...";
            this.exportToAzureStorageExplorerToolStripMenuItem.Click += new System.EventHandler(this.HandleExportToAzureStorageExplorerClicked);
            // 
            // exportToCloudBerryExplorerToolStripMenuItem
            // 
            this.exportToCloudBerryExplorerToolStripMenuItem.Name = "exportToCloudBerryExplorerToolStripMenuItem";
            this.exportToCloudBerryExplorerToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.exportToCloudBerryExplorerToolStripMenuItem.Text = "Export to CloudBerry Explorer...";
            this.exportToCloudBerryExplorerToolStripMenuItem.Click += new System.EventHandler(this.HandleExportToCloudBerryExplorerClicked);
            // 
            // extraToolStripMenuItem
            // 
            this.extraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useOptimizedQueriesForWADTablesToolStripMenuItem,
            this.convertEventTickCountColumnToReadableFormatToolStripMenuItem,
            this.showPerfCountersAsChartMenuItem,
            this.toolStripSeparator2,
            this.useKarellPartitionKey,
            this.useKarellRowKey});
            this.extraToolStripMenuItem.Name = "extraToolStripMenuItem";
            this.extraToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.extraToolStripMenuItem.Text = "Extra";
            // 
            // useOptimizedQueriesForWADTablesToolStripMenuItem
            // 
            this.useOptimizedQueriesForWADTablesToolStripMenuItem.Name = "useOptimizedQueriesForWADTablesToolStripMenuItem";
            this.useOptimizedQueriesForWADTablesToolStripMenuItem.Size = new System.Drawing.Size(604, 22);
            this.useOptimizedQueriesForWADTablesToolStripMenuItem.Text = "Use optimized queries for WAD tables";
            // 
            // convertEventTickCountColumnToReadableFormatToolStripMenuItem
            // 
            this.convertEventTickCountColumnToReadableFormatToolStripMenuItem.Name = "convertEventTickCountColumnToReadableFormatToolStripMenuItem";
            this.convertEventTickCountColumnToReadableFormatToolStripMenuItem.Size = new System.Drawing.Size(604, 22);
            this.convertEventTickCountColumnToReadableFormatToolStripMenuItem.Text = "Convert EventTickCount column to readable format";
            // 
            // showPerfCountersAsChartMenuItem
            // 
            this.showPerfCountersAsChartMenuItem.Name = "showPerfCountersAsChartMenuItem";
            this.showPerfCountersAsChartMenuItem.Size = new System.Drawing.Size(604, 22);
            this.showPerfCountersAsChartMenuItem.Text = "Show PerformanceCounters in a chart";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(601, 6);
            // 
            // useKarellPartitionKey
            // 
            this.useKarellPartitionKey.CheckOnClick = true;
            this.useKarellPartitionKey.Name = "useKarellPartitionKey";
            this.useKarellPartitionKey.Size = new System.Drawing.Size(604, 22);
            this.useKarellPartitionKey.Text = "Use Karell Ste-Marie\'s \"log4net.Appender.Azure\" (PartitionKey only, query by hour" +
    ")";
            this.useKarellPartitionKey.Click += new System.EventHandler(this.HandleUseKarellPartitionKeyClicked);
            // 
            // useKarellRowKey
            // 
            this.useKarellRowKey.CheckOnClick = true;
            this.useKarellRowKey.Name = "useKarellRowKey";
            this.useKarellRowKey.Size = new System.Drawing.Size(604, 22);
            this.useKarellRowKey.Text = "Use Karell Ste-Marie\'s \"log4net.Appender.Azure\" (RowKey in reversed date format, " +
    "query by seconds)";
            this.useKarellRowKey.Click += new System.EventHandler(this.HandleUseKarellRowKeyClicked);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changelogToolStripMenuItem,
            this.submitFeedbackToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.changelogToolStripMenuItem.Text = "Changelog...";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.HandleChangelogClicked);
            // 
            // submitFeedbackToolStripMenuItem
            // 
            this.submitFeedbackToolStripMenuItem.Name = "submitFeedbackToolStripMenuItem";
            this.submitFeedbackToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.submitFeedbackToolStripMenuItem.Text = "Submit feedback...";
            this.submitFeedbackToolStripMenuItem.Click += new System.EventHandler(this.HandleSubmitFeedbackClicked);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.HandleAboutClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 373);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1627, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Tag = "KEEP_ENABLED";
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.Text = "Status...";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownButtonWidth = 0;
            this.toolStripSplitButton1.Image = global::AzureLogViewerGui.Properties.Resources.corssbtn;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(58, 20);
            this.toolStripSplitButton1.Tag = "KEEP_ENABLED";
            this.toolStripSplitButton1.Text = "Abort";
            this.toolStripSplitButton1.ToolTipText = "Abort";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.HandleAbortClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1627, 395);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "AzureLogViewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HandleFormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.ctxMenuPopup.ResumeLayout(false);
            this.ctxPresets.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.resultPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshInterval)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker fromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker toDate;
        private System.Windows.Forms.Button fetchButton;
        private System.Windows.Forms.ComboBox accountSelection;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeMenuItem;
        private System.Windows.Forms.ComboBox tableSelection;
        private System.Windows.Forms.ComboBox orderByCombo;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem scrapeForStorageAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToAzureStorageExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectionToCsvToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxMenuPopup;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectionToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToCloudBerryExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useOptimizedQueriesForWADTablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertEventTickCountColumnToReadableFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPerfCountersAsChartMenuItem;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.LinkLabel llPresets;
        private System.Windows.Forms.ContextMenuStrip ctxPresets;
        private System.Windows.Forms.ToolStripMenuItem last30MinutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastHourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem last2HoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem last4HoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem last8HoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wholeCurrentDayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem todayAndYesterdayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem past7DaysToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown refreshInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem submitFeedbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem useKarellPartitionKey;
        private System.Windows.Forms.ToolStripMenuItem useKarellRowKey;
        private System.Windows.Forms.ToolStripMenuItem removeUnavliableStorageAccountsToolStripMenuItem;
    }
}

