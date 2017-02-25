using System.Windows.Forms;
using ViewModels;

namespace RepositoryVisualClient
{
    public class WinformsMessageBox : IVisualDialog
    {
        public bool? ShowDialog(object dataContext)
        {
            var vm = dataContext as MessageVm;
            if (vm == null)
                return null;
            MessageBoxButtons btn;
            if (vm.Yes && vm.No)
                btn = vm.Cancel ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo;
            else
                btn = vm.Cancel ? MessageBoxButtons.OKCancel : MessageBoxButtons.OK;

            vm.DialogResult = MessageBox.Show(vm.Text, vm.Caption, btn, MessageBoxIcon.Information);
            return true;
        }        
    }
}
