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
        public static string ID;
        public static string Password;
        private bool isCalling; //flag means the CallX is calling now
        private bool isConnected; //flag means the CallX is connected to server
        private bool isLoggedIn; //flag means the CallX is logged in
        private bool isInConference; //flag means the CallX is in conference
        internal static bool cancelAuth;

        public Form1()
        {  
            InitializeComponent();
            axTrueConfCallX1.OnXAfterStart += axTrueConfCallX1_OnXAfterStart; //handle OnXAfterStart event
            axTrueConfCallX1.OnInviteReceived += AxTrueConfCallX1_OnInviteReceived; //handle OnInviteReceived event
            axTrueConfCallX1.OnXChangeState += AxTrueConfCallX1_OnXChangeState; //handle OnXChangeState event (update app state)
        }

        private void AxTrueConfCallX1_OnXChangeState(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXChangeStateEvent e)
        {
            // Update connection status when it was changed
            // * 0 - no connection to server
            // * 1 - connecting to server
            // * 2 - connected to server, not logged in
            // * 3 - connected to server, logged in
            // * 4 - waiting: outgoing or incoming dialing 
            // * 5 - in conference
            // * 6 - conference is finishing
            switch (e.newState)
            {                
                case 2:
                    isConnected = true;
                    isLoggedIn = false;
                    isInConference = false;
                    if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(Password))
                        axTrueConfCallX1.login(ID, Password); //login on TrueConf Server with login and password
                    break;
                case 3:
                case 4:
                case 6:
                    isConnected = true;
                    isLoggedIn = true;
                    isInConference = false;
                    SetCallButtonAppearance("📞", Color.LimeGreen, Color.Green, Color.DarkGreen);
                    break;
                case 5:
                    isCalling = false;
                    isConnected = true;
                    isLoggedIn = true;
                    isInConference = true;
                    SetCallButtonAppearance("✖📞", Color.Red, Color.Maroon, Color.DarkRed);
                    break;
                case 0:
                case 1:
                default:
                    isConnected = false;
                    isLoggedIn = false;
                    isInConference = false;
                    break;
            }
            UpdateConnectionStatus();
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
        private void axTrueConfCallX1_OnXAfterStart(object sender, EventArgs e)
        {            
            for (int i = 0; i < axTrueConfCallX1.XGetCameraCount(); i++)
                chooseCameraToolStripMenuItem.DropDownItems.Add(axTrueConfCallX1.XGetCameraByIndex(i)); //get cameras and put them to combobox
            for (int i = 0; i < axTrueConfCallX1.XGetMicCount(); i++)
                chooseMicrophoneToolStripMenuItem.DropDownItems.Add(axTrueConfCallX1.XGetMicByIndex(i)); //get microphones and put them to combobox
            for (int i = 0; i < axTrueConfCallX1.XGetSpeakerCount(); i++)
                chooseSpreakersToolStripMenuItem.DropDownItems.Add(axTrueConfCallX1.XGetSpeakerByIndex(i)); //get speakers and put them to combobox    
            Auth auth = new Auth(); //open authorization dialog after form is initialized
            auth.FormClosed += Auth_FormClosed;
            auth.ShowDialog(); //open authorization dialog after form is initialized
        }

        private void Auth_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ServerName != null && !cancelAuth)
            {
                axTrueConfCallX1.connectToServer(ServerName); //after start connect to the server
            }
        }

        private void UpdateConnectionStatus()
        {
            //update app status and display it
            toolStripStatusServer.Text = "Not connected";
            toolStripStatusLogin.Text = "Not logged in";
            toolStripStatusConference.Text = "Not in conference";
            buttonCall.Enabled = isLoggedIn;
            if (isConnected && !isLoggedIn)
            {
                toolStripStatusServer.Text = "Connected to " + ServerName;
            }
            if (isLoggedIn && !isInConference)
            {
                toolStripStatusServer.Text = "Connected to " + ServerName;
                toolStripStatusLogin.Text = "Logged in as " + ID;
                
            }
            if (isInConference || isCalling)
            {
                toolStripStatusConference.Text = "In conference";
            }
        }

        private void SetCallButtonAppearance(string text, Color back, Color border, Color mouse)
        {
            buttonCall.Text = text;
            buttonCall.BackColor = back;
            buttonCall.FlatAppearance.BorderColor = border;
            buttonCall.FlatAppearance.MouseOverBackColor = buttonCall.FlatAppearance.MouseDownBackColor = mouse;
        }

        private void buttonCall_Click(object sender, EventArgs e)
        {
            if (isInConference || isCalling)
            {
                isCalling = false; //and set that the CallX can make the calls                
                axTrueConfCallX1.hangUp(); //end the call or stop calling                
            }
            else
            {
                if (textBoxTrueConfIdForCall.Text != "" && !isCalling && !isInConference) //if trueconf id entered, and CallX is connected to server and authorized and isn't in call or conference
                {
                    isCalling = true;
                    axTrueConfCallX1.call(textBoxTrueConfIdForCall.Text); //call the entered user                
                    textBoxTrueConfIdForCall.Clear(); //clear the textbox
                }
            }
            UpdateConnectionStatus();
        }

        private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auth auth = new Auth();
            auth.FormClosed += Auth_FormClosed;
            auth.ShowDialog();
        }

        private void ChooseCameraToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = chooseCameraToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem);
            axTrueConfCallX1.XSetCameraByIndex(i);
        }

        private void ChooseMicrophoneToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = chooseMicrophoneToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem);
            axTrueConfCallX1.XSelectMicByIndex(i);
        }

        private void ChooseSpreakersToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = chooseSpreakersToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem);
            axTrueConfCallX1.XSelectSpeakerByIndex(i);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
