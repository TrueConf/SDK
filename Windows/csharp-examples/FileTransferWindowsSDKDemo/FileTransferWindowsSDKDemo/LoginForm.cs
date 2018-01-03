using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileTransferWindowsSDKDemo
{
    public partial class LoginForm : Form
    {
        public static string status;

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public LoginForm()
        {            
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtLogin.Text) && !String.IsNullOrEmpty(txtPassword.Text))
            {
                MainForm mainForm = new MainForm(String.IsNullOrEmpty(txtServer.Text)? "" : txtServer.Text, txtLogin.Text, txtPassword.Text);
                
                txtPassword.Clear();
                labelError.Text = "";

                mainForm.StartPosition = FormStartPosition.CenterScreen;
                Hide();
                mainForm.FormClosing += (s, ee) =>
                {
                    this.Show();
                    labelError.Text = LoginForm.status;
                };
                mainForm.ShowDialog();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }
    }
}
