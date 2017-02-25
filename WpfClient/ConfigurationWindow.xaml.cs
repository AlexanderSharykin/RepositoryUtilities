using System;
using System.Windows;
using System.Windows.Input;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            InitializeComponent();            
        }

        private void TextBoxNumericInput(object sender, TextCompositionEventArgs e)
        {
            int i;
            if (Int32.TryParse(e.Text, out i) == false)
                e.Handled = true;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
