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
        private string server, id, password; // fields: server name (or host), TrueConf Id, password - for connection to server and login on server

        public MainForm(string server, string id, string password)
        {
            InitializeComponent();
            this.server = server; //get server from LoginForm
            this.id = id; //get TrueConf Id from LoginForm
            this.password = password; //get password from LoginForm
            sdk.OnXAfterStart += Sdk_OnXAfterStart; //handle OnXAfterStart event
            sdk.OnXError += Sdk_OnXError; //handle OnXError event
            sdk.OnServerConnected += Sdk_OnServerConnected; //handle OnServerConnected event
            sdk.OnServerDisconnected += Sdk_OnServerDisconnected; //handle OnServerDisconnected event
            sdk.OnXLogin += Sdk_OnXLogin; //handle OnXLogin event (last OnXLoginOk)
            sdk.OnXLoginError += Sdk_OnXLoginError; //handle OnXLoginError event
            sdk.OnLogout += Sdk_OnLogout; //handle OnLogout event
            sdk.OnConferenceCreated += Sdk_OnConferenceCreated; //handle OnConferenceCreated event
            sdk.OnConferenceDeleted += Sdk_OnConferenceDeleted; //handle OnConferenceDeleted event
            sdk.OnInviteReceived += Sdk_OnInviteReceived; //handle OnInviteReceived event
            sdk.OnXFileReceive += Sdk_OnXFileReceive; // handle OnXFileReceive event
            sdk.OnXFileRequestReceived += Sdk_OnXFileRequestReceived; //handle OnXFileRequestReceived event
            sdk.OnXNotify += Sdk_OnXNotify; //handle OnXNotify event
        }

        private void Sdk_OnXNotify(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXNotifyEvent e)
        {
            if (statusLblLogin.Text == "Logged in") //after success login
                UpdateFileList(); //get list of received files
        }

        private void Sdk_OnXFileRequestReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXFileRequestReceivedEvent e)
        {
            DialogResult result = MessageBox.Show("Do you want accept file " + e.fileName + " from " + e.peerId + "?", "Incoming file request", MessageBoxButtons.YesNo); //ask user about incoming file
            if (result == DialogResult.Yes) //if user says "Yes"
                sdk.XFileAccept(e.fileId); //accept incoming file
            else
                sdk.XFileReject(e.fileId); //else reject incoming file
            UpdateFileList(); //and update list of received files to show new file
        }

        private void Sdk_OnXFileReceive(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXFileReceiveEvent e)
        {
            UpdateFileList(); //when receive new file - update list of received files to show new file 
        }

        private void Sdk_OnInviteReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnInviteReceivedEvent e)
        {
            dynamic data = Json.Decode(e.eventDetails); //get information about incoming call from JSON data
            string message = "";
            switch(data.type)
            {
                case 0: //type 0 - incoming peer-to-peer call
                    message = "Do you want accept call from ";
                    break;
                case 1: //type 1 - incoming invitation to conference
                    message = "Do you want accept invite to conference from ";
                    break;
            }
            DialogResult result = MessageBox.Show(message + data.peerId + "?", "Incoming call", MessageBoxButtons.YesNo); //ask user about incoming call or invitation
            if (result == DialogResult.Yes) //if user says "Yes"
                sdk.accept(); //accept incoming call or invitation
            else
                sdk.reject(); //else reject incoming call or invitation
        }

        private void Sdk_OnConferenceDeleted(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceDeletedEvent e)
        {
            statusLblConference.Text = "Not in conference"; //update conference status
        }

        private void Sdk_OnConferenceCreated(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceCreatedEvent e)
        {
            statusLblConference.Text = "In conference"; //update conference status
        }

        private void Sdk_OnLogout(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnLogoutEvent e)
        {
            statusLblLogin.Text = "Logged out"; //update login status
        }

        private void Sdk_OnXLoginError(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXLoginErrorEvent e)
        {
            statusLblLogin.Text = "Logged out"; //update login status
            LoginForm.status = "Login error. Check your auth data and try again."; //send message to LoginForm
            Close(); //closing this form to open LoginForm again
        }

        private void Sdk_OnXLogin(object sender, EventArgs e)
        {
            statusLblLogin.Text = "Logged in"; //update login status
        }

        private void UpdateFileList()
        {
            listViewFiles.Items.Clear(); //clear current list of received files
            string fileList = sdk.XGetFileTransferList(); //get list of reseived files in JSON
            dynamic data = Json.Decode(fileList); //convert JSON to dynamic value
            foreach(dynamic file in data.files)
            {
                listViewFiles.Items.Add(new ListViewItem(new string[] {file.fileName, file.peerId, file.downloadDir })); //add file info to list of received files: file id, file name, sender, path to file
            }
        }

        private void Sdk_OnServerDisconnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerDisconnectedEvent e)
        {
            statusLblServer.Text = "No connection to " + server; //update server status
            statusLblConference.Text = "Not in conference"; //update conference status
        }

        private void Sdk_OnServerConnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerConnectedEvent e)
        {
            statusLblServer.Text = "Connected to " + server;
            statusLblConference.Text = "Not in conference";
            sdk.login(id, password); //login
        }

        private void Sdk_OnXError(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXErrorEvent e)
        {
            LoginForm.status = "Error start SDK component :(";
            Close();
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFilePath.Text))
            {
                if (statusLblConference.Text == "Not in conference" && !String.IsNullOrEmpty(txtPeerId.Text)) //if not in conference and user id is not empty
                {
                    sdk.XFileSend(txtPeerId.Text, txtFilePath.Text, "*/*"); //send file to user id
                }
                if (statusLblConference.Text == "In conference") //if in conference
                {
                    sdk.XFileSendToConference(txtFilePath.Text, "*/*"); //send file to conference
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
                System.Diagnostics.Process.Start("explorer.exe", listViewFiles.SelectedItems[0].SubItems[3].Text); //open in explorer selected received file
            }
            catch (Exception ex)
            {
                labelErrorOpenFile.Text = "Error open file: " + ex.Message; //show error message
            }
        }

        private void btnUpdateFileList_Click(object sender, EventArgs e)
        {
            UpdateFileList(); //updating list of received files
        }

        private void Sdk_OnXAfterStart(object sender, EventArgs e)
        {
            sdk.XSetCameraByIndex(0); //setting the camera
            sdk.XSelectSpeakerByIndex(0); //setting the speaker
            sdk.XSelectMicByIndex(0); //setting the micriphone
            sdk.connectToServer(server); //connecting to the server
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); //show the open file dialog
            if(result == DialogResult.OK) //if the dialog return "OK"
            {
                txtFilePath.Text = openFileDialog1.FileName; //writing the file name in the appropriate field
            }
        }
    }
}
