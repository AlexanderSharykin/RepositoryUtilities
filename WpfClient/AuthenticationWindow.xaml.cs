using System;
using System.Collections.Generic;
using System.Windows;
using ViewModels;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window, IVisualDialog
    {
        private Dictionary<string, string> _data;

        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        public bool? ShowDialog(object dataContext)
        {
            _data = (Dictionary<string, string>) dataContext;
            return ShowDialog();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            _data["Login"] = TxtLogin.Text;
            _data["Password"] = Psw.Password;
            DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
