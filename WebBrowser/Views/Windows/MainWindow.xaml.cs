using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WebBrowserWPF.Base;
using WebBrowserWPF.Views.Pages;
using F = System.Windows.Forms;

namespace WebBrowserWPF.Views.Windows
{
    public partial class MainWindow : Window
    {
        private F.InputLanguageCollection _allInputs = F.InputLanguage.InstalledInputLanguages;

        private Rect _workRect = SystemParameters.WorkArea;

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //隐藏任务栏
            IntPtr hTray = FindWindowA("Shell_TrayWnd", string.Empty);
            ShowWindow(hTray, 0);
            var rwl2 = FindWindowA("Button", null);
            ShowWindow(rwl2, 0);

            //禁用触摸反馈
            RegeditHelper.DisbledBouncing();

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

            //启动Tcp服务
            var tcp = new TcpHelper();
            tcp.OnInputLanguageChange += ChangeInputLanguage;
            tcp.Start();

            ChangeInputLanguage(null, new MyEventArgs { InputLanguage = "中文(简体) - 手心输入法" });

            Process.Start("Keyboard.exe");
        }

        private void ChangeInputLanguage(object sender, MyEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                foreach (F.InputLanguage lang in _allInputs)
                {
                    if (lang.LayoutName == e.InputLanguage)
                    {
                        F.InputLanguage.CurrentInputLanguage = lang;
                        return;
                    }
                }
            }));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            IntPtr hTray = FindWindowA("Shell_TrayWnd", string.Empty);
            ShowWindow(hTray, 5); //显示任务栏
            var rwl2 = FindWindowA("Button", null);
            ShowWindow(rwl2, 5);

            //启用触摸反馈
            RegeditHelper.EnabledBouncing();

            var ks = Process.GetProcessesByName("Keyboard");
            foreach (var k in ks)
            {
                k.Kill();
            }
        }

        //http://www.cnblogs.com/waixingehao/archive/2011/10/12/2208598.html
        [DllImport("user32.dll", EntryPoint = "FindWindowA")]
        public static extern IntPtr FindWindowA(string lp1, string lp2);
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern IntPtr ShowWindow(IntPtr hWnd, int _value);

        private void Window_Activated(object sender, EventArgs e)
        {
            //var thisProc = Process.GetProcesses();

            Process[] ies = Process.GetProcessesByName("iexplore");
            foreach (var ie in ies)
            {
                ie.Kill();
            }
        }

        private void Window_ManipulationBoundaryFeedback(object sender, System.Windows.Input.ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
