using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoSDK
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
            textBoxPassword.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxServer.Text != "" && textBoxLogin.Text != "" && textBoxPassword.Text != "")
            {
                Form1.ServerName = textBoxServer.Text.ToString(); //send the server data to the main form
                Form1.TrueConf_Id = textBoxLogin.Text.ToString(); //send the authorization data to the main form
                Form1.TrueConf_Password = textBoxPassword.Text.ToString(); //send the authorization data to the main form
                Close(); //close this form
            }
        }
    }
}
