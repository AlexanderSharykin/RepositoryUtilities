using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;

namespace WpfClient
{
    public class WpfMessageBox : IVisualDialog
    {
        public bool? ShowDialog(object dataContext)
        {
            var vm = dataContext as MessageVm;
            if (vm == null)
                return null;
            MessageBoxButton btn;
            if (vm.Yes && vm.No)            
                btn = vm.Cancel ? MessageBoxButton.YesNoCancel : MessageBoxButton.YesNo;
            else
                btn = vm.Cancel ? MessageBoxButton.OKCancel : MessageBoxButton.OK;

            vm.DialogResult = MessageBox.Show(vm.Text, vm.Caption, btn, MessageBoxImage.Information);
            return true;
        }        
    }
}
