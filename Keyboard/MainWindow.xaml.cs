using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace Keyboard
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Top = SystemParameters.PrimaryScreenHeight - Height - 50;
            Left = SystemParameters.PrimaryScreenWidth - Width - 50;
        }

        private void Button_KeyBoard_Click(object sender, RoutedEventArgs e)
        {
            Popup_KeyBoard.IsOpen = !Popup_KeyBoard.IsOpen;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //if (e.ButtonState == MouseButtonState.Pressed)
                {
                    POINT curPos;
                    IntPtr hWndPopup;

                    GetCursorPos(out curPos);
                    hWndPopup = WindowFromPoint(curPos);

                    ReleaseCapture();
                    SendMessage(hWndPopup, WM_NCLBUTTONDOWN, new IntPtr(HT_CAPTION), IntPtr.Zero);
                }
            }
            catch (Exception)
            {

            }
        }
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void Button_ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            Popup_KeyBoard.IsOpen = false;
        }
    }
}