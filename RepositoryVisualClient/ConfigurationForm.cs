using System;
using System.Windows.Forms;
using RepositoryAccess;
using RepositoryScanner;
using RepositoryScanner.Activity;
using RepositoryScanner.Measures;
using RepositoryScanner.RepositoryConnection;

namespace RepositoryVisualClient
{
    public partial class ConfigurationForm : Form
    {
        private readonly bool _defaultConfig;

        public ConfigurationForm()
        {
            InitializeComponent();

            var loader = new ConfigurationStorage();
            var repository = loader.GetConfiguration(ConfigurationStorage.DefaultConfig);
            _defaultConfig = repository != null;
            if (_defaultConfig)            
                DisplayRepositoryInfo(repository);            
        }

        /// <summary>
        /// Tries to get history from repository and show activity chart in case of success
        /// </summary>
        private void ShowActivityClick(object sender, EventArgs e)
        {
            var repo = CreateRepositoryInfo();

            ISecuredRepositoryConnection service = new SvnRepositoryConnection();
            service.AuthenticationNeeded += ShowAuthenticationForm;
            
            var client = new RepositoryHistoryScanner(service);
            client.Measure = new RevisionsCountMeasure();
            //client.Measure = new LeaderCommitCountMeasure();
            //client.Distibution = new NormalActivityDistribution();
            client.Distibution = new UniformActivityDistribution();

            var history = client.LoadHistory(repo);
            if (history != null)
                using (var frm = new ActivityChartForm())
                    frm.ShowActivity(history);
            else
                MessageBox.Show("Couldn't load the history from this repository", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows form with login and password fields
        /// </summary>
        private void ShowAuthenticationForm(object sender, AuthenticationNeededEventArgs e)
        {
            using (var frm = new AuthenticationForm())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }
                e.Password = frm.Password;
                e.Login = frm.Login;
                Invalidate();
            }
        }

        /// <summary>
        /// Fill in form input fields with repository information
        /// </summary>
        /// <param name="repo"></param>
        private void DisplayRepositoryInfo(RepositoryInfo repo)
        {
            txtUri.Text = repo.Uri;            
            txtPath.Text = repo.Path;
            if (!string.IsNullOrWhiteSpace(repo.Path))
                rdoPath.Checked = true;
            else rdoUri.Checked = true;
            txtProject.Text = repo.ProjectName;
            if (repo.MinRevision.HasValue)
            {
                chkMinRevision.Checked = true;
                nudMinRevision.Value = repo.MinRevision.Value;
            }
            if (repo.MaxRevision.HasValue)
            {
                chkMaxRevision.Checked = true;
                nudMaxRevision.Value = repo.MaxRevision.Value;
            }
        }

        /// <summary>
        /// Creates repository information taking values from input fields
        /// </summary>
        private RepositoryInfo CreateRepositoryInfo()
        {
            var repo = new RepositoryInfo();
            repo.ProjectName = txtProject.Text;
            if (rdoUri.Checked)
                repo.Uri = txtUri.Text;
            else
                repo.Path = txtPath.Text;
            if (chkMinRevision.Checked && chkMaxRevision.Checked)
            {
                repo.MinRevision = (int)Math.Min(nudMinRevision.Value, nudMaxRevision.Value);
                repo.MaxRevision = (int)Math.Max(nudMinRevision.Value, nudMaxRevision.Value);
            }
            else if (chkMinRevision.Checked)
                repo.MinRevision = (int)nudMinRevision.Value;
            else if (chkMaxRevision.Checked)
                repo.MaxRevision = (int)nudMaxRevision.Value;
            return repo;
        }

        private void MinRevisionCheckedChanged(object sender, EventArgs e)
        {
            nudMinRevision.Enabled = chkMinRevision.Checked;
        }

        private void MaxRevisionCheckedChanged(object sender, EventArgs e)
        {
            nudMaxRevision.Enabled = chkMaxRevision.Checked;
        }

        private static void ShowErrorMessage(Exception ex)
        {
            if (ex == null)
                MessageBox.Show("Something has gone wrong", "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Loads repository configuration from xml-file 
        /// </summary>
        private void LoadClick(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Filter = "Configuration file|*" + ConfigurationStorage.Extension;
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;
                var loader = new ConfigurationStorage();
                var repository = loader.GetConfiguration(ofd.FileName);
                if (repository == null)
                {
                    ShowErrorMessage(loader.Error);
                    return;
                }
                DisplayRepositoryInfo(repository);
            }
        }

        /// <summary>
        /// Saves repository configuration into xml-file 
        /// </summary>
        private void SaveClick(object sender, EventArgs e)
        {
            string name = null;
            if (_defaultConfig)
            {
                var res = MessageBox.Show("Save as default?", "Save", MessageBoxButtons.YesNoCancel,
                                          MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                    return;
                if (res == DialogResult.Yes)
                    name = ConfigurationStorage.DefaultConfig;
            }
            
            // select file for save
            if (name == null)
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Configuration file|*" + ConfigurationStorage.Extension;
                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;
                    name = sfd.FileName;
                }
            var loader = new ConfigurationStorage();
            if (false == loader.SetConfiguration(CreateRepositoryInfo(), name))
                ShowErrorMessage(loader.Error);
        }

        private void QuitClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
