using CefSharp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WebBrowserWPF.Base;
using WebBrowserWPF.Views.Pages;

namespace WebBrowserWPF.Views.Windows
{
    public partial class MainWindow : Window
    {
        private System.Windows.Rect _workRect = SystemParameters.WorkArea;

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
            InitializeChromium();
        }

        private void InitializeWindow()
        {
            var pageId = Ini.ReadValue("System", "BrowserPage");
            switch (pageId)
            {
                case "2":
                    WindowState = WindowState.Normal;
                    Width = _workRect.Width;
                    Left = _workRect.Left;
                    Height = _workRect.Height;
                    Top = _workRect.Top;
                    break;
            }
        }

        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(settings);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                var pageId = Ini.ReadValue("System", "BrowserPage");

                var frame = new Frame()
                {
                    NavigationUIVisibility = NavigationUIVisibility.Hidden,
                    FocusVisualStyle = null,
                };
                switch (pageId)
                {
                    case "1":
                        frame.Content = new BrowserPage1();
                        break;
                    case "2":
                    default:
                        frame.Content = new BrowserPage2();
                        break;
                }

                Grid_Main.Children.Add(frame);
            }));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
