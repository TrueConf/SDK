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
    public partial class Form1 : Form
    {
        public static string ServerName;
        public static string TrueConf_Id;
        public static string TrueConf_Password;
        private bool isCanCall = false; //flag means the CallX can make calls
        private bool started = false; //flag means that TrueConf CallX started
        public Form1()
        {
            Auth auth = new Auth(); //open authorization dialog after form is initialized
            auth.ShowDialog(); //open authorization dialog after form is initialized        
            InitializeComponent();                
        }
        private void axTrueConfCallX1_OnServerConnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerConnectedEvent e) //notification about success connection to server
        {
            MessageBox.Show("Connect to server ok!");
            if(!String.IsNullOrEmpty(TrueConf_Id) && !String.IsNullOrEmpty(TrueConf_Password))
                axTrueConfCallX1.login(TrueConf_Id, TrueConf_Password); //login on TrueConf Server with login and password
        }
        private void axTrueConfCallX1_OnXLoginOk(object sender, EventArgs e) //notification about successed login
        {
            MessageBox.Show("Login ok!"); //show message about successed login
            isCanCall = true; //flag means the CallX can make calls
            for (int i = 0; i < axTrueConfCallX1.XGetCameraCount(); i++)
                comboBoxCameras.Items.Add(axTrueConfCallX1.XGetCameraByIndex(i)); //get cameras and put them to combobox
            for (int i = 0; i < axTrueConfCallX1.XGetMicCount(); i++)
                comboBoxMicrophones.Items.Add(axTrueConfCallX1.XGetMicByIndex(i)); //get microphones and put them to combobox
            for (int i = 0; i < axTrueConfCallX1.XGetSpeakerCount(); i++)
                comboBoxSpeakers.Items.Add(axTrueConfCallX1.XGetSpeakerByIndex(i)); //get speakers and put them to combobox       
        }
        private void axTrueConfCallX1_OnXLoginError(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXLoginErrorEvent e)
        {
            MessageBox.Show("Login error!"); //show message about error login
            Auth auth = new Auth(); //open authorization dialog again
            auth.ShowDialog();
        }
        private void axTrueConfCallX1_OnXAfterStart(object sender, EventArgs e)
        {
            started = true; //flag means TrueConf CallX started
            if(ServerName != null)
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
            if (textBoxTrueConfIdForCall.Text != "" && isCanCall) //if trueconf id entered, and CallX is connected to server and authorized and isn't in call or conference
            {
                axTrueConfCallX1.call(textBoxTrueConfIdForCall.Text); //call the entered user                
                textBoxTrueConfIdForCall.Text = ""; //clear the textbox
            }
        }
        private void buttonEndCall_Click(object sender, EventArgs e)
        {
            if (!isCanCall) //if Callx is in call
            {
                axTrueConfCallX1.hangUp(); //end the call
                isCanCall = true; //and set that the CallX can make the calls
            }
        }
        
        private void axTrueConfCallX1_OnConferenceCreated(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceCreatedEvent e)
        {
            isCanCall = false; //set that CallX cannot make calls
        }
    }
}
