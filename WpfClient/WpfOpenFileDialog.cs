using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using ViewModels;

namespace WpfClient
{
    public class WpfOpenFileDialog : IVisualDialog
    {
        public bool? ShowDialog(object dataContext)
        {
            var vm = dataContext as FileDialogVm;
            if (vm == null)
                return null;
            var ofd = new OpenFileDialog
            {
                Filter = vm.Filter,
                Title = vm.Title
            };
            var res = ofd.ShowDialog();
            if (res ?? false)
                vm.FileName = ofd.FileName;
            return res;
        }
    }
}
