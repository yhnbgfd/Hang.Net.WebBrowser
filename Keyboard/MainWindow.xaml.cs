using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using F = System.Windows.Forms;

namespace Keyboard
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Top = SystemParameters.PrimaryScreenHeight - Height - 20;
            Left = SystemParameters.PrimaryScreenWidth - Width - 30;
        }

        private void Button_KeyBoard_Click(object sender, RoutedEventArgs e)
        {
            //调用系统的屏幕键盘
            //var file = @"C:\Program Files\Common Files\microsoft shared\ink\TabTip.exe";
            //if (File.Exists(file))
            //{
            //    Process.Start(file);
            //}
            //else
            {
                var ks = Process.GetProcessesByName("osk.exe");
                foreach (var k in ks)
                {
                    k.Kill();
                }
                Process pk = Process.Start("osk.exe");
            }
        }

        private void Button_Input_Click(object sender, RoutedEventArgs e)
        {
            keybd_event((byte)F.Keys.ControlKey, 0, 0, 0);  //按下
            keybd_event((byte)F.Keys.ShiftKey, 0, 0, 0);  //按下
            keybd_event((byte)F.Keys.ShiftKey, 0, 0x2, 0);  //弹起
            keybd_event((byte)F.Keys.ControlKey, 0, 0x2, 0);  //弹起
        }

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
    }
}