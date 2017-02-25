namespace RepositoryVisualClient
{
    partial class ConfigurationForm
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
            this.grdRepository = new System.Windows.Forms.GroupBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtUri = new System.Windows.Forms.TextBox();
            this.rdoPath = new System.Windows.Forms.RadioButton();
            this.rdoUri = new System.Windows.Forms.RadioButton();
            this.lblProject = new System.Windows.Forms.Label();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.chkMinRevision = new System.Windows.Forms.CheckBox();
            this.nudMinRevision = new System.Windows.Forms.NumericUpDown();
            this.nudMaxRevision = new System.Windows.Forms.NumericUpDown();
            this.chkMaxRevision = new System.Windows.Forms.CheckBox();
            this.tsConfig = new System.Windows.Forms.ToolStrip();
            this.tsbutLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbutSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbutShowActivity = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbutQuit = new System.Windows.Forms.ToolStripButton();
            this.cboMeasure = new System.Windows.Forms.ComboBox();
            this.lblMeasure = new System.Windows.Forms.Label();
            this.lblDistribution = new System.Windows.Forms.Label();
            this.cboDistribution = new System.Windows.Forms.ComboBox();
            this.grdRepository.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinRevision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxRevision)).BeginInit();
            this.tsConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdRepository
            // 
            this.grdRepository.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRepository.Controls.Add(this.txtPath);
            this.grdRepository.Controls.Add(this.txtUri);
            this.grdRepository.Controls.Add(this.rdoPath);
            this.grdRepository.Controls.Add(this.rdoUri);
            this.grdRepository.Location = new System.Drawing.Point(12, 28);
            this.grdRepository.Name = "grdRepository";
            this.grdRepository.Size = new System.Drawing.Size(578, 91);
            this.grdRepository.TabIndex = 0;
            this.grdRepository.TabStop = false;
            this.grdRepository.Text = "Repository";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(105, 57);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(467, 20);
            this.txtPath.TabIndex = 3;
            this.txtPath.TextChanged += new System.EventHandler(this.PathChanged);
            // 
            // txtUri
            // 
            this.txtUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUri.Location = new System.Drawing.Point(105, 19);
            this.txtUri.Name = "txtUri";
            this.txtUri.Size = new System.Drawing.Size(467, 20);
            this.txtUri.TabIndex = 2;
            this.txtUri.TextChanged += new System.EventHandler(this.UriChanged);
            // 
            // rdoPath
            // 
            this.rdoPath.AutoSize = true;
            this.rdoPath.Location = new System.Drawing.Point(6, 57);
            this.rdoPath.Name = "rdoPath";
            this.rdoPath.Size = new System.Drawing.Size(47, 17);
            this.rdoPath.TabIndex = 1;
            this.rdoPath.Text = "Path";
            this.rdoPath.UseVisualStyleBackColor = true;
            // 
            // rdoUri
            // 
            this.rdoUri.AutoSize = true;
            this.rdoUri.Checked = true;
            this.rdoUri.Location = new System.Drawing.Point(6, 19);
            this.rdoUri.Name = "rdoUri";
            this.rdoUri.Size = new System.Drawing.Size(38, 17);
            this.rdoUri.TabIndex = 0;
            this.rdoUri.TabStop = true;
            this.rdoUri.Text = "Uri";
            this.rdoUri.UseVisualStyleBackColor = true;
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(32, 125);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(40, 13);
            this.lblProject.TabIndex = 10;
            this.lblProject.Text = "Project";
            // 
            // txtProject
            // 
            this.txtProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProject.Location = new System.Drawing.Point(117, 125);
            this.txtProject.Name = "txtProject";
            this.txtProject.Size = new System.Drawing.Size(467, 20);
            this.txtProject.TabIndex = 9;
            this.txtProject.TextChanged += new System.EventHandler(this.ProjectChanged);
            // 
            // chkMinRevision
            // 
            this.chkMinRevision.AutoSize = true;
            this.chkMinRevision.Location = new System.Drawing.Point(18, 155);
            this.chkMinRevision.Name = "chkMinRevision";
            this.chkMinRevision.Size = new System.Drawing.Size(93, 17);
            this.chkMinRevision.TabIndex = 11;
            this.chkMinRevision.Text = "From Revision";
            this.chkMinRevision.UseVisualStyleBackColor = true;
            this.chkMinRevision.CheckedChanged += new System.EventHandler(this.MinRevisionCheckedChanged);
            // 
            // nudMinRevision
            // 
            this.nudMinRevision.Enabled = false;
            this.nudMinRevision.Location = new System.Drawing.Point(117, 155);
            this.nudMinRevision.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMinRevision.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinRevision.Name = "nudMinRevision";
            this.nudMinRevision.Size = new System.Drawing.Size(74, 20);
            this.nudMinRevision.TabIndex = 12;
            this.nudMinRevision.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinRevision.ValueChanged += new System.EventHandler(this.MinRevisionChanged);
            // 
            // nudMaxRevision
            // 
            this.nudMaxRevision.Enabled = false;
            this.nudMaxRevision.Location = new System.Drawing.Point(117, 181);
            this.nudMaxRevision.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMaxRevision.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxRevision.Name = "nudMaxRevision";
            this.nudMaxRevision.Size = new System.Drawing.Size(74, 20);
            this.nudMaxRevision.TabIndex = 14;
            this.nudMaxRevision.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxRevision.ValueChanged += new System.EventHandler(this.MaxRevisionChanged);
            // 
            // chkMaxRevision
            // 
            this.chkMaxRevision.AutoSize = true;
            this.chkMaxRevision.Location = new System.Drawing.Point(18, 181);
            this.chkMaxRevision.Name = "chkMaxRevision";
            this.chkMaxRevision.Size = new System.Drawing.Size(83, 17);
            this.chkMaxRevision.TabIndex = 13;
            this.chkMaxRevision.Text = "To Revision";
            this.chkMaxRevision.UseVisualStyleBackColor = true;
            this.chkMaxRevision.CheckedChanged += new System.EventHandler(this.MaxRevisionCheckedChanged);
            // 
            // tsConfig
            // 
            this.tsConfig.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbutLoad,
            this.tsbutSave,
            this.toolStripSeparator1,
            this.tsbutShowActivity,
            this.toolStripSeparator2,
            this.tsbutQuit});
            this.tsConfig.Location = new System.Drawing.Point(0, 0);
            this.tsConfig.Name = "tsConfig";
            this.tsConfig.Size = new System.Drawing.Size(602, 25);
            this.tsConfig.TabIndex = 15;
            this.tsConfig.Text = "toolStrip1";
            // 
            // tsbutLoad
            // 
            this.tsbutLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutLoad.Image = global::RepositoryVisualClient.Properties.Resources.Load;
            this.tsbutLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutLoad.Name = "tsbutLoad";
            this.tsbutLoad.Size = new System.Drawing.Size(23, 22);
            this.tsbutLoad.Text = "Load configuration";
            this.tsbutLoad.Click += new System.EventHandler(this.LoadClick);
            // 
            // tsbutSave
            // 
            this.tsbutSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutSave.Image = global::RepositoryVisualClient.Properties.Resources.Save;
            this.tsbutSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutSave.Name = "tsbutSave";
            this.tsbutSave.Size = new System.Drawing.Size(23, 22);
            this.tsbutSave.Text = "Save configuration";
            this.tsbutSave.Click += new System.EventHandler(this.SaveClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbutShowActivity
            // 
            this.tsbutShowActivity.Image = global::RepositoryVisualClient.Properties.Resources.Chart;
            this.tsbutShowActivity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutShowActivity.Name = "tsbutShowActivity";
            this.tsbutShowActivity.Size = new System.Drawing.Size(99, 22);
            this.tsbutShowActivity.Text = "Activity Chart";
            this.tsbutShowActivity.Click += new System.EventHandler(this.ShowActivityClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbutQuit
            // 
            this.tsbutQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbutQuit.Image = global::RepositoryVisualClient.Properties.Resources.Quit;
            this.tsbutQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbutQuit.Name = "tsbutQuit";
            this.tsbutQuit.Size = new System.Drawing.Size(23, 22);
            this.tsbutQuit.Text = "Quit";
            this.tsbutQuit.Click += new System.EventHandler(this.QuitClick);
            // 
            // cboMeasure
            // 
            this.cboMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMeasure.FormattingEnabled = true;
            this.cboMeasure.Location = new System.Drawing.Point(303, 153);
            this.cboMeasure.Name = "cboMeasure";
            this.cboMeasure.Size = new System.Drawing.Size(281, 21);
            this.cboMeasure.TabIndex = 16;
            this.cboMeasure.SelectedIndexChanged += new System.EventHandler(this.MeasureChanged);
            // 
            // lblMeasure
            // 
            this.lblMeasure.AutoSize = true;
            this.lblMeasure.Location = new System.Drawing.Point(227, 155);
            this.lblMeasure.Name = "lblMeasure";
            this.lblMeasure.Size = new System.Drawing.Size(48, 13);
            this.lblMeasure.TabIndex = 17;
            this.lblMeasure.Text = "Measure";
            // 
            // lblDistribution
            // 
            this.lblDistribution.AutoSize = true;
            this.lblDistribution.Location = new System.Drawing.Point(227, 181);
            this.lblDistribution.Name = "lblDistribution";
            this.lblDistribution.Size = new System.Drawing.Size(59, 13);
            this.lblDistribution.TabIndex = 18;
            this.lblDistribution.Text = "Distribution";
            // 
            // cboDistribution
            // 
            this.cboDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDistribution.FormattingEnabled = true;
            this.cboDistribution.Location = new System.Drawing.Point(303, 181);
            this.cboDistribution.Name = "cboDistribution";
            this.cboDistribution.Size = new System.Drawing.Size(281, 21);
            this.cboDistribution.TabIndex = 19;
            this.cboDistribution.SelectedIndexChanged += new System.EventHandler(this.DistributionChanged);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 209);
            this.Controls.Add(this.cboDistribution);
            this.Controls.Add(this.lblDistribution);
            this.Controls.Add(this.lblMeasure);
            this.Controls.Add(this.cboMeasure);
            this.Controls.Add(this.tsConfig);
            this.Controls.Add(this.nudMaxRevision);
            this.Controls.Add(this.chkMaxRevision);
            this.Controls.Add(this.nudMinRevision);
            this.Controls.Add(this.chkMinRevision);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.txtProject);
            this.Controls.Add(this.grdRepository);
            this.Name = "ConfigurationForm";
            this.Text = "Configuration";
            this.grdRepository.ResumeLayout(false);
            this.grdRepository.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinRevision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxRevision)).EndInit();
            this.tsConfig.ResumeLayout(false);
            this.tsConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grdRepository;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtUri;
        private System.Windows.Forms.RadioButton rdoPath;
        private System.Windows.Forms.RadioButton rdoUri;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.CheckBox chkMinRevision;
        private System.Windows.Forms.NumericUpDown nudMinRevision;
        private System.Windows.Forms.NumericUpDown nudMaxRevision;
        private System.Windows.Forms.CheckBox chkMaxRevision;
        private System.Windows.Forms.ToolStrip tsConfig;
        private System.Windows.Forms.ToolStripButton tsbutLoad;
        private System.Windows.Forms.ToolStripButton tsbutSave;
        private System.Windows.Forms.ToolStripButton tsbutQuit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbutShowActivity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ComboBox cboMeasure;
        private System.Windows.Forms.Label lblMeasure;
        private System.Windows.Forms.Label lblDistribution;
        private System.Windows.Forms.ComboBox cboDistribution;
    }
}

