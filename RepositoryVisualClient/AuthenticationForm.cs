using System;
using System.Windows.Forms;

namespace RepositoryVisualClient
{
    /// <summary>
    /// Login form
    /// </summary>
    public partial class AuthenticationForm : Form
    {
        public AuthenticationForm()
        {
            InitializeComponent();
        }

        public string Login { get; set; }
        public string Password { get; set; }

        private void ApplyClick(object sender, EventArgs e)
        {
            Login = txtLogin.Text;
            Password = txtPassword.Text;
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
