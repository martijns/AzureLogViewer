using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using System.Reflection;

namespace AzureLogViewerGui
{
    public partial class PerformanceCountersControl : UserControl
    {
        private PCData[] _counters;
        private string[] _lastsearchterms;

        public PerformanceCountersControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Name = "PerformanceCountersControl";
            this.TabIndex = 1;

            typeof(DataGridView).InvokeMember(
               "DoubleBuffered",
               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
               null,
               dataGridView1,
               new object[] { true });

            this.dataGridView1.CellValueChanged += HandleGridCellValueChanged;
            this.dataGridView1.CurrentCellDirtyStateChanged += HandleGridDirtyStateChanged;
            this.dataGridView1.DataError += HandleGridDataError;
        }

        internal void Initialize(string[][] rows, string[] searchterms)
        {
            if (rows == null || rows.Length == 0 || rows[0].Length < 6)
                throw new ArgumentException("Number of rows loaded is empty, or the number of columns is less than expected", "rows");

            _lastsearchterms = searchterms;
            _counters = GetPCData(rows);
            RestoreCounterPreferences();
            UpdateGrid();
            UpdateChart();
        }

        private PCData[] GetPCData(string[][] rows)
        {
            List<PCData> pcdataList = new List<PCData>();

            var counters = rows.Select(a => new { deployment = a[2], roleinstance = a[3], countername = a[4] }).Distinct();
            foreach (var counter in counters)
            {
                PCData data = new PCData { Deployment = counter.deployment, RoleInstance = counter.roleinstance, CounterName = counter.countername };
                data.Points = rows.Where(a => a[2] == data.Deployment && a[3] == data.RoleInstance && a[4] == data.CounterName).Select(a => new PCPoint { X = a[1], Y = ConvertToDouble(a[5]) }).OrderBy(p => p.X).ToArray();
                pcdataList.Add(data);
            }

            return pcdataList.OrderBy(p => p.CounterName).ThenBy(p => p.Deployment).ThenBy(p => p.RoleInstance).ToArray();
        }

        private void UpdateGrid()
        {
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = true; // Resizen mag wel
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Enabled", FalseValue = false, TrueValue = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Color", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Deployment", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "RoleInstance", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "CounterName", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Scale", ValueType = typeof(double) });
            foreach (var counter in _counters)
            {
                if (_lastsearchterms != null)
                {
                    if (!counter.ToString().ToLower().Contains(_lastsearchterms[0].ToLower()))
                        continue;
                }
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView1, counter.Enabled, "", counter.Deployment, counter.RoleInstance, counter.CounterName, counter.Scale);
                row.Cells[1].Style.BackColor = counter.Color;
                row.Tag = counter;
                dataGridView1.Rows.Add(row);
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        void HandleGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            var cell = row.Cells[e.ColumnIndex];
            var pcdata = (PCData)row.Tag;
            switch (cell.OwningColumn.HeaderText)
            {
                case "Enabled":
                    pcdata.Enabled = (bool)cell.Value;
                    UpdateChart();
                    break;
                case "Scale":
                    pcdata.Scale = (double)cell.Value;
                    UpdateChart();
                    break;
            }
        }

        void HandleGridDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
                return;

            switch (dataGridView1.CurrentCell.OwningColumn.HeaderText)
            {
                case "Enabled":
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    break;
            }
        }

        void HandleGridDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void UpdateChart()
        {
            UpdateCounterPreferences();
            chart1.Series.Clear();
            var enabledCounters = _counters.Where(c => c.Enabled == true).ToArray();
            foreach (var series in GetSeries(enabledCounters))
            {
                chart1.Series.Add(series);
            }
            foreach (var area in chart1.ChartAreas)
            {
                area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                area.AxisX.LabelStyle.IsStaggered = true;
                area.RecalculateAxesScale();
            }
        }

        private Series[] GetSeries(PCData[] counters)
        {
            List<Series> seriesList = new List<Series>();

            // Distinct list of counters
            foreach (var counter in counters)
            {
                Series series = new Series(counter.ToString());
                series.ChartType = SeriesChartType.Line;
                foreach (var point in counter.ScaledPoints)
                    series.Points.AddXY(point.X, point.Y);
                series.Color = counter.Color;
                series.BorderWidth = 3;
                seriesList.Add(series);
            }

            return seriesList.ToArray();
        }

        private double ConvertToDouble(string number)
        {
            double result;
            if (double.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;
            return 0.0;
        }

        #region Counter preferences

        private void UpdateCounterPreferences()
        {
            List<PerformanceCounterPreference> toberemoved = new List<PerformanceCounterPreference>();
            foreach (var counter in _counters)
            {
                var pref = Configuration.Instance.PerformanceCounterPreferences.Where(p => p.Name == counter.ToString()).FirstOrDefault();

                // Remove counters no longer enabled
                if (!counter.Enabled)
                {
                    if (pref != null)
                        toberemoved.Add(pref);
                    continue;
                }

                // Make a new preference for this counter as none could be found
                if (pref == null)
                {
                    pref = new PerformanceCounterPreference();
                    Configuration.Instance.PerformanceCounterPreferences.Add(pref);
                }
                pref.Name = counter.ToString();
                pref.Scale = counter.Scale;
                pref.LastSeen = DateTime.UtcNow;
            }
            Configuration.Instance.PerformanceCounterPreferences.RemoveAll(p => toberemoved.Contains(p));
            CleanOldCounters();
            Configuration.Instance.Save();
        }

        private void RestoreCounterPreferences()
        {
            foreach (var pref in Configuration.Instance.PerformanceCounterPreferences)
            {
                foreach (var counter in _counters.Where(c => c.ToString() == pref.Name))
                {
                    counter.Enabled = true;
                    counter.Scale = pref.Scale;
                    pref.LastSeen = DateTime.UtcNow;
                }
            }
            CleanOldCounters();
            Configuration.Instance.Save();
        }

        private void CleanOldCounters()
        {
            Configuration.Instance.PerformanceCounterPreferences.RemoveAll(p => DateTime.UtcNow - p.LastSeen > TimeSpan.FromDays(30));
        }

        #endregion

        internal void SetFilter(string[] searchterms)
        {
            _lastsearchterms = searchterms;
            UpdateGrid();
        }
    }

    public struct PCPoint
    {
        public string X { get; set; }
        public double Y { get; set; }
    }

    public class PCData
    {
        public PCData()
        {
            Scale = 1.0;
        }

        public bool Enabled { get; set; }
        public Color Color
        {
            get
            {
                Color c = Color.FromArgb(ToString().GetHashCode());
                c = Color.FromArgb(255, c.R % 200, c.G % 200, c.B % 200);
                return c;
            }
        }
        public string Deployment { get; set; }
        public string RoleInstance { get; set; }
        public string CounterName { get; set; }
        public PCPoint[] Points { get; set; }
        public double Scale { get; set; }
        public PCPoint[] ScaledPoints
        {
            get
            {
                return Scale == 1 ? Points : Points.Select(p => new PCPoint { X = p.X, Y = p.Y * Scale }).ToArray();
            }
        }
        public override string ToString()
        {
            return Deployment + " " + RoleInstance + " " + CounterName;
        }
    }
}
