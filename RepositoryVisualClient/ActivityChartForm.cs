using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RepositoryScanner.Activity;
using RepositoryScanner.Stats;

namespace RepositoryVisualClient
{
    public partial class ActivityChartForm : Form
    {
        Color cNone, cLow, cAverage, cHigh, cGreat;
        private static string[] _months = {
                                              "Jan", "Feb", "Mar",
                                              "Apr", "May", "Jun",
                                              "Jul", "Aug", "Sep",
                                              "Oct", "Nov", "Dec",
                                          };

        private PeriodHistory _history;
        public ActivityChartForm()
        {
            InitializeComponent();

            // defines acivity colors and displays them in color legend grid
            cNone = Color.WhiteSmoke;
            cLow = Color.FromArgb(214, 230, 133);
            cAverage = Color.FromArgb(140, 198, 101);
            cHigh = Color.FromArgb(68, 163, 64);
            cGreat = Color.FromArgb(30, 104, 35);

            grdLegend.Columns.Add("cNone", "None");
            grdLegend.Columns.Add("cLow", "Low");
            grdLegend.Columns.Add("cAverage", "Avg");
            grdLegend.Columns.Add("cHigh", "High");
            grdLegend.Columns.Add("cGreat", "Great");
            grdLegend.RowCount = 1;
            grdLegend.Rows[0].Height = 25;
            var colors = new[] {cNone, cLow, cAverage, cHigh, cGreat};
            for (int i = 0; i < colors.Length; i++)
            {
                grdLegend.Columns[i].Width = 60;
                grdLegend.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                grdLegend.Columns[i].DefaultCellStyle.BackColor = Color.Wheat;
                grdLegend[i, 0].Style.BackColor = grdLegend[i, 0].Style.SelectionBackColor = colors[i];
            }
        }
        
        public void ShowActivity(PeriodHistory history)
        {
            _history = history;

            for (int i = 0; i < _history.Weeks; i++)
            {
                var column = new DataGridViewTextBoxColumn();
                column.HeaderText = "";
                grdActivity.Columns.Add(column);
                column.Width = 20;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            grdActivity.RowCount = 7;

            for (int i = 0; i < 7; i++)
            {
                grdActivity.Rows[i].Height = 20;
            }

            dtpEnd.MinDate = history.PeriodStart;
            dtpStart.MinDate = history.PeriodStart;
            dtpEnd.MaxDate = history.PeriodEnd;
            dtpStart.MaxDate = history.PeriodEnd;
            dtpStart.Value = history.PeriodStart;
            dtpEnd.Value = history.PeriodEnd;

            cboAuthors.DataSource = history.Authors.ToList();
            cboAuthors.SelectedIndex = 0;

            grdActivity.CurrentCellChanged += ActivityCurrentCellChanged;
                        
            ShowDialog();
        }

        private void AuthorsSelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayHistory();
            DisplayDaylyActivity();
        }

        private void ActivityCurrentCellChanged(object sender, EventArgs e)
        {
            DisplayDaylyActivity();
        }

        /// <summary>
        /// Fills the activity grid with selected author stats
        /// </summary>
        private void DisplayHistory()
        {
            var author = cboAuthors.SelectedItem.ToString();
            var stats = _history[author];
            var date = _history.PeriodStart;

            grdBestStats.Rows.Clear();
            grdBestStats.Rows.Add(
                stats.LongestStreak,
                stats.LongestStreakFirstDate.ToShortDateString() + " - " +
                stats.LongestStreakLastDate.ToShortDateString(),
                stats.MaxCommitCount,
                stats.MaxCommitDate.ToShortDateString());

            int w = 0;
            DateTime weekStart = date;
            while (date <= _history.PeriodEnd)
            {
                var daylyStats = stats[date];

                int day = (int) date.DayOfWeek;
                if (day == 0 && date > _history.PeriodStart)
                    w++;
                grdActivity[w, day].Value = date;

                #region Sets columns header text

                if (day == 0)
                    weekStart = date;

                string headerText;
                if (day == 6 || date == _history.PeriodEnd)
                {
                    int month = weekStart.Month - 1;
                    if (date.Month != weekStart.Month)
                    {
                        if (date.Year != weekStart.Year)
                            headerText = _months[month] + " " + weekStart.Year + "  " + _months[date.Month - 1] +
                                         " " +
                                         date.Year;

                        else headerText = _months[month] + "  " + _months[date.Month - 1];
                    }
                    else headerText = _months[month];
                    grdActivity.Columns[w].HeaderText = headerText;
                }


                #endregion

                Color c = cNone;
                if (daylyStats != null)
                    c = GetActivityColor(daylyStats.Activity);

                grdActivity[w, day].Style.BackColor = c;

                date = date.AddDays(1);
            }
            grdActivity.Invalidate();
        }

