using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WebBrowserWPF.Base;
using WebBrowserWPF.Views.Pages;

namespace WebBrowserWPF.Views.Windows
{
    public partial class MainWindow : Window
    {
        private Timer _timerToClose;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if !DEBUG
            //隐藏任务栏
            IntPtr hTray = FindWindowA("Shell_TrayWnd", string.Empty);
            ShowWindow(hTray, 0);
            var rwl2 = FindWindowA("Button", null);
            ShowWindow(rwl2, 0);
#endif

            //禁用触摸反馈
            RegeditHelper.DisbledBouncing();

            Dispatcher.Invoke(new Action(() =>
            {
                var frame = new Frame()
                {
                    NavigationUIVisibility = NavigationUIVisibility.Hidden,
                    FocusVisualStyle = null,
                };
                frame.Content = new BrowserPage1();

                Grid_Main.Children.Add(frame);
            }));

            Process.Start("Keyboard.exe");

            _timerToClose = new Timer(CloseSystemWindow, null, 1000, 1000);
        }

        private void CloseSystemWindow(object state)
        {
            var ks = Process.GetProcessesByName("SystemSettings");
            foreach (var k in ks)
            {
                k.Kill();
            }
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
            Process[] ies = Process.GetProcessesByName("iexplore");
            foreach (var ie in ies)
            {
                ie.Kill();//杀死所有被遮盖的ie窗口
            }
        }

    }
}
