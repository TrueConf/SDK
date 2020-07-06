using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using IniFiles;
using AxTrueConf_CallXLib;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AxTrueConfCallX tc;
        string logName;
        IniFile INI;
        public MainWindow()
        {
            InitializeComponent();
            //
            labelInfo.Content = "";
            //
            logName = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("dd-MM-yy_HH-mm") + ".txt";
            //
            INI = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "config.ini");
            // Create 'TrueConf' COM object
            tc = new AxTrueConfCallX();
            tcHost.Child = tc;
            //
            tc.OnXAfterStart += Tc_OnXAfterStart;           //setting the OnXAfterStart event handler
            tc.OnServerConnected += Tc_OnServerConnected;   //setting OnServerConnected event handler
            tc.OnXLogin += Tc_OnXLogin;                     //setting OnXLogin event handler
            tc.OnXLoginError += Tc_OnXLoginError;           //setting OnXLoginError event handler
            tc.OnInviteReceived += Tc_OnInviteReceived;     //setting OnInviteReceived event handler
            tc.OnFECCRequest += Tc_OnFECCRequest;           //setting OnFECCRequest event handler
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
                if(idCam>=0 && idCam<100) tc.XSetCameraByIndex(idCam);
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
            labelInfo.Content = "Login failed";
            WriteLog(String.Format("ERROR : {0} : Login failed.", DateTime.Now.ToString("g")));
        }
        //------------------ Tc_OnXLogin -------------------//
        // Sussess login
        private void Tc_OnXLogin(object sender, EventArgs e)
        {
            labelInfo.Content = "Login success";
            WriteLog(String.Format("Info : {0} : Login success.", DateTime.Now.ToString("g")));
        }

        //------------------------ Tc_OnInviteReceived ---------------------//
        // Invite received
        private void Tc_OnInviteReceived(object sender, AxTrueConf_CallXLib._ITrueConfCallXEvents_OnInviteReceivedEvent e)
        {
            WriteLog(String.Format("Info : {0} : InviteReceived !!!", DateTime.Now.ToString("g")));
            labelInfo.Content = "Invite is received";
            // accept incoming invitation
            tc.accept(); 
        }

        //----------------- Tc_OnFECCRequest -----------------//
        private void Tc_OnFECCRequest(object sender, _ITrueConfCallXEvents_OnFECCRequestEvent e)
        {
            string user = e.peerId;
            WriteLog(String.Format("Info : {0} : OnFECCRequest Received ({1}) !!!", DateTime.Now.ToString("g"), user));
            labelInfo.Content = "OnFECCRequest Received";
            // Accept 
            tc.FECCAccept(user);
        }

        //------------- WriteLog ----------------//
        public void WriteLog(string msg)
        {
            using (StreamWriter sw = new StreamWriter(logName, true))
            {
                sw.WriteLine(msg);
            }
        }
    }
}
