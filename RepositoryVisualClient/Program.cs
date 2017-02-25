using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using RepositoryAccess;
using ViewModels;

namespace RepositoryVisualClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationOnThreadException;
            
            var vm = new ConfigurationVm
                     {
                         RepositoryConnection = new SvnRepositoryConnection(),
                         GetAuthDialog = () => new AuthenticationForm(),
                         GetChartDialog = () => new ActivityChartForm(),
                         GetOpenFileDialog = () => new WinformsOpenFileDialog(),
                         GetSaveFileDialog = () => new WinformsSaveFileDialog(),
                         GetMessageDialog = () => new WinformsMessageBox()
                     };
            var f = new ConfigurationForm();
            f.DataContext = vm;
            Application.Run(f);
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs ae)
        {
            MessageBox.Show(ae.Exception.Message, ae.Exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
