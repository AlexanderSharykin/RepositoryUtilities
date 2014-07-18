using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using RepositoryScanner.Activity;
using RepositoryScanner.Stats;

namespace RepositoryVisualClient
{
    public partial class ActivityChartForm : Form
    {
        SolidBrush cNone, cLow, cAverage, cHigh, cGreat;
        SolidBrush _headerBrush = new SolidBrush(Color.Wheat);
        private SolidBrush[] _brushes;
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
            cNone = new SolidBrush(Color.WhiteSmoke);
            cLow = new SolidBrush(Color.FromArgb(214, 230, 133));
            cAverage = new SolidBrush(Color.FromArgb(140, 198, 101));
            cHigh = new SolidBrush(Color.FromArgb(68, 163, 64));
            cGreat = new SolidBrush(Color.FromArgb(30, 104, 35));

            grdLegend.Columns.Add("cNone", "None");
            grdLegend.Columns.Add("cLow", "Low");
            grdLegend.Columns.Add("cAverage", "Avg");
            grdLegend.Columns.Add("cHigh", "High");
            grdLegend.Columns.Add("cGreat", "Great");
            grdLegend.RowCount = 1;
            grdLegend.Rows[0].Height = 25;
            _brushes = new[] {cNone, cLow, cAverage, cHigh, cGreat};
            for (int i = 0; i < _brushes.Length; i++)
            {
                grdLegend.Columns[i].Width = 60;
                grdLegend.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                grdLegend.Columns[i].DefaultCellStyle.BackColor = _headerBrush.Color;
                grdLegend[i, 0].Style.BackColor = grdLegend[i, 0].Style.SelectionBackColor = _brushes[i].Color;
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

                Color c = cNone.Color;
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
            txtSelectedStats.BackColor = cNone.Color;

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
            Color c = cNone.Color;
            switch (activity)
            {
                case ActivityLevel.Low:
                    c = cLow.Color;
                    break;
                case ActivityLevel.Average:
                    c = cAverage.Color;
                    break;
                case ActivityLevel.High:
                    c = cHigh.Color;
                    break;
                case ActivityLevel.Great:
                    c = cGreat.Color;
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
                    e.CellStyle.BackColor = cNone.Color;
                return;
            }
            e.CellStyle.BackColor =
            e.CellStyle.SelectionBackColor = _headerBrush.Color;

            e.PaintBackground(e.ClipBounds, false);
            
            DrawHeaderCell(e.Graphics, e.ClipBounds, e.CellBounds, e.CellStyle.Font, grdActivity.Columns[e.ColumnIndex].HeaderText);

            e.Handled = true;
        }

        private void DrawHeaderCell(Graphics g, RectangleF clipBounds, Rectangle cellBounds, Font font, string headerText)
        {
            var frm = new StringFormat();
            frm.FormatFlags = StringFormatFlags.DirectionVertical;
            frm.LineAlignment = StringAlignment.Center;
            frm.Alignment = StringAlignment.Near;
            
            System.Drawing.Drawing2D.GraphicsContainer gc;
            gc = g.BeginContainer(clipBounds, clipBounds, GraphicsUnit.Pixel);
            g.TranslateTransform(cellBounds.Right, cellBounds.Height);
            g.RotateTransform(180);
            g.DrawString(headerText, font, Brushes.Black, new RectangleF(0, -cellBounds.Y, cellBounds.Width, cellBounds.Height), frm);
            g.EndContainer(gc);            
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

        /// <summary>
        /// Creates a .png image with activity chart
        /// </summary>
        private void PrintClick(object sender, EventArgs e)
        {
            string filename;
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Image|*.png";
                sfd.FileName = string.Format("Activity of {0} on {1} - {2}", cboAuthors.SelectedItem, dtpStart.Value.ToShortDateString(), dtpEnd.Value.ToShortDateString());
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;
                filename = sfd.FileName;
            }
            var margin = new Padding(20);
            int cellHeight = grdActivity.Rows[0].Height;
            int cellWidth = grdActivity.Columns[0].Width;
            int tWidth = cellWidth*grdActivity.ColumnCount;
            int tHeight = cellHeight * grdActivity.RowCount;
            int headersHeight = grdActivity.ColumnHeadersHeight;
            int leftCornerY = margin.Top + headersHeight;

            Pen borderPen = Pens.Gray;
            using (Bitmap b = new Bitmap(margin.Horizontal + tWidth, margin.Vertical + tHeight + headersHeight))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(Color.White);

                    // draw headers
                    for (int col = 0; col < grdActivity.ColumnCount; col++)
                    {
                        int x = col*cellWidth + margin.Left;
                        g.FillRectangle(_headerBrush, x, margin.Top, cellWidth, headersHeight);
                    }

                    for (int col = 0; col < grdActivity.ColumnCount; col++)
                    {
                        int x = col*cellWidth + margin.Left;
                        var r = new Rectangle(x, margin.Top, cellWidth, headersHeight);

                        DrawHeaderCell(g, r, r, grdActivity.DefaultCellStyle.Font, grdActivity.Columns[col].HeaderText);
                    }

                    // draw cells
                    for (int row = 0; row < grdActivity.RowCount; row++)
                        for (int col = 0; col < grdActivity.ColumnCount; col++)
                        {
                            var c = grdActivity[col, row].Style.BackColor;
                            g.FillRectangle(new SolidBrush(c), margin.Left + col * cellWidth, leftCornerY + row * cellHeight, cellWidth, cellHeight);
                        }

                    // draw horizontal separators
                    for (int row = 0; row < grdActivity.RowCount; row++)
                    {
                        int y = row * cellHeight + leftCornerY;
                        g.DrawLine(borderPen, margin.Left, y, margin.Left + tWidth, y);
                    }

                    // draw vertical separators
                    for (int col = 0; col < grdActivity.ColumnCount; col++)
                    {
                        int x = col*cellWidth + margin.Left;
                        g.DrawLine(borderPen, x, margin.Top, x, leftCornerY + tHeight);
                    }

                    // draw outer borders
                    g.DrawRectangle(borderPen, margin.Left, margin.Top, tWidth, b.Height - margin.Vertical);
                }
                
                b.Save(filename, ImageFormat.Png);

                // start default program to show created image
                Process.Start(filename);
            }
        }  
    }
}
