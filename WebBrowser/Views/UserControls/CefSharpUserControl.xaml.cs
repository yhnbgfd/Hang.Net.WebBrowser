//http://ourcodeworld.com/articles/read/173/how-to-use-cefsharp-chromium-embedded-framework-csharp-in-a-winforms-application
//CefSharp中文帮助文档 https://github.com/cefsharp/CefSharp/wiki/CefSharp%E4%B8%AD%E6%96%87%E5%B8%AE%E5%8A%A9%E6%96%87%E6%A1%A3

using CefSharp;
using CefSharp.WinForms;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WebBrowserWPF.Base;

namespace WebBrowserWPF.Views.UserControls
{
    public partial class CefSharpUserControl : UserControl, IMyBrowser
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
            chromeBrowser.LifeSpanHandler = new LifeSpanHandler()
            {
                Browser = chromeBrowser
            };
            chromeBrowser.MenuHandler = new ContextMenuHandler();
            wfh.Child = chromeBrowser;
        }

        public void Refresh()
        {
            chromeBrowser.Load(chromeBrowser.Address);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isFirstLoad)
            {
                _isFirstLoad = false;
                Home();
            }
        }

        internal class LifeSpanHandler : ILifeSpanHandler
        {
            public ChromiumWebBrowser Browser { get; set; }

            public bool DoClose(IWebBrowser browserControl, IBrowser browser)
            {
                return true;
            }

            public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
            {
            }

            public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
            {
            }

            public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
            {
                Console.WriteLine("拦截弹出窗：" + targetUrl);

                if (!AppData.PopupUrl.Any(s => s == targetUrl))
                {
                    Browser.Load(targetUrl);
                }

                newBrowser = null;
                return true;
            }
        }

        internal class ContextMenuHandler : IContextMenuHandler
        {
            public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
            {
            }

            public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
            {
                return true;
            }

            public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
            {
            }

            public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
            {
                return true;
            }
        }
    }
}
