using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using RepositoryScanner;
using RepositoryScanner.Activity;
using RepositoryScanner.Measures;
using RepositoryScanner.RepositoryConnection;

namespace ViewModels
{
    public class ConfigurationVm: ObservableModel
    {
        private ICommand _showHistoryCmd;
        private bool _defaultConfig;
        private string _uri;
        private string _path;
        private string _projectName;
        private bool _useMinRevision;
        private int _minRevision;
        private bool _useMaxRevision;
        private int _maxRevision;
        private IRepositoryConnection _repositoryConnection;

        public ConfigurationVm()
        {
            IsUriSelected = true;
            KnownMeasures = new List<Type> { typeof(RevisionsCountMeasure), typeof(LeaderRevisionsCountMeasure) };
            KnownDistributions = new List<Type> { typeof(NormalActivityDistribution), typeof(UniformActivityDistribution) };
            SelectedMeasure = KnownMeasures.First();
            SelectedDistribution = KnownDistributions.First();
            MinRevision = MaxRevision = 1;

            if (File.Exists(ConfigurationStorage.DefaultConfig) == false)
                ConfigurationStorage.CreateDefaultConfig();
            _defaultConfig = LoadConfigFromFile(ConfigurationStorage.DefaultConfig);

            LoadConfigCmd = new BaseCommand(o => LoadConfig());
            SaveConfigCmd = new BaseCommand(o => SaveConfig());
        }

        public Func<IVisualDialog> GetChartDialog { get; set; }

        public IRepositoryConnection RepositoryConnection
        {
            get { return _repositoryConnection; }
            set
            {
                if (_repositoryConnection != null)
                    _repositoryConnection.AuthenticationNeeded -= RepositoryAuthenticationNeeded;

                _repositoryConnection = value;

                if (_repositoryConnection != null)
                    _repositoryConnection.AuthenticationNeeded += RepositoryAuthenticationNeeded;
            }
        }

        public bool IsUriSelected { get; set; }

