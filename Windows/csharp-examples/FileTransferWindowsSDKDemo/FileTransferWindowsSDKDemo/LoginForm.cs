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
        public static string status; //a status field that retrieves data from MainForm

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close(); //closing form
        }

        public LoginForm()
        {            
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtLogin.Text) && !String.IsNullOrEmpty(txtPassword.Text)) //if login and password fields aren't empty
            {
                MainForm mainForm = new MainForm(String.IsNullOrEmpty(txtServer.Text)? "" : txtServer.Text, txtLogin.Text, txtPassword.Text); //opening the MainForm and passing the server, login and password to the MainForm
                
                txtPassword.Clear(); //clearing password field
                labelError.Text = ""; //clearing label with error message

                mainForm.StartPosition = FormStartPosition.CenterScreen; //setting MainForm start position
                Hide(); //hide this form
                mainForm.FormClosing += (s, ee) => //when MainForm closing
                {
                    this.Show(); //showing this form
                    labelError.Text = LoginForm.status; //updating error message from status field
                };
                mainForm.ShowDialog(); //showing MainForm
            }
        }        
    }
}
