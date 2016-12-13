using System;

namespace WebBrowserWPF.Base
{
    public interface IWebBrowser
    {
        Uri MainUrl { get; set; }
        void Home();
        void Refresh();
        void GoBack();
        void GoForward();
    }
}
