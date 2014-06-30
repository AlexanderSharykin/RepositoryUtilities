using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

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
            if (File.Exists(ConfigurationStorage.DefaultConfig) == false)
                ConfigurationStorage.CreateDefaultConfig();
            Application.Run(new ConfigurationForm());
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs ae)
        {
            MessageBox.Show(ae.Exception.Message, ae.Exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
