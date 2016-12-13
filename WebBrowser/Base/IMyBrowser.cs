using System;

namespace WebBrowserWPF.Base
{
    public interface IMyBrowser
    {
        Uri MainUrl { get; set; }
        void Home();
        void Refresh();
        void GoBack();
        void GoForward();
    }
}
