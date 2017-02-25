using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using ViewModels;

namespace WpfClient
{
    class WpfSaveFileDialog : IVisualDialog
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
            if (res ?? false)
                vm.FileName = sfd.FileName;
            return res;
        }
    }
}
