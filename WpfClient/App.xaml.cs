using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using RepositoryAccess;
using ViewModels;

namespace WpfClient
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow = new ConfigurationWindow
                         {
                             DataContext = new ConfigurationVm
                                           {
                                               RepositoryConnection = new SvnRepositoryConnection(),
                                               GetAuthDialog = () => new AuthenticationWindow(),
                                               GetChartDialog = () => new ActivityChartWindow(),
                                               GetOpenFileDialog = () => new WpfOpenFileDialog(),
                                               GetSaveFileDialog = () => new WpfSaveFileDialog(),
                                               GetMessageDialog = () => new WpfMessageBox()
                                           }
                         };
            MainWindow.ShowDialog();
        }
    }
}
