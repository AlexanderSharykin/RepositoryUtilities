using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ViewModels;

namespace RepositoryVisualClient
{
    /// <summary>
    /// Login form
    /// </summary>
    public partial class AuthenticationForm : Form, IVisualDialog
    {
        private Dictionary<string, string> _data;

        public AuthenticationForm()
        {
            InitializeComponent();
        }

        public bool? ShowDialog(object dataContext)
        {
            _data = (Dictionary<string, string>)dataContext;
            return ShowDialog() == DialogResult.OK;
        }

        private void ApplyClick(object sender, EventArgs e)
        {
            _data["Login"] = txtLogin.Text;
            _data["Password"] = txtPassword.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
