using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using F = System.Windows.Forms;
using System.Windows.Navigation;
using WebBrowserWPF.Base;
using WebBrowserWPF.Views.Pages;

namespace WebBrowserWPF.Views.Windows
{
    public partial class MainWindow : Window
    {
        private Timer _timerToCloseSystemWindow;
        private Timer _timerToShutdown;

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

            //HookStart();

            Process.Start("Keyboard.exe");

            _timerToCloseSystemWindow = new Timer((o) =>
            {
                var ks = Process.GetProcessesByName("SystemSettings");
                foreach (var k in ks)
                {
                    k.Kill();
                }
            }, null, 1000, 1000);
            _timerToShutdown = new Timer((o) =>
            {
                //判断是否自动关机
                try
                {
                    var time = Ini.ReadValue("System", "AutoShutdownTime");
                    if (!string.IsNullOrWhiteSpace(time))
                    {
                        var fullTime = DateTime.Now.ToString("yyyy-MM-dd ") + time;
                        if (DateTime.TryParse(fullTime, out DateTime tt))
                        {
                            if (DateTime.Now >= tt)
                            {
                                Process.Start("shutdown", "/s /t 60");
                                if (MessageBox.Show("60秒后将自动关闭计算机。\n点击【确定】取消自动关机。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
                                {
                                    Process.Start("shutdown", "/a");
                                    _timerToShutdown.Change(Timeout.Infinite, Timeout.Infinite);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //_logger.Error(ex.ToString());
                }
            }, null, 1000, 1000 * 60);
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

            //HookStop();
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


        //[StructLayout(LayoutKind.Sequential)]
        //public class KeyBoardHookStruct
        //{
        //    public int vkCode;
        //    public int scanCode;
        //    public int flags;
        //    public int time;
        //    public int dwExtraInfo;
        //}
        //// 安装钩子 
        //[DllImport("user32.dll")]
        //public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //// 卸载钩子 
        //[DllImport("user32.dll")]
        //public static extern bool UnhookWindowsHookEx(int idHook);
        //// 继续下一个钩子 
        //[DllImport("user32.dll")]
        //public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        //public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        //static int hKeyboardHook = 0;
        //HookProc KeyboardHookProcedure;
        //// 安装钩子 
        //public void HookStart()
        //{
        //    if (hKeyboardHook == 0)
        //    {
        //        // 创建HookProc实例 
        //        KeyboardHookProcedure = new HookProc(KeyboardHookProc);
        //        //定义全局钩子 
        //        hKeyboardHook = SetWindowsHookEx(13, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
        //        if (hKeyboardHook == 0)
        //        {
        //            HookStop();
        //            throw new Exception("SetWindowsHookEx failed.");
        //        }
        //    }
        //}
        ////钩子子程就是钩子所要做的事情。 
        //private int KeyboardHookProc(int nCode, int wParam, IntPtr lParam)
        //{
        //    if (nCode >= 0)
        //    {
        //        KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
        //        //【BUG 截获左win会导致osk键盘打开失败】
        //        //if (kbh.vkCode == 91) // 截获左win(开始菜单键) 
        //        //    return 1;
        //        if (kbh.vkCode == 92)// 截获右win 
        //            return 1;
        //        //if (kbh.vkCode == (int)F.Keys.Escape && (int)F.Control.ModifierKeys == (int)F.Keys.Control) //截获Ctrl+Esc 
        //        //    return 1;
        //        //if (kbh.vkCode == (int)F.Keys.F4 && (int)F.Control.ModifierKeys == (int)F.Keys.Alt) //截获alt+f4 
        //        //    return 1;
        //        if (kbh.vkCode == (int)F.Keys.Tab && (int)F.Control.ModifierKeys == (int)F.Keys.Alt) //截获alt+tab 
        //            return 1;
        //        if (kbh.vkCode == (int)F.Keys.Escape && (int)F.Control.ModifierKeys == (int)F.Keys.Control + (int)F.Keys.Shift) //截获Ctrl+Shift+Esc 
        //            return 1;
        //        if (kbh.vkCode == (int)F.Keys.Space && (int)F.Control.ModifierKeys == (int)F.Keys.Alt) //截获alt+空格 
        //            return 1;
        //        //if (kbh.vkCode == 241) //截获F1 
        //        //    return 1;
        //        if (kbh.vkCode == (int)F.Keys.Control && kbh.vkCode == (int)F.Keys.Alt && kbh.vkCode == (int)F.Keys.Delete) //截获Ctrl+Alt+Delete 
        //            return 1;
        //        if ((int)F.Control.ModifierKeys == (int)F.Keys.Control + (int)F.Keys.Alt + (int)F.Keys.Delete)      //截获Ctrl+Alt+Delete 
        //            return 1;
        //        //if ((int)F.Control.ModifierKeys == (int)F.Keys.Control + (int)F.Keys.Shift)      //截获Ctrl+Shift 
        //        //{
        //        //    return 1;
        //        //}
        //    }
        //    return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        //}
        //// 卸载钩子 
        //public void HookStop()
        //{
        //    bool retKeyboard = true;
        //    if (hKeyboardHook != 0)
        //    {
        //        retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
        //        hKeyboardHook = 0;
        //    }
        //    if (!(retKeyboard)) throw new Exception("UnhookWindowsHookEx failed.");
        //}
    }
}