        public string Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                OnPropertyChanged();
            }
        }

        public bool IsPathSelected { get; set; }

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                OnPropertyChanged();
            }
        }


        public bool UseMinRevision
        {
            get { return _useMinRevision; }
            set
            {
                _useMinRevision = value;
                OnPropertyChanged();
            }
        }

        public int MinRevision
        {
            get { return _minRevision; }
            set
            {
                _minRevision = value;
                OnPropertyChanged();
            }
        }

        public bool UseMaxRevision
        {
            get { return _useMaxRevision; }
            set
            {
                _useMaxRevision = value;
                OnPropertyChanged();
            }
        }

        public int MaxRevision
        {
            get { return _maxRevision; }
            set
            {
                _maxRevision = value;
                OnPropertyChanged();
            }
        }


        public Type SelectedMeasure { get; set; }

        public IList<Type> KnownMeasures { get; private set; }

        public Type SelectedDistribution { get; set; }

        public IList<Type> KnownDistributions { get; private set; }

        /// <summary>
        /// Creates repository information taking values from input fields
        /// </summary>
        private RepositoryInfo CreateRepositoryInfo()
        {
            var repo = new RepositoryInfo();
            repo.ProjectName = ProjectName;
            if (IsUriSelected)
                repo.Uri = Uri;
            else
                repo.Path = Path;
            if (UseMinRevision && UseMaxRevision)
            {
                repo.MinRevision = (int)Math.Min(MinRevision, MaxRevision);
                repo.MaxRevision = (int)Math.Max(MinRevision, MaxRevision);
            }
            else if (UseMinRevision)
                repo.MinRevision = MinRevision;
            else if (UseMaxRevision)
                repo.MaxRevision = MaxRevision;
            return repo;
        }
        

        public ICommand ShowHistoryCmd
        {
            get
            {
                if (_showHistoryCmd == null)
                    _showHistoryCmd = new BaseCommand(o=>ShowHistory(), o=>CanShowHistory());
                return _showHistoryCmd;
            }            
        }

        public void ShowHistory()
        {
            var repo = CreateRepositoryInfo();

            //ISecuredRepositoryConnection service = new SvnRepositoryConnection();
            //service.AuthenticationNeeded += ShowAuthenticationForm;

            var client = new RepositoryHistoryScanner(RepositoryConnection);
            client.Measure = (IDaylyStatsMeasure)Activator.CreateInstance(SelectedMeasure);
            client.Distibution = (IActivityDistribution)Activator.CreateInstance(SelectedDistribution);

            var history = client.LoadHistory(repo);
            if (history != null)
            {
                var vm = new PeriodHistoryVm() {History = history};
                var frm = GetChartDialog();
                frm.ShowDialog(vm);
            }                    
        }

        public bool CanShowHistory()
        {
            if (String.IsNullOrWhiteSpace(ProjectName) ||
                IsUriSelected && String.IsNullOrWhiteSpace(Uri) ||
                IsPathSelected && String.IsNullOrWhiteSpace(Path))
                return false;

            return true;
        }

        /// <summary>
        /// Shows form with login and password fields
        /// </summary>
        private void RepositoryAuthenticationNeeded(object sender, AuthenticationNeededEventArgs e)
        {
            if (GetAuthDialog == null)
                return;
            var frm = GetAuthDialog();
            var data = new Dictionary<string, string>
                       {
                           {"Login", null},
                           {"Password", null},
                       };
            if (frm.ShowDialog(data) != true)
            {
                e.Cancel = true;
                return;
            }

            e.Password = data["Password"];
            e.Login = data["Login"];                    
        }

        public Func<IVisualDialog> GetAuthDialog { get; set; }

        public Func<IVisualDialog> GetOpenFileDialog { get; set; }

        public Func<IVisualDialog> GetSaveFileDialog { get; set; }

        public Func<IVisualDialog> GetMessageDialog { get; set; }


        public ICommand LoadConfigCmd { get; private set; }
        
        public ICommand SaveConfigCmd { get; private set; }

        public void LoadConfig()
        {
            if (GetOpenFileDialog == null)
                return;

            var ofd = GetOpenFileDialog();
            var fileVm = new FileDialogVm();
            fileVm.Filter = "Configuration file|*" + ConfigurationStorage.Extension;

            if (ofd.ShowDialog(fileVm) != true)
                return;

            LoadConfigFromFile(fileVm.FileName);
        }

        private bool LoadConfigFromFile(string fileName)
        {
            var loader = new ConfigurationStorage();
            var repository = loader.GetConfiguration(fileName);

            if (repository == null)
                return false;

            ProjectName = repository.ProjectName;
            Path = repository.Path;
            Uri = repository.Uri;

            UseMinRevision = repository.MinRevision.HasValue;
            MinRevision = UseMinRevision ? repository.MinRevision.Value : 1;

            UseMaxRevision = repository.MaxRevision.HasValue;
            MaxRevision = UseMaxRevision ? repository.MaxRevision.Value : 1;
            return true;
        }

        public void SaveConfig()
        {
            if (GetSaveFileDialog == null)
                return;
            string name = null;
            if (_defaultConfig && GetMessageDialog != null)
            {
                var msg = GetMessageDialog();
                var vm = new MessageVm
                         {
                             Caption = "Save",
                             Text = "Save as default?",
                             Yes = true,
                             No = true,
                             Cancel = true
                         };
                msg.ShowDialog(vm);

                var res = vm.DialogResult.ToString();
                if (res == "Cancel")
                    return;
                if (res == "Yes")
                    name = ConfigurationStorage.DefaultConfig;
            }

            // select file for save
            if (name == null)
            {
                var sfd = GetSaveFileDialog();
                var fileVm = new FileDialogVm();
                fileVm.Filter = "Configuration file|*" + ConfigurationStorage.Extension;
                
                if (sfd.ShowDialog(fileVm) != true)
                    return;
                name = fileVm.FileName;
            }

            var loader = new ConfigurationStorage();
            loader.SetConfiguration(CreateRepositoryInfo(), name);                
        }
    }
}
