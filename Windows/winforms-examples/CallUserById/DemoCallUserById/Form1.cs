using System;
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

        public Form1()
        {
            Auth auth = new Auth(); //open authorization dialog after form is initialized
            auth.ShowDialog(); //open authorization dialog after form is initialized        
            InitializeComponent();
            Enabled = false;
            axTrueConfCallX1.OnServerConnected += axTrueConfCallX1_OnServerConnected; //handle OnServerConnected event
            axTrueConfCallX1.OnXAfterStart += axTrueConfCallX1_OnXAfterStart; //handle OnXAfterStart event
            axTrueConfCallX1.OnXLogin += axTrueConfCallX1_OnXLogin; //handle OnXLogin event
            axTrueConfCallX1.OnXLoginError += axTrueConfCallX1_OnXLoginError; //handle OnXLoginError event
            axTrueConfCallX1.OnInviteReceived += AxTrueConfCallX1_OnInviteReceived; //handle OnInviteReceived event
            axTrueConfCallX1.OnXChangeState += AxTrueConfCallX1_OnXChangeState;
        }

        private void AxTrueConfCallX1_OnXChangeState(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXChangeStateEvent e)
        {
            switch (e.newState)
            {
                case 0:
                case 1:
                    toolStripStatusServer.Text = "Not connected";
                    toolStripStatusLogin.Text = "";
                    toolStripStatusConference.Text = "";
                    break;
                case 2:
                    toolStripStatusServer.Text = "Connected to " + ServerName; //show server status
                    toolStripStatusLogin.Text = "Not logged in";
                    toolStripStatusConference.Text = "";
                    break;
                case 3:
                    toolStripStatusLogin.Text = "You are logged in as " + TrueConf_Id; //show login status
                    toolStripStatusConference.Text = "Not in conference";
                    break;
                case 4:
                    toolStripStatusConference.Text = "Calling";
                    break;
                case 5:
                    toolStripStatusConference.Text = "In conference";
                    break;
                case 6:
                    toolStripStatusConference.Text = "Not in conference";
                    break;
            }
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
            {
                axTrueConfCallX1.accept(); //if user says "Yes" - accept incoming call                
            }
            else
            {
                axTrueConfCallX1.reject();// else - reject incoming call
            }
        }

        private void axTrueConfCallX1_OnServerConnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerConnectedEvent e) //notification about success connection to server
        {
            
            if (!String.IsNullOrEmpty(TrueConf_Id) && !String.IsNullOrEmpty(TrueConf_Password))
            {
                axTrueConfCallX1.login(TrueConf_Id, TrueConf_Password); //login on TrueConf Server with login and password
            }
        }
        private void axTrueConfCallX1_OnXLogin(object sender, EventArgs e) //notification about successed login
        {
            Enabled = true;
            for (int i = 0; i < axTrueConfCallX1.XGetCameraCount(); i++)
            {
                comboBoxCameras.Items.Add(axTrueConfCallX1.XGetCameraByIndex(i)); //get cameras and put them to combobox
            }

            for (int i = 0; i < axTrueConfCallX1.XGetMicCount(); i++)
            {
                comboBoxMicrophones.Items.Add(axTrueConfCallX1.XGetMicByIndex(i)); //get microphones and put them to combobox
            }

            for (int i = 0; i < axTrueConfCallX1.XGetSpeakerCount(); i++)
            {
                comboBoxSpeakers.Items.Add(axTrueConfCallX1.XGetSpeakerByIndex(i)); //get speakers and put them to combobox       
            }
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
            if (ServerName != null)
            {
                axTrueConfCallX1.connectToServer(ServerName); //after start connect to the server/service
            }
        }
        private void comboBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCameras.Items.Count > 0)
            {
                axTrueConfCallX1.XSelectCamera(comboBoxCameras.SelectedItem.ToString()); //set the camera that chosen in the comboBoxCameras
            }
        }
        private void comboBoxMicrophones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCameras.Items.Count > 0)
            {
                axTrueConfCallX1.XSelectMicByIndex(comboBoxMicrophones.SelectedIndex); //set the microphone that chosen in the comboBoxMicrophones
            }
        }
        private void comboBoxSpeakers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCameras.Items.Count > 0)
            {
                axTrueConfCallX1.XSelectSpeakerByIndex(comboBoxSpeakers.SelectedIndex); //set the speaker that chosen in the comboBoxSpeakers
            }
        }
        private void buttonCall_Click(object sender, EventArgs e)
        {
            if (textBoxTrueConfIdForCall.Text != "") //if trueconf id entered, and CallX is connected to server and authorized and isn't in call or conference
            {
                axTrueConfCallX1.call(textBoxTrueConfIdForCall.Text); //call the entered user
            }
        }
        private void buttonEndCall_Click(object sender, EventArgs e)
        {
            axTrueConfCallX1.hangUp();        //and set that the CallX can make the calls
        }
    }
}
