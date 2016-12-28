using System;
using System.Windows;
using WebBrowserWPF.Base;

namespace WebBrowserWPF.Views.UserControls
{
    public partial class WinFormWebBrowserUserControl : System.Windows.Controls.UserControl, IMyBrowser
    {
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

            ////WPF
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

    }
}
