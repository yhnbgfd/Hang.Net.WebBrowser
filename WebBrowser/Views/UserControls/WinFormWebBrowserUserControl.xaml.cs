using System;
using System.Runtime.InteropServices;
using System.Windows;
using WebBrowserWPF.Base;

namespace WebBrowserWPF.Views.UserControls
{
    public partial class WinFormWebBrowserUserControl : System.Windows.Controls.UserControl, IMyBrowser
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int flags, int dX, int dY, int buttons, int extraInfo);

        const int MOUSEEVENTF_MOVE = 0x1;
        const int MOUSEEVENTF_LEFTDOWN = 0x2;
        const int MOUSEEVENTF_LEFTUP = 0x4;
        const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        const int MOUSEEVENTF_RIGHTUP = 0x10;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        const int MOUSEEVENTF_MIDDLEUP = 0x40;
        const int MOUSEEVENTF_WHEEL = 0x800;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        private bool _isFirstLoad = true;

        public Uri MainUrl { get; set; }

        public WinFormWebBrowserUserControl()
        {
            InitializeComponent();

            //WinForm
            WebBrowser_Main.NewWindow += (s, e) =>
            {
                try
                {
                    System.Windows.Forms.WebBrowser webBrowser_temp = (System.Windows.Forms.WebBrowser)s;
                    string newUrl = webBrowser_temp.Document.ActiveElement.GetAttribute("href");
                    //if (!AppData.PopupUrl.Any(s => s == newUrl))
                    {
                        WebBrowser_Main.Url = new Uri(newUrl);
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                }
            };

            //WPF
            //WebBrowser_Main.Navigating += (s, e) =>
            //{
            //    FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            //    if (fiComWebBrowser == null) return;
            //    object objComWebBrowser = fiComWebBrowser.GetValue(s as WebBrowser);
            //    if (objComWebBrowser == null) return;
            //    objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { true });
            //};
        }

        public void Home()
        {
            WebBrowser_Main.Navigate(MainUrl);
        }

        public void Refresh()
        {
            WebBrowser_Main.Refresh();
        }

        public void GoBack()
        {
            if (WebBrowser_Main.CanGoBack)
            {
                WebBrowser_Main.GoBack();
            }
        }

        public void GoForward()
        {
            if (WebBrowser_Main.CanGoForward)
            {
                WebBrowser_Main.GoForward();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isFirstLoad)
            {
                _isFirstLoad = false;
                Home();
            }
        }

        private void Button_Up_Click(object sender, RoutedEventArgs e)
        {
            var x = System.Windows.Forms.Cursor.Position.X;
            var y = System.Windows.Forms.Cursor.Position.Y;
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(10, y);
            //WebBrowser_Main.Focus();
            WebBrowser_Main.Document.Focus();

            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 500, 0);
        }

        private void Button_Down_Click(object sender, RoutedEventArgs e)
        {
            var x = System.Windows.Forms.Cursor.Position.X;
            var y = System.Windows.Forms.Cursor.Position.Y;
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(10, y);
            //WebBrowser_Main.Focus();
            WebBrowser_Main.Document.Focus();

            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -500, 0);
        }
    }
}
