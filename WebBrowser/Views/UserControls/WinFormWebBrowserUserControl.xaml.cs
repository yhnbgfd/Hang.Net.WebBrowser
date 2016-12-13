using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WebBrowserWPF.Base;

namespace WebBrowserWPF.Views.UserControls
{
    public partial class WinFormWebBrowserUserControl : UserControl, IMyBrowser
    {
        private bool _isFirstLoad = true;

        public Uri MainUrl { get; set; }

        public WinFormWebBrowserUserControl()
        {
            InitializeComponent();
            WebBrowser_Main.NewWindow += HandleWebBrowserNewWindow;
        }

        /// <summary>
        /// 拦截弹出窗口并在本地显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleWebBrowserNewWindow(object sender, CancelEventArgs e)
        {
            try
            {
                System.Windows.Forms.WebBrowser webBrowser_temp = (System.Windows.Forms.WebBrowser)sender;
                string newUrl = webBrowser_temp.Document.ActiveElement.GetAttribute("href");
                if (!AppData.PopupUrl.Any(s => s == newUrl))
                {
                    WebBrowser_Main.Url = new Uri(newUrl);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                e.Cancel = true;
            }
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
