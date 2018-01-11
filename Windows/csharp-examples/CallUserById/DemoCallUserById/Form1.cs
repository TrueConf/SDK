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

namespace DemoSDK
{
    public partial class Form1 : Form
    {
        public static string ServerName;
        public static string TrueConf_Id;
        public static string TrueConf_Password;
        private bool started = false; //flag means that TrueConf CallX started
        private bool isCalling; //flag means the CallX is calling now

        public Form1()
        {
            Auth auth = new Auth(); //open authorization dialog after form is initialized
            auth.ShowDialog(); //open authorization dialog after form is initialized        
            InitializeComponent();
            axTrueConfCallX1.OnServerConnected += axTrueConfCallX1_OnServerConnected; //handle OnServerConnected event
            axTrueConfCallX1.OnXAfterStart += axTrueConfCallX1_OnXAfterStart; //handle OnXAfterStart event
            axTrueConfCallX1.OnXLogin += axTrueConfCallX1_OnXLogin; //handle OnXLogin event
            axTrueConfCallX1.OnXLoginError += axTrueConfCallX1_OnXLoginError; //handle OnXLoginError event
            axTrueConfCallX1.OnConferenceCreated += axTrueConfCallX1_OnConferenceCreated; //handle OnConferenceCreated event
            axTrueConfCallX1.OnInviteReceived += AxTrueConfCallX1_OnInviteReceived; //handle OnInviteReceived event
            axTrueConfCallX1.OnServerDisconnected += AxTrueConfCallX1_OnServerDisconnected; //handle OnServerDisconnected event
            axTrueConfCallX1.OnLogout += AxTrueConfCallX1_OnLogout; //handle OnLogout event
            axTrueConfCallX1.OnConferenceDeleted += AxTrueConfCallX1_OnConferenceDeleted; //handle OnConferenceDeleted event
        }

        private void AxTrueConfCallX1_OnConferenceDeleted(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceDeletedEvent e)
        {
            toolStripStatusConference.Text = "Not in conference";
        }

        private void AxTrueConfCallX1_OnLogout(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnLogoutEvent e)
        {
            toolStripStatusLogin.Text = "Not logged in";
        }

        private void AxTrueConfCallX1_OnServerDisconnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerDisconnectedEvent e)
        {
            toolStripStatusServer.Text = "Not connected";
        }

        private void AxTrueConfCallX1_OnInviteReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnInviteReceivedEvent e)
        {
            dynamic data = Json.Decode(e.eventDetails); //get information about incoming call from JSON data
            string message = "";
            switch (data.type)
            {
                case 0: //type 0 - peer-to-peer call
                    message = "Do you want accept call from ";
                    break;
                case 1: //type 1 - conference
                    message = "Do you want accept invite to conference from ";
                    break;
            }
            DialogResult result = MessageBox.Show(message + data.peerId + "?", "Incoming call", MessageBoxButtons.YesNo); //ask user
            if (result == DialogResult.Yes)
                axTrueConfCallX1.accept(); //if user says "Yes" - accept incoming call
            else
                axTrueConfCallX1.reject();// else - reject incoming call
        }

        private void axTrueConfCallX1_OnServerConnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerConnectedEvent e) //notification about success connection to server
        {
            toolStripStatusServer.Text = "Connected to " + ServerName; //show server status
            if(!String.IsNullOrEmpty(TrueConf_Id) && !String.IsNullOrEmpty(TrueConf_Password))
                axTrueConfCallX1.login(TrueConf_Id, TrueConf_Password); //login on TrueConf Server with login and password
        }
        private void axTrueConfCallX1_OnXLogin(object sender, EventArgs e) //notification about successed login
        {
            Enabled = true;
            toolStripStatusLogin.Text = "You are logged in as " + TrueConf_Id; //show login status
            isCalling = false; //flag means the CallX can make calls
            for (int i = 0; i < axTrueConfCallX1.XGetCameraCount(); i++)
                comboBoxCameras.Items.Add(axTrueConfCallX1.XGetCameraByIndex(i)); //get cameras and put them to combobox
            for (int i = 0; i < axTrueConfCallX1.XGetMicCount(); i++)
                comboBoxMicrophones.Items.Add(axTrueConfCallX1.XGetMicByIndex(i)); //get microphones and put them to combobox
            for (int i = 0; i < axTrueConfCallX1.XGetSpeakerCount(); i++)
                comboBoxSpeakers.Items.Add(axTrueConfCallX1.XGetSpeakerByIndex(i)); //get speakers and put them to combobox       
        }
        private void axTrueConfCallX1_OnXLoginError(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXLoginErrorEvent e)
        {
            toolStripStatusLogin.Text = "Login error!"; //show login status
            Auth auth = new Auth(); //open authorization dialog again
            auth.ShowDialog();
        }
        private void axTrueConfCallX1_OnXAfterStart(object sender, EventArgs e)
        {
            started = true; //flag means TrueConf CallX started
            toolStripStatusServer.Text = "Not connected";
            toolStripStatusLogin.Text = "Not logged in";
            toolStripStatusConference.Text = "Not in conference";
            Enabled = false;
            if (ServerName != null)
            {
                axTrueConfCallX1.connectToServer(ServerName); //after start connect to the server/service
            }
        }
        private void comboBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCameras.Items.Count > 0)
                axTrueConfCallX1.XSelectCamera(comboBoxCameras.SelectedItem.ToString()); //set the camera that chosen in the comboBoxCameras
        }
        private void comboBoxMicrophones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCameras.Items.Count > 0)
                axTrueConfCallX1.XSelectMicByIndex(comboBoxMicrophones.SelectedIndex); //set the microphone that chosen in the comboBoxMicrophones
        }
        private void comboBoxSpeakers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCameras.Items.Count > 0)
                axTrueConfCallX1.XSelectSpeakerByIndex(comboBoxSpeakers.SelectedIndex); //set the speaker that chosen in the comboBoxSpeakers
        }
        private void buttonCall_Click(object sender, EventArgs e)
        {
            if (textBoxTrueConfIdForCall.Text != "" && !isCalling) //if trueconf id entered, and CallX is connected to server and authorized and isn't in call or conference
            {
                isCalling = true;
                axTrueConfCallX1.call(textBoxTrueConfIdForCall.Text); //call the entered user                
                textBoxTrueConfIdForCall.Text = ""; //clear the textbox
            }
        }
        private void buttonEndCall_Click(object sender, EventArgs e)
        {
            if (isCalling) //if Callx is in call
            {
                axTrueConfCallX1.hangUp(); //end the call
                isCalling = false; //and set that the CallX can make the calls
            }
        }
        
        private void axTrueConfCallX1_OnConferenceCreated(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceCreatedEvent e)
        {
            toolStripStatusConference.Text = "In conference"; //show conference status
        }
    }
}
