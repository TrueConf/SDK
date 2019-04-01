using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace RemotePrintPdfClient
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private MainPage mainPage;

        public LoginPage()
        {
            InitializeComponent();
            defaultBorder = txtID.BorderBrush;
            mainPage = new MainPage();
            Loaded += LoginPage_Loaded;
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationWindow nav = (NavigationWindow)Window.GetWindow(this);
            nav.Navigating += (s, ev) => ev.Cancel = ev.NavigationMode == NavigationMode.Forward;
            nav.Title = "TrueConf Remote Print PDF Example";
            nav.WindowStyle = WindowStyle.SingleBorderWindow;
            nav.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private Brush defaultBorder;

        internal bool CheckEnteredData()
        {
            //show red border if fields was empty
            txtID.BorderBrush = txtID.Text.Length > 0 ? defaultBorder : new SolidColorBrush(Colors.Red);
            txtPassword.BorderBrush = txtPassword.Password.Length > 0 ? defaultBorder : new SolidColorBrush(Colors.Red);
            return txtID.Text.Length > 0 && txtPassword.Password.Length > 0;
        }

        private void Txt_GotFocus(object sender, RoutedEventArgs e)
        {
            //finish displaying red border after item got focus
            Control txt = (Control)sender;
            if (defaultBorder != null)
            {
                txt.BorderBrush = defaultBorder;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            //check enetred id and password
            if (CheckEnteredData())
            {
                //navigate to main page
                NavigationWindow nav = (NavigationWindow)Window.GetWindow(this);
                mainPage.SetLoginData(txtServer.Text, txtID.Text, txtPassword.Password);
                nav.Navigate(mainPage);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //exit
            NavigationWindow nav = (NavigationWindow)Window.GetWindow(this);
            nav.Close();
        }
    }
}
