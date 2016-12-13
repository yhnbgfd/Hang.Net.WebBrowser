//http://ourcodeworld.com/articles/read/173/how-to-use-cefsharp-chromium-embedded-framework-csharp-in-a-winforms-application
//CefSharp中文帮助文档 https://github.com/cefsharp/CefSharp/wiki/CefSharp%E4%B8%AD%E6%96%87%E5%B8%AE%E5%8A%A9%E6%96%87%E6%A1%A3

using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WebBrowserWPF.Views.UserControls
{
    public partial class CefSharpUserControl : UserControl, Base.IWebBrowser
    {
        private bool _isFirstLoad = true;
        public ChromiumWebBrowser chromeBrowser;

        public Uri MainUrl { get; set; }

        public CefSharpUserControl()
        {
            InitializeComponent();
        }

        public void GoBack()
        {
            if (chromeBrowser.CanGoBack)
            {
                chromeBrowser.Back();
            }
        }

        public void GoForward()
        {
            if (chromeBrowser.CanGoForward)
            {
                chromeBrowser.Forward();
            }
        }

        public void Home()
        {
            chromeBrowser = new ChromiumWebBrowser(MainUrl.ToString());
            wfh.Child = chromeBrowser;
        }

        public void Refresh()
        {

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
