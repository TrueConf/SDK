using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCallUserByIdExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AxTrueConf_CallXLib.AxTrueConfCallX sdk; //the TrueConf SDK control
        private int status; //current appstate
        private static List<string> statuses = new List<string>() //list of app states
        {
            "no connection", //=0
            "connecting to server", //=1
            "please log in", //=2
            "logged in", //=3
            "in call", //=4
            "in conference", //=5
            "finishing the conference" //=6
        };

        private List<string> cameras = new List<string>(); //list of cameras
        private List<string> mics = new List<string>(); //list of microphones
        private List<string> speakers = new List<string>(); //list of speakers

        private int camera; //current index of the camera
        private int mic; //current index of the microphone
        private int speaker; //current index of the speaker
         
        private bool isStarted = false; //TrueConf SDK control starting flag

        private DateTime start; //variable for conference timer
        private Timer timer; //conference timer

        public MainWindow()
        {
            InitializeComponent();
            sdk = new AxTrueConf_CallXLib.AxTrueConfCallX(); //creating new TrueConf SDK control
            sdk.OnXAfterStart += Sdk_OnXAfterStart; //setting the OnXAfterStart event handler
            sdk.OnServerConnected += Sdk_OnServerConnected; //setting OnServerConnected event handler
            sdk.OnServerDisconnected += Sdk_OnServerDisconnected; //setting OnServerDisconnected event handler
            sdk.OnXLogin += Sdk_OnXLogin; //setting OnXLogin event handler
            sdk.OnLogout += Sdk_OnLogout; //setting OnLogout event handler
            sdk.OnXLoginError += Sdk_OnXLoginError; //setting OnXLoginError event handler
            sdk.OnInviteReceived += Sdk_OnInviteReceived; //setting OnInviteReceived event handler
            sdk.OnRejectReceived += Sdk_OnRejectReceived; //setting OnRejectReceived event handler
            sdk.OnConferenceCreated += Sdk_OnConferenceCreated; //setting OnConferenceCreated event handler
            sdk.OnConferenceDeleted += Sdk_OnConferenceDeleted; //setting OnConferenceDeleted event handler
            sdk.OnHardwareChanged += Sdk_OnHardwareChanged; //setting OnHardwareChanged event handler
            sdk.OnXNotify += Sdk_OnXNotify; //setting OnXNotify event handler
            sdkHost.Child = sdk; //putting the TrueConf SDK control into the WindowsFormsHost control

            camera = 0; //setting current chosen index of the camera from combobox
            mic = 0; //setting current chosen index of the microphone from combobox
            speaker = 0; //setting current chosen index of the speaker from combobox

            timer = new Timer(); //create conference timer
            timer.Interval = 1000; //set the interval for conference timer
            timer.Tick += Timer_Tick; //set timer Tick event handler

            btnCall.IsEnabled = btnConnect.IsEnabled = false; //disabling call and connect buttons
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //setting content of the call status label (call/conference timer)
            lblCallStatus.Content = "Звонок " + (DateTime.Now.TimeOfDay - start.TimeOfDay).ToString(@"hh\:mm\:ss");
        }

        private void Sdk_OnXNotify(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXNotifyEvent e)
        {
            if(e.data.Contains("getAppState")) //checking that received notification contains result for getAppState
            {
                dynamic json = JsonConvert.DeserializeObject(e.data); //converting received JSON object
                status = json.appState; //saving app state
                lblStatus.Content = statuses.ElementAt(status); //setting content of the status label
                SetColorStatus(); //setting the color of the status label
                switch (status) //disabling the call button when the user is not logged in (app state <3) and enabling after logging in (app state> 2)
                {
                    case 1:
                    case 2:
                        btnCall.IsEnabled = false;
                        btnConnect.IsEnabled = true;
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        btnCall.IsEnabled = true;
                        btnConnect.IsEnabled = true;
                        break;
                }
            }
        }

        private void SetColorStatus()
        {
            switch(status)
            {
                case 0: //no connection
                case 1: //connecting to server
                    lblStatus.Background = new SolidColorBrush(Colors.Gray); //setting the gray color of the status label
                    break;
                case 2: //connected to server
                    lblStatus.Background = new SolidColorBrush(Colors.Red); //setting the red color of the status label
                    break;
                case 3: //logged in
                    lblStatus.Background = new SolidColorBrush(Colors.Green); //setting the green color of the status label
                    break;
                case 4: //in call
                case 5: //in conference
                    lblStatus.Background = new SolidColorBrush(Colors.Orange); //settig the orange color of the status label
                    break;
                case 6: //finishing the conference
                    lblStatus.Background = new SolidColorBrush(Colors.Green); //setting the green color of the status label
                    break;
            }
        }

        private void Sdk_OnLogout(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnLogoutEvent e)
        {
            sdk.getAppState(); //get the app state changing
            btnCall.IsEnabled = false; //disable the call button
        }

        private void UpdateHardware()
        {
            cameras.Clear(); //clear list of cameras 
            speakers.Clear(); //clear list of speakers
            mics.Clear(); //clear list of microphones
            for (int i = 0; i < sdk.XGetCameraCount(); i++) //getting list of cameras
                cameras.Add(sdk.XGetCameraByIndex(i));
            cameras.Add("none"); //disabling camera
            for (int i = 0; i < sdk.XGetMicCount(); i++) //getting list of microphones
                mics.Add(sdk.XGetMicByIndex(i));
            mics.Add("none"); //disabling microphone
            for (int i = 0; i < sdk.XGetSpeakerCount(); i++) //getting list of speakers
                speakers.Add(sdk.XGetSpeakerByIndex(i));
            speakers.Add("none"); //disabling speaker

            cbCamera.ItemsSource = cameras; //putting the list of cameras to the combobox
            cbMic.ItemsSource = mics; //putting the list of microphones to the combobox
            cbSpeaker.ItemsSource = speakers; //putting the list of speakers to the combobox

            cbCamera.SelectedIndex = camera; //setting current chosen index of the camera from combobox
            cbMic.SelectedIndex = mic; //setting current chosen index of the microphone from combobox
            cbSpeaker.SelectedIndex = speaker; //setting current chosen index of the speaker from combobox
        }

        private void Sdk_OnHardwareChanged(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnHardwareChangedEvent e)
        {
            UpdateHardware(); //update the hardware lists
        }

        private void Sdk_OnConferenceDeleted(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceDeletedEvent e)
        {
            sdk.getAppState(); //get the app state changing
            timer.Stop(); //stop the conference timer
            lblCallStatus.Content = "Call is ended"; //change the content of the call status label
            btnCall.Content = "Call"; //change the content of the call button
        }

        private void Sdk_OnConferenceCreated(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceCreatedEvent e)
        {
            sdk.getAppState(); //get the app state changing
            timer.Start(); //start the conference timer
            start = DateTime.Now; //save current time
            btnCall.Content = "End call"; //change the content of the call button
        }

        private void Sdk_OnRejectReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnRejectReceivedEvent e)
        {
            lblCallStatus.Content = "Call is rejected"; //change the content of the call status label
        }

        private void Sdk_OnInviteReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnInviteReceivedEvent e)
        {
            if (status != 4 && status != 5) //if app state is not in call or in conference
            {
                sdk.accept(); //accept incoming invitation
                lblCallStatus.Content = "Calling..."; //change the content of the call status label
                dynamic json = JsonConvert.DeserializeObject(e.eventDetails); //gconvert the received JSON object
                txtCallId.Text = json.peerId; //put received peerId to the call textbox
            }
        }

        private void Sdk_OnXLoginError(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXLoginErrorEvent e)
        {
            sdk.getAppState(); //get the app state changing
        }

        private void Sdk_OnXLogin(object sender, EventArgs e)
        {
            sdk.getAppState(); //get the app state changing
            btnCall.IsEnabled = true; //enabing the call button
        }

        private void Sdk_OnServerDisconnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerDisconnectedEvent e)
        {
            sdk.getAppState(); //get the app state changing
        }

        private void Sdk_OnServerConnected(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerConnectedEvent e)
        {
            sdk.getAppState(); //get the app state changing
            sdk.login(txtLogin.Text, txtPass.Password); //logging in
        }

        private void Sdk_OnXAfterStart(object sender, EventArgs e)
        {
            isStarted = true; //set TrueConf SDK control starting flag
            sdk.getAppState(); //get the app state changing
            UpdateHardware(); //update the hardware lists
            btnConnect.IsEnabled = true; //enable the connect button

            { //Set the selected camera in the TrueConf SDK
                if (camera == cameras.Count - 1 || camera < 0)
                    sdk.XDeselectCamera();
                else
                    sdk.XSetCameraByIndex(camera);
            }
            { //Set the selected microphone in the TrueConf SDK
                if (mic == mics.Count - 1 || mic < 0)
                    sdk.XDeselectMic();
                else
                    sdk.XSelectMicByIndex(mic);
            }
            { //Set the selected speaker in the TrueConf SDK
                if (speaker == speakers.Count - 1 || speaker < 0)
                    sdk.XDeselectSpeaker();
                else
                    sdk.XSelectSpeakerByIndex(speaker);
            }
        }

        //selection change event handler for the camera combobox
        private void cbCamera_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            camera = cbCamera.SelectedIndex; //save the selected index of camera
            if (isStarted) //if TrueConf SDK is started
            { //Set the selected camera in the TrueConf SDK
                if (camera == cameras.Count - 1 || camera < 0)
                    sdk.XDeselectCamera();
                else
                    sdk.XSetCameraByIndex(camera);
            }
                 
        }

        //selection change event handler for the microphone combobox
        private void cbMic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mic = cbMic.SelectedIndex; //save the selected index of microphone
            if (isStarted) //if TrueConf SDK is started
            { //Set the selected microphone in the TrueConf SDK
                if (mic == mics.Count - 1 || mic < 0)
                    sdk.XDeselectMic();
                else
                    sdk.XSelectMicByIndex(mic);
            }
        }

        //selection change event handler for the speaker combobox
        private void cbSpeaker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            speaker = cbSpeaker.SelectedIndex; //save the selected index of speaker
            if (isStarted)  //if TrueConf SDK is started
            { //Set the selected speaker in the TrueConf SDK
                if (speaker == speakers.Count - 1 || speaker < 0)
                    sdk.XDeselectSpeaker();
                else
                    sdk.XSelectSpeakerByIndex(speaker);
            }
        }

        //the click event handler for the connect button
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (status == 4 || status == 5) //if app state is in call or in conference
                sdk.hangUp(); //finish the call or leave the conference
            if (txtLogin.Text.Length > 0 && //if the login field is not empty and
                txtPass.Password.Length > 0) //the password field is not empty
            {
                sdk.connectToServer(txtServer.Text); //connect to the server
            }
            else
                lblConnectStatus.Content = "Some fields are empty"; //otherwise show the error message in the connect status label
        }

        //the click event handler for the call button
        private void btnCall_Click(object sender, RoutedEventArgs e)
        {
            if (status != 4 && status != 5) //if app state is not in call or in conference
            {
                if (txtCallId.Text.Length > 0) //if the call id field is not empty
                {
                    sdk.call(txtCallId.Text); //call the user
                    sdk.getAppState(); //get the app state changing
                    lblCallStatus.Content = "Calling..."; //change the content of the call status label
                }
                else
                {
                    lblCallStatus.Content = "User ID field is empty"; //otherwise show the error messge in the call status label
                }
                
            }
            else //otherwise
            {
                sdk.hangUp(); //finish the call or leave the conference
                sdk.getAppState(); //get the app state changing
            }
        }
    }
}
