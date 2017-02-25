using System;
using System.ComponentModel;
using System.Windows.Forms;
using ViewModels;

namespace RepositoryVisualClient
{
    public partial class ConfigurationForm : Form
    {
        private ConfigurationVm _dataContext;

        public ConfigurationForm()
        {
            InitializeComponent();
        }

        public ConfigurationVm DataContext
        {
            get { return _dataContext; }
            set
            {
                if (_dataContext != null)                
                    _dataContext.PropertyChanged -= DataContextChanged;

                _dataContext = value;

                if (_dataContext != null)
                {
                    _dataContext.PropertyChanged += DataContextChanged;
                    SetUri();
                    SetPath();
                    SetProjectName();
                    SetUseMinRevision();
                    SetMinRevision();
                    SetUseMaxRevision();
                    SetMaxRevision();

                    foreach (var t in DataContext.KnownMeasures)
                        cboMeasure.Items.Add(GetTypeDescription(t));

                    foreach (var t in DataContext.KnownDistributions)
                        cboDistribution.Items.Add(GetTypeDescription(t));
                    cboMeasure.SelectedIndex = cboDistribution.SelectedIndex = 0;
                }
            }
        }

        private void DataContextChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Uri": SetUri(); break;
                case "Path": SetPath(); break;
                case "ProjectName": SetProjectName(); break;
                case "UseMinRevision": SetUseMinRevision(); break;
                case "MinRevision": SetMinRevision(); break;
                case "UseMaxRevision": SetUseMaxRevision(); break;
                case "MaxRevision": SetMaxRevision(); break;
            }
        }

        private void SetUri()
        {
            txtUri.Text = DataContext.Uri;
        }

        private void SetPath()
        {
            txtPath.Text = DataContext.Path;
        }

        private void SetProjectName()
        {
            txtProject.Text = DataContext.ProjectName;
        }

        private void SetUseMinRevision()
        {
            chkMinRevision.Checked =
            nudMinRevision.Enabled = DataContext.UseMinRevision;
        }

        private void SetMinRevision()
        {
            nudMinRevision.Value = DataContext.MinRevision;
        }

        private void SetUseMaxRevision()
        {
            chkMaxRevision.Checked = 
            nudMaxRevision.Enabled = DataContext.UseMaxRevision;
        }

        private void SetMaxRevision()
        {
            nudMaxRevision.Value = DataContext.MaxRevision;
        }

        private static string GetTypeDescription(Type t)
        {
            string description;
            var attr = t.GetCustomAttributes(typeof (DescriptionAttribute), false);
            if (attr.Length == 0)
                description = string.Format("{0} ({1})", t.Name, t.Assembly.GetName().Name);
            else description = ((DescriptionAttribute) attr[0]).Description;
            return description;
        }

        /// <summary>
        /// Tries to get history from repository and show activity chart in case of success
        /// </summary>
        private void ShowActivityClick(object sender, EventArgs e)
        {
            if (DataContext.CanShowHistory())
                DataContext.ShowHistory();
        }

        private void MinRevisionCheckedChanged(object sender, EventArgs e)
        {
            DataContext.UseMinRevision = chkMinRevision.Checked;
        }

        private void MaxRevisionCheckedChanged(object sender, EventArgs e)
        {
            DataContext.UseMaxRevision = chkMaxRevision.Checked;
        }

        private void UriChanged(object sender, EventArgs e)
        {
            DataContext.Uri = txtUri.Text;
        }

        private void PathChanged(object sender, EventArgs e)
        {
            DataContext.Path = txtPath.Text;
        }

        private void ProjectChanged(object sender, EventArgs e)
        {
            DataContext.ProjectName = txtProject.Text;
        }

        private void MinRevisionChanged(object sender, EventArgs e)
        {
            DataContext.MinRevision = (int)nudMinRevision.Value;
        }

        private void MaxRevisionChanged(object sender, EventArgs e)
        {
            DataContext.MaxRevision = (int)nudMaxRevision.Value;
        }

        private void MeasureChanged(object sender, EventArgs e)
        {
            if (cboMeasure.SelectedIndex > 0)
                DataContext.SelectedMeasure = DataContext.KnownMeasures[cboMeasure.SelectedIndex];
        }

        private void DistributionChanged(object sender, EventArgs e)
        {
            if (cboDistribution.SelectedIndex > 0)
                DataContext.SelectedDistribution = DataContext.KnownDistributions[cboDistribution.SelectedIndex];
        }

        /// <summary>
        /// Loads repository configuration from xml-file 
        /// </summary>
        private void LoadClick(object sender, EventArgs e)
        {
            DataContext.LoadConfig();
        }

        /// <summary>
        /// Saves repository configuration into xml-file 
        /// </summary>
        private void SaveClick(object sender, EventArgs e)
        {
            DataContext.SaveConfig();
        }

        private void QuitClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