        private void DisplayDaylyActivity()
        {
            DateTime? date = null;
            var cell = grdActivity.CurrentCell;
            if (cell != null)
            {
                if (cell.RowIndex >= 0 && cell.ColumnIndex >= 0)
                    date = (DateTime?) cell.Value;
            }
            txtLogMessages.Text = string.Empty;
            lblSelectedStats.Text = string.Empty;
            txtSelectedStats.BackColor = cNone;

            if (date == null || date.Value < dtpStart.Value || date.Value > dtpEnd.Value)
                return;                     

            var author = cboAuthors.SelectedItem.ToString();
            var stats = _history[author][date.Value];
            
            // shows list of revisions on date
            if (stats != null)
            {
                txtSelectedStats.BackColor = GetActivityColor(stats.Activity);
                lblSelectedStats.Text = string.Format("{0} commits on {1}", stats.CommitCount, date.Value.ToShortDateString());

                foreach (var commit in stats.Commits)
                {
                    txtLogMessages.AppendText(string.Format("{0}: [{1}] {2}", commit.Time.ToShortTimeString(), commit.Number, commit.Comment));
                    txtLogMessages.AppendText(Environment.NewLine);
                }
            }
        }

        private Color GetActivityColor(ActivityLevel activity)
        {
            Color c = cNone;
            switch (activity)
            {
                case ActivityLevel.Low:
                    c = cLow;
                    break;
                case ActivityLevel.Average:
                    c = cAverage;
                    break;
                case ActivityLevel.High:
                    c = cHigh;
                    break;
                case ActivityLevel.Great:
                    c = cGreat;
                    break;
            }
            return c;
        }

        private void PeriodStartValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpEnd.Value)
                dtpEnd.Value = dtpStart.Value;
            else
            grdActivity.Invalidate();
        }


        private void PeriodEndValueChanged(object sender, EventArgs e)
        {
            if (dtpEnd.Value < dtpStart.Value)
                dtpStart.Value = dtpEnd.Value;
            else
                grdActivity.Invalidate();
        }  

        /// <summary>
        /// Draw headers vertically. Sets cell backcolor.
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/5782674/net-winforms-vertical-text-in-datagridview"/>
        private void ActivityGridCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;
            if (e.RowIndex >= 0)
            {
                var date = (DateTime?) grdActivity[e.ColumnIndex, e.RowIndex].Value;
                if (!date.HasValue || date < dtpStart.Value || date > dtpEnd.Value)
                    e.CellStyle.BackColor = cNone;
                return;
            }
            e.CellStyle.BackColor = Color.Wheat;
            e.CellStyle.SelectionBackColor = Color.Wheat;

            var frm = new StringFormat();
            frm.FormatFlags = StringFormatFlags.DirectionVertical;
            frm.LineAlignment = StringAlignment.Center;
            frm.Alignment = StringAlignment.Near;

            e.PaintBackground(e.ClipBounds, false);
            System.Drawing.Drawing2D.GraphicsContainer gc;
            gc = e.Graphics.BeginContainer(e.Graphics.ClipBounds, e.Graphics.ClipBounds, GraphicsUnit.Pixel);
            e.Graphics.TranslateTransform(e.CellBounds.Right, e.CellBounds.Height);
            e.Graphics.RotateTransform(180);
            e.Graphics.DrawString(grdActivity.Columns[e.ColumnIndex].HeaderText, e.CellStyle.Font, Brushes.Black,
                                  new Rectangle(0, 0, e.CellBounds.Width, e.CellBounds.Height), frm);
            e.Graphics.EndContainer(gc);
            e.Handled = true;
        }

        /// <summary>
        /// Hides dates in grid cells
        /// </summary>
        private void ActivityGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            e.Value = string.Empty;
            e.FormattingApplied = true;
        }  
    }
}
