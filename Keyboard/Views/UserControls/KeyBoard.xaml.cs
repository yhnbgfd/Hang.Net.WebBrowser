using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WindowsInput;
using WindowsInput.Native;
using F = System.Windows.Forms;

namespace Keyboard.Views.UserControls
{
    public partial class KeyBoard : UserControl
    {
        private IKeyboardSimulator _keyboard = new InputSimulator().Keyboard;

        private bool isShift = false;
        private bool isCaps = false;

        public KeyBoard()
        {
            InitializeComponent();
        }

        private void Button_Key_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var c1 = btn.Content.ToString();

            //功能按键
            switch (c1)
            {
                case "Backspace":
                    _keyboard.KeyPress(VirtualKeyCode.BACK);
                    return;
                case "Tab":
                    _keyboard.KeyPress(VirtualKeyCode.TAB);
                    return;
                case "Caps":
                    {
                        _keyboard.KeyPress(VirtualKeyCode.CAPITAL);

                        isCaps = !isCaps;//改变按键颜色
                        if (isCaps)
                        {
                            Button_Caps.Background = new SolidColorBrush(Color.FromRgb(148, 62, 189));
                        }
                        else
                        {
                            Button_Caps.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                        }
                        return;
                    }
                case "Enter":
                    _keyboard.KeyPress(VirtualKeyCode.RETURN);
                    return;
                case "Shift"://Shift由值按键触发时智能添加
                    {
                        isShift = !isShift;
                        if (isShift == true)
                        {
                            Button_Shift1.Background = new SolidColorBrush(Color.FromRgb(148, 62, 189));
                            Button_Shift2.Background = new SolidColorBrush(Color.FromRgb(148, 62, 189));
                        }
                        else
                        {
                            Button_Shift1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                            Button_Shift2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                        }
                        return;
                    }
            }

            //值按键
            if (isShift == true)
            {
                _keyboard.KeyDown(VirtualKeyCode.SHIFT);
            }

            switch (c1)
            {
                #region 数字
                case "1": _keyboard.KeyPress(VirtualKeyCode.VK_1); break;
                case "2": _keyboard.KeyPress(VirtualKeyCode.VK_2); break;
                case "3": _keyboard.KeyPress(VirtualKeyCode.VK_3); break;
                case "4": _keyboard.KeyPress(VirtualKeyCode.VK_4); break;
                case "5": _keyboard.KeyPress(VirtualKeyCode.VK_5); break;
                case "6": _keyboard.KeyPress(VirtualKeyCode.VK_6); break;
                case "7": _keyboard.KeyPress(VirtualKeyCode.VK_7); break;
                case "8": _keyboard.KeyPress(VirtualKeyCode.VK_8); break;
                case "9": _keyboard.KeyPress(VirtualKeyCode.VK_9); break;
                case "0": _keyboard.KeyPress(VirtualKeyCode.VK_0); break;
                #endregion
                #region 字母
                case "A": _keyboard.KeyPress(VirtualKeyCode.VK_A); break;
                case "B": _keyboard.KeyPress(VirtualKeyCode.VK_B); break;
                case "C": _keyboard.KeyPress(VirtualKeyCode.VK_C); break;
                case "D": _keyboard.KeyPress(VirtualKeyCode.VK_D); break;
                case "E": _keyboard.KeyPress(VirtualKeyCode.VK_E); break;
                case "F": _keyboard.KeyPress(VirtualKeyCode.VK_F); break;
                case "G": _keyboard.KeyPress(VirtualKeyCode.VK_G); break;
                case "H": _keyboard.KeyPress(VirtualKeyCode.VK_H); break;
                case "I": _keyboard.KeyPress(VirtualKeyCode.VK_I); break;
                case "J": _keyboard.KeyPress(VirtualKeyCode.VK_J); break;
                case "K": _keyboard.KeyPress(VirtualKeyCode.VK_K); break;
                case "L": _keyboard.KeyPress(VirtualKeyCode.VK_L); break;
                case "M": _keyboard.KeyPress(VirtualKeyCode.VK_M); break;
                case "N": _keyboard.KeyPress(VirtualKeyCode.VK_N); break;
                case "O": _keyboard.KeyPress(VirtualKeyCode.VK_O); break;
                case "P": _keyboard.KeyPress(VirtualKeyCode.VK_P); break;
                case "Q": _keyboard.KeyPress(VirtualKeyCode.VK_Q); break;
                case "R": _keyboard.KeyPress(VirtualKeyCode.VK_R); break;
                case "S": _keyboard.KeyPress(VirtualKeyCode.VK_S); break;
                case "T": _keyboard.KeyPress(VirtualKeyCode.VK_T); break;
                case "U": _keyboard.KeyPress(VirtualKeyCode.VK_U); break;
                case "V": _keyboard.KeyPress(VirtualKeyCode.VK_V); break;
                case "W": _keyboard.KeyPress(VirtualKeyCode.VK_W); break;
                case "X": _keyboard.KeyPress(VirtualKeyCode.VK_X); break;
                case "Y": _keyboard.KeyPress(VirtualKeyCode.VK_Y); break;
                case "Z": _keyboard.KeyPress(VirtualKeyCode.VK_Z); break;
                #endregion
                case " ": _keyboard.KeyPress(VirtualKeyCode.SPACE); break;

                case "`": _keyboard.KeyPress(VirtualKeyCode.OEM_3); break;
                case "-": _keyboard.KeyPress(VirtualKeyCode.OEM_MINUS); break;
                case "=": _keyboard.KeyPress(VirtualKeyCode.OEM_PLUS); break;
                case "[": _keyboard.KeyPress(VirtualKeyCode.OEM_4); break;
                case "]": _keyboard.KeyPress(VirtualKeyCode.OEM_6); break;
                case "\\": _keyboard.KeyPress(VirtualKeyCode.OEM_5); break;
                case ";": _keyboard.KeyPress(VirtualKeyCode.OEM_1); break;
                case "'": _keyboard.KeyPress(VirtualKeyCode.OEM_7); break;
                case ",": _keyboard.KeyPress(VirtualKeyCode.OEM_COMMA); break;
                case ".": _keyboard.KeyPress(VirtualKeyCode.OEM_PERIOD); break;
                case "/": _keyboard.KeyPress(VirtualKeyCode.OEM_2); break;
            }
            if (isShift == true)
            {
                _keyboard.KeyUp(VirtualKeyCode.SHIFT);
            }
            isShift = false;//按什么键都要重置Shift
            Button_Shift1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            Button_Shift2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }

        /// <summary>
        /// 切换输入法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Lang_Click(object sender, RoutedEventArgs e)
        {
            _keyboard.KeyDown(VirtualKeyCode.CONTROL);
            _keyboard.KeyPress(VirtualKeyCode.SHIFT);
            _keyboard.KeyUp(VirtualKeyCode.CONTROL);
        }

        private void Button_LeftKey_Click(object sender, RoutedEventArgs e)
        {
            _keyboard.KeyPress(VirtualKeyCode.LEFT);
        }

        private void Button_RightKey_Click(object sender, RoutedEventArgs e)
        {
            _keyboard.KeyPress(VirtualKeyCode.RIGHT);
        }
    }
}
