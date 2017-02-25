using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModels;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for ActivityChartWindow.xaml
    /// </summary>
    public partial class ActivityChartWindow : Window, IVisualDialog
    {
        public ActivityChartWindow()
        {
            InitializeComponent();
        }

        public bool? ShowDialog(object dataContext)
        {
            DataContext = dataContext;
            return ShowDialog();
        }
    }
}
