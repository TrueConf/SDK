using AxTrueConf_CallXLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RemotePrintPdfClient
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        private AxTrueConfCallX sdk; //sdk object

        private MainModel mainModel; //data for displaying in main window

        private bool iSConnected; //field that indicate connection state
        private bool isLoggedIn; //field that indicate login state
        private bool isInCall; //field that indicate call state

        private Timer timer; //for dispaying call time
        private DateTime start; //for dispaying call time

        private string server;
        private string iD;
        private string password;
        private bool isStarted;

        public MainPage()
        {
            InitializeComponent();
            sdk = new AxTrueConfCallX(); // init sdk object
            mainModel = new MainModel
            {
                Abook = new List<string>() //will contain address book for auto-filling in user IDs
            };
            sdkHost.Child = sdk; //put sdk into WindowsFormsHost control
            InitSDKEvents(); // set sdk events handlers      
            DataContext = mainModel;

            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
        }

        internal void SetLoginData(string server, string iD, string password)
        {
            this.server = server;
            this.iD = iD;
            this.password = password;
            if (!string.IsNullOrEmpty(server) && isStarted)
            {
                sdk.connectToServer(server);
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            mainModel.CallTime = new DateTime(DateTime.Now.Subtract(start).Ticks).ToString("HH:mm:ss");
        }

        private void InitSDKEvents()
        {
            //handle sdk events
            sdk.OnXAfterStart += Sdk_OnXAfterStart;
            sdk.OnServerConnected += Sdk_OnServerConnected;
            sdk.OnServerDisconnected += Sdk_OnServerDisconnected;
            sdk.OnXLogin += Sdk_OnXLogin;
            sdk.OnXLoginError += Sdk_OnXLoginError;
            sdk.OnInviteReceived += Sdk_OnInviteReceived;
            sdk.OnConferenceCreated += Sdk_OnConferenceCreated;
            sdk.OnConferenceDeleted += Sdk_OnConferenceDeleted;
            sdk.OnXFileSend += Sdk_OnXFileSend;
            sdk.OnXFileSendError += Sdk_OnXFileSendError;
            sdk.OnXNotify += Sdk_OnXNotify;
            sdk.OnSlideShowInfoUpdate += Sdk_OnSlideShowInfoUpdate;
            sdk.OnAbookUpdate += Sdk_OnAbookUpdate;
        }

        private void Sdk_OnAbookUpdate(object sender, _ITrueConfCallXEvents_OnAbookUpdateEvent e)
        {
            sdk.getAbook();
        }

        private void Sdk_OnSlideShowInfoUpdate(object sender, _ITrueConfCallXEvents_OnSlideShowInfoUpdateEvent e)
        {
            //put the slides in the list when updating slideshow information
            listSlides.Items.Clear();
            dynamic json = JsonConvert.DeserializeObject(e.eventDetails);
            if (json.slides != null)
            {
                foreach (dynamic slide in json.slides)
                {
                    listSlides.Items.Add((string)slide.name);
                }
            }
        }

        private void GetAbook(string json)
        {
            //put the address book in the list when you receive address book in onxnotify event handler
            List<string> list = new List<string>();
            dynamic data = JsonConvert.DeserializeObject(json);
            if (data.abook != null)
            {
                foreach (dynamic user in data.abook)
                {
                    list.Add((string)user.peerId);
                }
            }

            mainModel.Abook = list;
        }

        private void Sdk_OnXNotify(object sender, _ITrueConfCallXEvents_OnXNotifyEvent e)
        {
            //handle onxnotify event
            dynamic data = JsonConvert.DeserializeObject(e.data);
            switch ((string)data.method)
            {
                case "getAbook":
                    GetAbook(e.data); //if get address book, process it
                    break;
                case "startSlideShow":
                    if (!(bool)data.result)
                    {
                        ShowMessage("Could not start slideshow, error: " + (string)data.error); //show error when the command execution failed
                    }

                    break;
                case "stopSlideShow":
                    if (!(bool)data.result)
                    {
                        ShowMessage("Could not stop slideshow, error: " + (string)data.error); //show error when the command execution failed
                    }

                    break;
                case "nextSlide":
                    if (!(bool)data.result)
                    {
                        ShowMessage("Failed to show next slide, error: " + (string)data.error); //show error when the command execution failed
                    }

                    break;
                case "prevSlide":
                    if (!(bool)data.result)
                    {
                        ShowMessage("Failed to show prev slide, error: " + (string)data.error); //show error when the command execution failed
                    }

                    break;
                default:
                    Debug.WriteLine(e.data.ToString(), "TrueConf"); //write the SDK log to the debug output
                    break;
            }
        }

        private void ShowMessage(string text)
        {
            MessageBox.Show(text, "TrueConf", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Sdk_OnXFileSendError(object sender, _ITrueConfCallXEvents_OnXFileSendErrorEvent e)
        {
            ShowMessage("Error while sending file " + e.fileCaption + ", error code " + e.error_code); //show error when sending file failed
        }

        private void Sdk_OnXFileSend(object sender, _ITrueConfCallXEvents_OnXFileSendEvent e)
        {
            if (txtCallID.Text.Length > 0)
            {
                sdk.XRemotePrintPdf(txtCallID.Text, e.fileId); //after sending file - print it remote
            }
        }

        private void Sdk_OnConferenceDeleted(object sender, _ITrueConfCallXEvents_OnConferenceDeletedEvent e)
        {
            isInCall = false;
            timer.Stop(); //clear timer
            mainModel.PeerID = null; //clear call interlocutor ID
            mainModel.CallTime = null; //clear call time
            UpdateStatus();
        }

        private void Sdk_OnConferenceCreated(object sender, _ITrueConfCallXEvents_OnConferenceCreatedEvent e)
        {
            isInCall = true;
            dynamic data = JsonConvert.DeserializeObject(e.eventDetails);
            if (mainModel.PeerID == null)
            {
                mainModel.PeerID = data.peerId != null ? (string)data.peerId : data.confTitle; //get call interlocutor ID
            }

            start = DateTime.Now; //get start call time
            timer.Start(); //start timer
            UpdateStatus();
        }

        private void Sdk_OnInviteReceived(object sender, _ITrueConfCallXEvents_OnInviteReceivedEvent e)
        {
            sdk.accept(); //accept incoming calls and invitations
            dynamic data = JsonConvert.DeserializeObject(e.eventDetails);
            mainModel.PeerID = (string)data.peerId; //get call interlocutor  ID
            mainModel.SelectedID = mainModel.PeerID;
        }

        private void Sdk_OnXLoginError(object sender, _ITrueConfCallXEvents_OnXLoginErrorEvent e)
        {
            //show error when login failed
            ShowMessage("Login failed, invalid login details, please, check entered id and password and try again");
            isLoggedIn = false;
            UpdateStatus();
            NavigateBack();
        }

        private void Sdk_OnXLogin(object sender, EventArgs e)
        {
            isLoggedIn = true;
            sdk.getAbook();
            UpdateStatus();
        }

        private void Sdk_OnServerDisconnected(object sender, _ITrueConfCallXEvents_OnServerDisconnectedEvent e)
        {//show error when unable to connect to server
            ShowMessage("Unable to connect to server, please, check entered server or try another");
            iSConnected = false;
            isLoggedIn = false;
            isInCall = false;
            UpdateStatus();
            NavigateBack();
        }

        private void NavigateBack()
        {
            NavigationWindow nav = (NavigationWindow)Window.GetWindow(this);
            nav.GoBack();
        }

        private void Sdk_OnServerConnected(object sender, _ITrueConfCallXEvents_OnServerConnectedEvent e)
        {
            iSConnected = true;
            isLoggedIn = false;
            isInCall = false;
            if (iD != null && password != null)
            {
                sdk.login(iD, password); //login after connecting to server
            }

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            //show status in the status bar
            if (iSConnected)
            {
                mainModel.Status = "Connected to server " + server;
            }
            else
            {
                mainModel.Status = "Not connected to server";
            }

            if (isLoggedIn)
            {
                mainModel.Status += ", logged in as " + iD;
            }
            else
            if (isInCall)
            {
                mainModel.Status += ", in conference";
            }
        }

        private void Sdk_OnXAfterStart(object sender, EventArgs e)
        {
            //init sdk
            sdk.XSetCameraByIndex(0);
            sdk.XSelectMicByIndex(0);
            sdk.XSelectSpeakerByIndex(0);
            UpdateStatus();
            isStarted = true;
            if (!string.IsNullOrEmpty(server))
            {
                sdk.connectToServer(server);
            }
        }

        private void BtnCall_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted && isLoggedIn)
            {
                //start and finish calls
                switch (btnCall.Content.ToString().ToLower())
                {
                    case "call":
                        if (!isInCall && txtCallID.Text.Length > 0)
                        {
                            sdk.call(txtCallID.Text);
                            btnCall.Content = "END CALL";
                        }
                        break;
                    case "end call":
                        sdk.hangUp();
                        btnCall.Content = "CALL";
                        break;
                }
            }
        }

        private void BtnRemotePrint_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted && isLoggedIn)
            {
                //send file for remote print pdf
                if (txtCallID.Text.Length > 0 && txtFile.Content.ToString().Length > 0)
                {
                    sdk.XFileSend(txtCallID.Text, mainModel.FileName, System.IO.Path.GetFileName(mainModel.FileName));
                }
            }
        }

        private void BtnAddSlides_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted && isLoggedIn)
            {
                //add slides
                sdk.XAddSlidesDialog();
            }
        }

        private void BtnClearSlides_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted && isLoggedIn)
            {
                //clear slides
                for (int i = listSlides.Items.Count - 1; i >= 0; i--)
                {
                    sdk.XRemoveSlide(i);
                }
            }
        }

        private void BtnStartSlideShow_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted && isInCall)
            {
                //start slideshow
                sdk.startSlideShow("pesentation");
            }
        }

        private void BtnStopSlideShow_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted && isInCall)
            {
                //stop slideshow
                sdk.stopSlideShow();
            }
        }

        private void BtnNextSlide_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted && isInCall)
            {
                //show next slide
                sdk.nextSlide();
            }
        }

        private void BtnPrevSlide_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted && isInCall)
            {
                //show prev slide
                sdk.prevSlide();
            }
        }

        private void LblFile_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted && isLoggedIn)
            {
                //open pdf file for remote printing pdf
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog
                {
                    Multiselect = false,
                    CheckFileExists = true,
                    DefaultExt = "pdf",
                    Filter = "Pdf files (*.pdf)|*.pdf"
                };
                System.Windows.Forms.DialogResult res = dialog.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    mainModel.FileName = dialog.FileName;
                }
            }
        }
    }
}
