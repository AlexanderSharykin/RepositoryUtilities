using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using RepositoryScanner.Activity;
using RepositoryScanner.Stats;
using ViewModels;

namespace RepositoryVisualClient
{
    public partial class ActivityChartForm : Form, IVisualDialog
    {
        SolidBrush cNone, cLow, cAverage, cHigh, cGreat;
        SolidBrush _headerBrush = new SolidBrush(Color.Wheat);
        private SolidBrush[] _brushes;
        
        private PeriodHistoryVm _dataContext;

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

        public PeriodHistoryVm DataContext
        {
            get { return _dataContext; }
            set
            {
                if (_dataContext != null)
                    _dataContext.PropertyChanged -= DataContextChanged;

                _dataContext = value;

                if (_dataContext != null)
                    _dataContext.PropertyChanged += DataContextChanged;
            }
        }

        private void DataContextChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Weeks": DisplayHistory(); break;
                case "SelectedDaylyStats": DisplayDaylyActivity(); break;
            }
        }

        public bool? ShowDialog(object dataContext)
        {
            DataContext = (PeriodHistoryVm) dataContext;
            ShowActivity();
            return true;
        }

        public void ShowActivity()
        {
            var history = DataContext;
            for (int i = 0; i < history.Weeks.Count; i++)
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

            dtpStart.Value =
            dtpStart.MinDate =
            dtpEnd.MinDate = history.MinDate;

            dtpStart.MaxDate =
            dtpEnd.Value =
            dtpEnd.MaxDate = history.MaxDate;

            cboAuthors.DataSource = history.Authors.ToList();
            cboAuthors.SelectedIndex = 0;

            grdActivity.CurrentCellChanged += ActivityCurrentCellChanged;
                        
            ShowDialog();
        }

        private void AuthorsSelectedIndexChanged(object sender, EventArgs e)
        {
            DataContext.SelectedAuthor = cboAuthors.SelectedItem.ToString();
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
            var stats = DataContext.AuthorStats;
            
            grdBestStats.Rows.Clear();
            grdBestStats.Rows.Add(
                stats.LongestStreak,
                stats.LongestStreakFirstDate.ToShortDateString() + " - " +
                stats.LongestStreakLastDate.ToShortDateString(),
                stats.MaxCommitCount,
                stats.MaxCommitDate.ToShortDateString());

            for (int w = 0; w < DataContext.Weeks.Count; w++)
            {
                grdActivity.Columns[w].HeaderText = DataContext.Weeks[w].Title;
                for (int day = 0; day < 7; day++)
                {
                    var h = DataContext.Weeks[w][day];
                    grdActivity[w, day].Value = h;

                    Color c = cNone.Color;
                    if (h != null)
                        c = GetActivityColor(h.Activity);

                    grdActivity[w, day].Style.BackColor = c;
                }
            }

            grdActivity.Invalidate();
        }

        private void DisplayDaylyActivity()
        {
            txtLogMessages.Text = string.Empty;
            lblSelectedStats.Text = string.Empty;
            txtSelectedStats.BackColor = cNone.Color;

            var stats = DataContext.SelectedDaylyStats;
            
            // shows list of revisions on date
            if (stats != null)
            {
                txtSelectedStats.BackColor = GetActivityColor(stats.Activity);
                lblSelectedStats.Text = string.Format("{0} commits on {1}", stats.CommitCount, stats.Date.ToShortDateString());

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

        private void SelectedDayChanged(object sender, EventArgs e)
        {
            DaylyStats st = null;
            if (grdActivity.CurrentCell != null)
                st = grdActivity.CurrentCell.Value as DaylyStats;
            DataContext.SelectedDaylyStats = st;
        }

        private void PeriodStartValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpEnd.Value)
                dtpEnd.Value = dtpStart.Value;

            DataContext.MinDate = dtpStart.Value;
            grdActivity.Invalidate();
        }


        private void PeriodEndValueChanged(object sender, EventArgs e)
        {
            if (dtpEnd.Value < dtpStart.Value)
                dtpStart.Value = dtpEnd.Value;

            DataContext.MaxDate = dtpEnd.Value;
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
                var ds = (DaylyStats) grdActivity[e.ColumnIndex, e.RowIndex].Value;
                if (ds== null || ds.Date < dtpStart.Value || ds.Date > dtpEnd.Value)
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
