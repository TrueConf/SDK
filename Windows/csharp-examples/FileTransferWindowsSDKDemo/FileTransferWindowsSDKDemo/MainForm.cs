using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;

namespace FileTransferWindowsSDKDemo
{
    public partial class MainForm : Form
    {
        private string server, id, password;

        public MainForm(string server, string id, string password)
        {
            InitializeComponent();
            this.server = server;
            this.id = id;
            this.password = password;
            sdk.OnXAfterStart += Sdk_OnXAfterStart;
            sdk.OnXError += Sdk_OnXError;
            sdk.OnServerConnected += Sdk_OnServerConnected;
            sdk.OnServerDisconnected += Sdk_OnServerDisconnected;
            sdk.OnXLogin += Sdk_OnXLogin;
            sdk.OnXLoginError += Sdk_OnXLoginError;
            sdk.OnLogout += Sdk_OnLogout;
            sdk.OnConferenceCreated += Sdk_OnConferenceCreated;
            sdk.OnConferenceDeleted += Sdk_OnConferenceDeleted;
            sdk.OnInviteReceived += Sdk_OnInviteReceived;
            sdk.OnXFileReceive += Sdk_OnXFileReceive;
            sdk.OnXFileRequestReceived += Sdk_OnXFileRequestReceived;
            sdk.OnXNotify += Sdk_OnXNotify;
        }

        private void Sdk_OnXNotify(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXNotifyEvent e)
        {
            if (statusLblLogin.Text == "Logged in")
                UpdateFileList();
        }

        private void Sdk_OnXFileRequestReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXFileRequestReceivedEvent e)
        {
            DialogResult result = MessageBox.Show("Do you want accept file " + e.fileName + " from " + e.peerId + "?", "Incoming file request", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                sdk.XFileAccept(e.fileId);
            else
                sdk.XFileReject(e.fileId);
            UpdateFileList();
        }

        private void Sdk_OnXFileReceive(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXFileReceiveEvent e)
        {
            UpdateFileList();
        }

        private void Sdk_OnInviteReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnInviteReceivedEvent e)
        {
            dynamic data = Json.Decode(e.eventDetails);
            string message = "";
            switch(data.type)
            {
                case 0:
                    message = "Do you want accept call from ";
                    break;
                case 1:
                    message = "Do you want accept invite to conference from ";
                    break;
            }
            DialogResult result = MessageBox.Show(message + data.peerId + "?", "Incoming call", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                sdk.accept();
            else
                sdk.reject();
        }

        private void Sdk_OnConferenceDeleted(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceDeletedEvent e)
        {
            statusLblConference.Text = "Not in conference";
        }

        private void Sdk_OnConferenceCreated(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceCreatedEvent e)
        {
            statusLblConference.Text = "In conference";
        }

        private void Sdk_OnLogout(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnLogoutEvent e)
        {
            statusLblLogin.Text = "Logged out";
            btnChangeServer.Enabled = true;
        }

        private void Sdk_OnXLoginError(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXLoginErrorEvent e)
        {
            statusLblLogin.Text = "Logged out";
            LoginForm.status = "Login error. Check your auth data and try again.";
            Close();
        }

        private void Sdk_OnXLogin(object sender, EventArgs e)
        {
            statusLblLogin.Text = "Logged in";
            btnChangeServer.Enabled = false;
            UpdateFileList();
        }

        private void UpdateFileList()
        {
            listViewFiles.Items.Clear();
            string fileList = sdk.XGetFileTransferList();
            dynamic data = Json.Decode(fileList);
            foreach(dynamic file in data.files)
            {
                listViewFiles.Items.Add(new ListViewItem(new string[] { "" + file.id, file.fileName, file.peerId, file.downloadDir }));
            }
        }

        private void Sdk_OnServerDisconnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerDisconnectedEvent e)
        {
            statusLblServer.Text = "No connection to " + server;
            statusLblConference.Text = "Not in conference";
            btnChangeServer.Enabled = true;
        }

        private void Sdk_OnServerConnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerConnectedEvent e)
        {
            statusLblServer.Text = "Connected to " + server;
            statusLblConference.Text = "Not in conference";
            btnChangeServer.Enabled = false;
            sdk.login(id, password);
        }

        private void Sdk_OnXError(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXErrorEvent e)
        {
            LoginForm.status = "Connection to server error. Check you connection or try another server.";
            Close();
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFilePath.Text))
            {
                if (statusLblConference.Text == "Not in conference" && !String.IsNullOrEmpty(txtPeerId.Text))
                {
                    sdk.XFileSend(txtPeerId.Text, txtFilePath.Text, "*/*");
                }
                if (statusLblConference.Text == "In conference")
                {
                    sdk.XFileSendToConference(txtFilePath.Text, "*/*");
                }
            }
        }

        private void btnChangeServer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOpenSelectedFile_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", listViewFiles.SelectedItems[0].SubItems[3].Text);
            }
            catch (Exception ex) { }
        }

        private void btnUpdateFileList_Click(object sender, EventArgs e)
        {
            UpdateFileList();
        }

        private void Sdk_OnXAfterStart(object sender, EventArgs e)
        {
            sdk.XSetCameraByIndex(0);
            sdk.XSelectSpeakerByIndex(0);
            sdk.XSelectMicByIndex(0);
            sdk.connectToServer(server);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
            }
        }
    }
}
