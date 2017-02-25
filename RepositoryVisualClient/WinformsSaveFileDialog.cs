using System.Windows.Forms;
using ViewModels;

namespace RepositoryVisualClient
{
    class WinformsSaveFileDialog : IVisualDialog
    {
        public bool? ShowDialog(object dataContext)
        {
            var vm = dataContext as FileDialogVm;
            if (vm == null)
                return null;
            var sfd = new SaveFileDialog
            {
                Filter = vm.Filter,
                Title = vm.Title
            };
            var res = sfd.ShowDialog();
            if (res == DialogResult.OK)
                vm.FileName = sfd.FileName;
            return res == DialogResult.OK;
        }
    }
}
