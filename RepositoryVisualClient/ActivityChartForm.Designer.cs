namespace RepositoryVisualClient
{
    partial class ActivityChartForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdActivity = new System.Windows.Forms.DataGridView();
            this.cboAuthors = new System.Windows.Forms.ComboBox();
            this.lblAuthors = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.txtLogMessages = new System.Windows.Forms.TextBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lblLegend = new System.Windows.Forms.Label();
            this.grdLegend = new System.Windows.Forms.DataGridView();
            this.grdBestStats = new System.Windows.Forms.DataGridView();
            this.lblBestStats = new System.Windows.Forms.Label();
            this.txtSelectedStats = new System.Windows.Forms.TextBox();
            this.lblSelectedStats = new System.Windows.Forms.Label();
            this.cStreak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStreakDates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaxCommit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaxCommitDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLegend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBestStats)).BeginInit();
            this.SuspendLayout();
            // 
            // grdActivity
            // 
            this.grdActivity.AllowUserToAddRows = false;
            this.grdActivity.AllowUserToDeleteRows = false;
            this.grdActivity.AllowUserToResizeColumns = false;
            this.grdActivity.AllowUserToResizeRows = false;
            this.grdActivity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdActivity.ColumnHeadersHeight = 120;
            this.grdActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdActivity.Location = new System.Drawing.Point(12, 37);
            this.grdActivity.MultiSelect = false;
            this.grdActivity.Name = "grdActivity";
            this.grdActivity.ReadOnly = true;
            this.grdActivity.RowHeadersVisible = false;
            this.grdActivity.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.grdActivity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdActivity.Size = new System.Drawing.Size(860, 280);
            this.grdActivity.TabIndex = 0;
            this.grdActivity.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ActivityGridCellFormatting);
            this.grdActivity.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.ActivityGridCellPainting);
            // 
            // cboAuthors
            // 
            this.cboAuthors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cboAuthors.FormattingEnabled = true;
            this.cboAuthors.Location = new System.Drawing.Point(56, 7);
            this.cboAuthors.Name = "cboAuthors";
            this.cboAuthors.Size = new System.Drawing.Size(144, 24);
            this.cboAuthors.TabIndex = 1;
            this.cboAuthors.SelectedIndexChanged += new System.EventHandler(this.AuthorsSelectedIndexChanged);
            // 
            // lblAuthors
            // 
            this.lblAuthors.AutoSize = true;
            this.lblAuthors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAuthors.Location = new System.Drawing.Point(12, 9);
            this.lblAuthors.Name = "lblAuthors";
            this.lblAuthors.Size = new System.Drawing.Size(46, 16);
            this.lblAuthors.TabIndex = 2;
            this.lblAuthors.Text = "Author";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(392, 7);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(100, 20);
            this.dtpEnd.TabIndex = 4;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.PeriodEndValueChanged);
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(286, 7);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(100, 20);
            this.dtpStart.TabIndex = 3;
            this.dtpStart.ValueChanged += new System.EventHandler(this.PeriodStartValueChanged);
            // 
            // txtLogMessages
            // 
            this.txtLogMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtLogMessages.Location = new System.Drawing.Point(12, 426);
            this.txtLogMessages.Multiline = true;
            this.txtLogMessages.Name = "txtLogMessages";
            this.txtLogMessages.ReadOnly = true;
            this.txtLogMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogMessages.Size = new System.Drawing.Size(859, 294);
            this.txtLogMessages.TabIndex = 5;
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPeriod.Location = new System.Drawing.Point(232, 9);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(48, 16);
            this.lblPeriod.TabIndex = 7;
            this.lblPeriod.Text = "Period";
            // 
            // lblLegend
            // 
            this.lblLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLegend.AutoSize = true;
            this.lblLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLegend.Location = new System.Drawing.Point(773, 323);
            this.lblLegend.Name = "lblLegend";
            this.lblLegend.Size = new System.Drawing.Size(99, 16);
            this.lblLegend.TabIndex = 8;
            this.lblLegend.Text = "Activity Legend";
            // 
            // grdLegend
            // 
            this.grdLegend.AllowUserToAddRows = false;
            this.grdLegend.AllowUserToDeleteRows = false;
            this.grdLegend.AllowUserToResizeColumns = false;
            this.grdLegend.AllowUserToResizeRows = false;
            this.grdLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Wheat;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdLegend.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grdLegend.ColumnHeadersHeight = 25;
            this.grdLegend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdLegend.Location = new System.Drawing.Point(572, 342);
            this.grdLegend.Name = "grdLegend";
            this.grdLegend.ReadOnly = true;
            this.grdLegend.RowHeadersVisible = false;
            this.grdLegend.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grdLegend.Size = new System.Drawing.Size(300, 50);
            this.grdLegend.TabIndex = 9;
            // 
            // grdBestStats
            // 
            this.grdBestStats.AllowUserToAddRows = false;
            this.grdBestStats.AllowUserToDeleteRows = false;
            this.grdBestStats.AllowUserToResizeColumns = false;
            this.grdBestStats.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Wheat;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdBestStats.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grdBestStats.ColumnHeadersHeight = 25;
            this.grdBestStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdBestStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cStreak,
            this.cStreakDates,
            this.cMaxCommit,
            this.cMaxCommitDate});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdBestStats.DefaultCellStyle = dataGridViewCellStyle9;
            this.grdBestStats.Location = new System.Drawing.Point(12, 342);
            this.grdBestStats.Name = "grdBestStats";
            this.grdBestStats.ReadOnly = true;
            this.grdBestStats.RowHeadersVisible = false;
            this.grdBestStats.RowTemplate.Height = 25;
            this.grdBestStats.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grdBestStats.Size = new System.Drawing.Size(480, 50);
            this.grdBestStats.TabIndex = 11;
            // 
            // lblBestStats
            // 
            this.lblBestStats.AutoSize = true;
            this.lblBestStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBestStats.Location = new System.Drawing.Point(9, 322);
            this.lblBestStats.Name = "lblBestStats";
            this.lblBestStats.Size = new System.Drawing.Size(68, 16);
            this.lblBestStats.TabIndex = 12;
            this.lblBestStats.Text = "Best Stats";
            // 
            // txtSelectedStats
            // 
            this.txtSelectedStats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSelectedStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSelectedStats.Location = new System.Drawing.Point(12, 398);
            this.txtSelectedStats.Name = "txtSelectedStats";
            this.txtSelectedStats.ReadOnly = true;
            this.txtSelectedStats.Size = new System.Drawing.Size(68, 22);
            this.txtSelectedStats.TabIndex = 13;
            // 
            // lblSelectedStats
            // 
            this.lblSelectedStats.AutoSize = true;
            this.lblSelectedStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSelectedStats.Location = new System.Drawing.Point(86, 401);
            this.lblSelectedStats.Name = "lblSelectedStats";
            this.lblSelectedStats.Size = new System.Drawing.Size(153, 16);
            this.lblSelectedStats.TabIndex = 14;
            this.lblSelectedStats.Text = "0 commits on 2014-30-06";
            // 
            // cStreak
            // 
            this.cStreak.HeaderText = "Longest Streak";
            this.cStreak.Name = "cStreak";
            this.cStreak.ReadOnly = true;
            this.cStreak.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cStreak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cStreakDates
            // 
            this.cStreakDates.HeaderText = "Dates";
            this.cStreakDates.Name = "cStreakDates";
            this.cStreakDates.ReadOnly = true;
            this.cStreakDates.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cStreakDates.Width = 140;
            // 
            // cMaxCommit
            // 
            this.cMaxCommit.HeaderText = "Max Commit";
            this.cMaxCommit.Name = "cMaxCommit";
            this.cMaxCommit.ReadOnly = true;
            this.cMaxCommit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cMaxCommitDate
            // 
            this.cMaxCommitDate.HeaderText = "Date";
            this.cMaxCommitDate.Name = "cMaxCommitDate";
            this.cMaxCommitDate.ReadOnly = true;
            this.cMaxCommitDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cMaxCommitDate.Width = 140;
            // 
            // GitActivityChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 740);
            this.Controls.Add(this.lblSelectedStats);
            this.Controls.Add(this.txtSelectedStats);
            this.Controls.Add(this.lblBestStats);
            this.Controls.Add(this.grdBestStats);
            this.Controls.Add(this.grdLegend);
            this.Controls.Add(this.lblLegend);
            this.Controls.Add(this.lblPeriod);
            this.Controls.Add(this.txtLogMessages);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblAuthors);
            this.Controls.Add(this.cboAuthors);
            this.Controls.Add(this.grdActivity);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "GitActivityChartForm";
            this.Text = "GitActivityChartForm";
            ((System.ComponentModel.ISupportInitialize)(this.grdActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLegend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBestStats)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdActivity;
        private System.Windows.Forms.ComboBox cboAuthors;
        private System.Windows.Forms.Label lblAuthors;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.TextBox txtLogMessages;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Label lblLegend;
        private System.Windows.Forms.DataGridView grdLegend;
        private System.Windows.Forms.DataGridView grdBestStats;
        private System.Windows.Forms.Label lblBestStats;
        private System.Windows.Forms.TextBox txtSelectedStats;
        private System.Windows.Forms.Label lblSelectedStats;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStreak;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStreakDates;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaxCommit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaxCommitDate;
    }
}