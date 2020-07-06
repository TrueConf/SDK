using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using AxTrueConf_CallXLib;
using IniFiles;
namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AxTrueConfCallX tc;
        string logName;
        IniFile INI;
        bool inConference = false;
        TrueConf_CallXLib.CallX_FECC feccObject;
        //
        //private static System.Timers.Timer aTimer;
        System.Timers.Timer aTimer;

        int nTimerCount = 0;
        // 
        public MainWindow()
        {
            InitializeComponent();
            labelInfo.Content = "";
            //
            logName = AppDomain.CurrentDomain.BaseDirectory+ DateTime.Now.ToString("dd-MM-yy_HH-mm") + ".txt";
            //
            INI = new IniFile(AppDomain.CurrentDomain.BaseDirectory+"config.ini");
            // Create 'TrueConf' COM object
            tc = new AxTrueConfCallX();
            tcHost.Child = tc;
            //
            tc.OnXAfterStart += Tc_OnXAfterStart;           //setting the OnXAfterStart event handler
            tc.OnServerConnected += Tc_OnServerConnected;   //setting OnServerConnected event handler
            tc.OnXLogin += Tc_OnXLogin;                     //setting OnXLogin event handler
            tc.OnXLoginError += Tc_OnXLoginError;             //setting OnXLoginError event handler
            tc.OnConferenceCreated += Tc_OnConferenceCreated; //setting OnConferenceCreated event handler
            tc.OnConferenceDeleted += Tc_OnConferenceDeleted; //setting OnConferenceDeleted event handler
            tc.OnInviteReceived += Tc_OnInviteReceived;     //setting OnInviteReceived event handler
            tc.OnRejectReceived += Tc_OnRejectReceived;     //setting OnRejectReceived event handler
            tc.OnFECCControl += Tc_OnFECCControl;           //setting OnFECCControl event handler
            tc.OnXNotify += Tc_OnXNotify;                   //setting OnXNotify event handler
            tc.OnXChangeState += Tc_OnXChangeState;         //setting OnXChangeState event handler
            // Set visible state UI elements
            button_Call.Visibility = Visibility.Hidden;
            labelFecc.Visibility = Visibility.Hidden;
            button_Left.Visibility = Visibility.Hidden;
            button_Right.Visibility = Visibility.Hidden;
            button_Up.Visibility = Visibility.Hidden;
            button_Down.Visibility = Visibility.Hidden;
            button_ZoomIn.Visibility = Visibility.Hidden;
            button_ZoomOut.Visibility = Visibility.Hidden;
            //
        }


        //------------- Tc_OnXAfterStart -------------//
        //Executed after 'Trueconf' com object started
        private void Tc_OnXAfterStart(object sender, EventArgs e)
        {
            WriteLog("Tc_OnXAfterStart()...");

            try
            {
                string sServer = INI.ReadINI("connection", "server");
                WriteLog("connectToServer: " + sServer);
                //Try connect to server
                tc.connectToServer(sServer);
                //Use camera
                string sCam = INI.ReadINI("connection", "camera_id");
                int idCam = System.Convert.ToInt32(sCam);
                WriteLog(String.Format("SetCameraByIndex( {0} )", idCam));
                // Try XSetCameraByIndex
                if (idCam >= 0 && idCam < 100) tc.XSetCameraByIndex(idCam);
            }
            catch
            {
                WriteLog(String.Format("EXCEPTION !!! : connectToServer !!!"));
            }
        }

        //------------------ Tc_OnServerConnected -------------------//
        // Server connected
        private void Tc_OnServerConnected(object sender, _ITrueConfCallXEvents_OnServerConnectedEvent e)
        {
            WriteLog(String.Format("Info : {0} : Connect to server success.", DateTime.Now.ToString("g")));
            // 
            string sUser = INI.ReadINI("connection", "user");
            string sPwd = INI.ReadINI("connection", "password");
            WriteLog("Try login (as user: " + sUser + ", pwd: " + sPwd + ")");
            // Try login
            tc.login(sUser, sPwd);
        }
        //------------------ Tc_OnXLoginError -------------------//
        // Login failed
        private void Tc_OnXLoginError(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXLoginErrorEvent e)
        {
            WriteLog(String.Format("ERROR : {0} : Login failed.", DateTime.Now.ToString("g")));
            labelInfo.Content = "Login Failed";
        }
        //------------------ Tc_OnXLogin -------------------//
        // Sussess login
        private void Tc_OnXLogin(object sender, EventArgs e)
        {
            WriteLog(String.Format("Info : {0} : Login success.", DateTime.Now.ToString("g")));
            labelInfo.Content = "Login success";
            //
            button_Call.Visibility = Visibility.Visible;
        }

        //--------------------- Button_Click_Call ------------------//
        // Call to remote user
        private void Button_Click_Call(object sender, RoutedEventArgs e)
        {
            if (!inConference)
            {   //call the user
                string sUser = INI.ReadINI("connection", "call_to");
                WriteLog(String.Format("Info : {0} : Call to {1}", DateTime.Now.ToString("g"), sUser));
                labelInfo.Content = "Call ...";
                // Try to call...
                tc.call(sUser);
            }
            else
            {   // finish the call or leave the conference
                WriteLog(String.Format("hangUp : {0} ", DateTime.Now.ToString("g")));
                labelInfo.Content = "hangUp ...";
                // Try hangUp
                tc.hangUp();
            }
        }
        //------------------------ Tc_OnConferenceCreated ---------------------//
        // Conference (call) is started
        private void Tc_OnConferenceCreated(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceCreatedEvent e)
        {
            inConference = true;
            WriteLog(String.Format("Info : {0} : ConferenceCreated !!!", DateTime.Now.ToString("g")));
            labelInfo.Content = "ConferenceCreated !!!";
            button_Call.Content = "End call";
        }
        //------------------------ Tc_OnConferenceDeleted ---------------------//
        // Conference (call) is ended
        private void Tc_OnConferenceDeleted(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceDeletedEvent e)
        {
            inConference = false;
            feccObject = null;
            //
            WriteLog(String.Format("Info : {0} : ConferenceDeleted !!!", DateTime.Now.ToString("g")));
            labelInfo.Content = "ConferenceDeleted !!!";
            button_Call.Content = "Call to user";
            // Set visible state UI elements
            labelFecc.Visibility = Visibility.Hidden;
            button_Left.Visibility = Visibility.Hidden;
            button_Right.Visibility = Visibility.Hidden;
            button_Up.Visibility = Visibility.Hidden;
            button_Down.Visibility = Visibility.Hidden;
            button_ZoomIn.Visibility = Visibility.Hidden;
            button_ZoomOut.Visibility = Visibility.Hidden;
        }
        //------------------------ Tc_OnRejectReceived ----------------------//
        // Reject received
        private void Tc_OnRejectReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnRejectReceivedEvent e)
        {
            WriteLog(String.Format("Info : {0} : RejectReceived !!!", DateTime.Now.ToString("g")));
            labelInfo.Content = "Call is rejected";
        }
        //------------------------ Tc_OnInviteReceived ---------------------//
        // Invite received
        private void Tc_OnInviteReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnInviteReceivedEvent e)
        {
            WriteLog(String.Format("Info : {0} : InviteReceived !!!", DateTime.Now.ToString("g")));
            labelInfo.Content = "Invite is received";
        }
        //-------------------- Tc_OnFECCControl --------------------//
        // Remote user alloy/disable FECC controling
        private void Tc_OnFECCControl(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnFECCControlEvent e)
        {
            WriteLog(String.Format("Info : {0} : OnFECCControl Received !!!", DateTime.Now.ToString("g")));
            labelInfo.Content = "OnFECCControl is received";
            //
            feccObject = e.p as TrueConf_CallXLib.CallX_FECC;
            if (feccObject is null)
            {
                WriteLog(String.Format("WARNINF FECCControl Rejected !!!" ));
                labelInfo.Content = "FECCControl is Rejected";
                return;
            }
            // Set visible state UI elements
            labelFecc.Visibility = Visibility.Visible;
            button_Left.Visibility = Visibility.Visible;
            button_Right.Visibility = Visibility.Visible;
            button_Up.Visibility = Visibility.Visible;
            button_Down.Visibility = Visibility.Visible;
            button_ZoomIn.Visibility = Visibility.Visible;
            button_ZoomOut.Visibility = Visibility.Visible;
        }

        //--------------------- Tc_OnXNotify ----------------//
        // Notify events
        private void Tc_OnXNotify(object sender, _ITrueConfCallXEvents_OnXNotifyEvent e)
        {
            //WriteLog(String.Format("Info : {0} : {1}", DateTime.Now.ToString("g"), e.data));
        }

        //----------------------- Tc_OnXChangeState -------------------//
        // Application state changed
        private void Tc_OnXChangeState(object sender, _ITrueConfCallXEvents_OnXChangeStateEvent e)
        {
            int new_state = e.newState;
            WriteLog(String.Format("OnXChangeState : {0} : {1}", DateTime.Now.ToString("g"), new_state));
            
            // State(5) is 'conference mode'
            if (feccObject is null && new_state == 5)
            {
                // Request FECC control
                string sUser = INI.ReadINI("connection", "call_to");
                WriteLog(String.Format("GetFECCControl() : {0} / {1}", DateTime.Now.ToString("g"), sUser));
                labelInfo.Content = "GetFECCControl ...";
                // Try GetFECCControl
                tc.GetFECCControl(sUser);
            }
        }

        //------------- WriteLog ----------------//
        public void WriteLog(string msg)
        {
            using (StreamWriter sw = new StreamWriter(logName, true))
            {
                sw.WriteLine(msg);
            }
        }

        // //////////////// FECC Controllling ////////////////////
        //-------------------- Button_Click_ZoomIn --------------//
        // FECC / Control remote camera > Zoom in
        private void Button_Click_ZoomIn(object sender, RoutedEventArgs e)
        {
            if (feccObject is null) return;
            //
            WriteLog(String.Format("Info : {0} : ZoomIn() ", DateTime.Now.ToString("g")));
            labelInfo.Content = "OnFECC ZoomIn()";
            //
            feccObject.ZoomInc();
        }
        //-------------------- Button_Click_ZoomOut --------------//
        // FECC / Control remote camera > Zoom out
        private void Button_Click_ZoomOut(object sender, RoutedEventArgs e)
        {
            if (feccObject is null) return;
            //
            WriteLog(String.Format("Info : {0} : ZoomOut() ", DateTime.Now.ToString("g")));
            labelInfo.Content = "OnFECC ZoomOut()";
            //
            feccObject.ZoomOut();
        }
        //-------------------- Button_Click_Up --------------//
        // FECC / Control remote camera > Pan up
        private void Button_Click_Up(object sender, RoutedEventArgs e)
        {
            if (feccObject is null) return;
            //
            WriteLog(String.Format("Info : {0} : Up() ", DateTime.Now.ToString("g")));
            labelInfo.Content = "OnFECC Up()";
            //
            feccObject.Up();
        }
        //-------------------- Button_Click_Down --------------//
        // FECC / Control remote camera > Pan down
        private void Button_Click_Down(object sender, RoutedEventArgs e)
        {
            if (feccObject is null) return;
            //
            WriteLog(String.Format("Info : {0} : Diwn() ", DateTime.Now.ToString("g")));
            labelInfo.Content = "OnFECC Down()";
            //
            feccObject.Down();
        }
        //-------------------- Button_Click_Left --------------//
        // FECC / Control remote camera > Pan left
        private void Button_Click_Left(object sender, RoutedEventArgs e)
        {
            if (feccObject is null) return;
            //
            WriteLog(String.Format("Info : {0} : Left() ", DateTime.Now.ToString("g")));
            labelInfo.Content = "OnFECC Left()";
            //
            feccObject.Left();
        }
        //-------------------- Button_Click_Right --------------//
        // FECC / Control remote camera > Pan right
        private void Button_Click_Right(object sender, RoutedEventArgs e)
        {
            if (feccObject is null) return;
            //
            WriteLog(String.Format("Info : {0} : Right() ", DateTime.Now.ToString("g")));
            labelInfo.Content = "OnFECC Right()";
            //
            feccObject.Right();
        }

    }
}
