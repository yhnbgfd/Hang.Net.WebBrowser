using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace WebBrowserWPF.Views.UserControls
{
    public partial class KeyBoard : System.Windows.Controls.UserControl
    {
        private InputLanguageCollection _allInputs = InputLanguage.InstalledInputLanguages;
        private int _currentInputIndex = -1;

        private bool isShift = false;
        private bool isCtrl = false;
        private bool isCaps = false;

        public KeyBoard()
        {
            InitializeComponent();
        }

        private void Button_Key_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.Button;
            var c1 = btn.Content.ToString();
            var c2 = string.Empty;
            if (btn.Tag != null)
            {
                c2 = btn.Tag.ToString();
            }

            //功能按键
            switch (c1)
            {
                case "Backspace": SendKeys.SendWait("{BACKSPACE}"); return;
                case "Tab": SendKeys.SendWait("{TAB}"); return;
                case "Caps":
                    {
                        isCaps = !isCaps;
                        if (isCaps)
                        {
                            Button_Caps.Background = new SolidColorBrush(Color.FromRgb(148, 62, 189));
                        }
                        else
                        {
                            Button_Caps.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                        }
                        //System.Windows.Forms.SendKeys.SendWait("{CAPSLOCK}");//这个发送了没啥效果
                        return;
                    }
                case "Enter": SendKeys.SendWait("{ENTER}"); return;
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
                case "Ctrl":
                    {
                        isCtrl = !isCtrl;
                        if (isCtrl == true)
                        {
                            Button_Ctrl.Background = new SolidColorBrush(Color.FromRgb(148, 62, 189));
                        }
                        else
                        {
                            Button_Ctrl.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                        }
                        return;
                    }
            }

            //值按键
            var func = "";
            if ((isShift == true && isCaps == false) || (isCaps == true && isShift == false))
            {
                func = "+";
            }
            isShift = false;//按什么键都要重置Shift
            Button_Shift1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            Button_Shift2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            switch (c1)
            {
                case "1": SendKeys.SendWait(func + "1"); return;
                case "2": SendKeys.SendWait(func + "2"); return;
                case "3": SendKeys.SendWait(func + "3"); return;
                case "4": SendKeys.SendWait(func + "4"); return;
                case "5": SendKeys.SendWait(func + "5"); return;
                case "6": SendKeys.SendWait(func + "6"); return;
                case "7": SendKeys.SendWait(func + "7"); return;
                case "8": SendKeys.SendWait(func + "8"); return;
                case "9": SendKeys.SendWait(func + "9"); return;
                case "0": SendKeys.SendWait(func + "0"); return;
                case "A": SendKeys.SendWait(func + "a"); return;
                case "B": SendKeys.SendWait(func + "b"); return;
                case "C": SendKeys.SendWait(func + "c"); return;
                case "D": SendKeys.SendWait(func + "d"); return;
                case "E": SendKeys.SendWait(func + "e"); return;
                case "F": SendKeys.SendWait(func + "f"); return;
                case "G": SendKeys.SendWait(func + "g"); return;
                case "H": SendKeys.SendWait(func + "h"); return;
                case "I": SendKeys.SendWait(func + "i"); return;
                case "J": SendKeys.SendWait(func + "j"); return;
                case "K": SendKeys.SendWait(func + "k"); return;
                case "L": SendKeys.SendWait(func + "l"); return;
                case "M": SendKeys.SendWait(func + "m"); return;
                case "N": SendKeys.SendWait(func + "n"); return;
                case "O": SendKeys.SendWait(func + "o"); return;
                case "P": SendKeys.SendWait(func + "p"); return;
                case "Q": SendKeys.SendWait(func + "q"); return;
                case "R": SendKeys.SendWait(func + "r"); return;
                case "S": SendKeys.SendWait(func + "s"); return;
                case "T": SendKeys.SendWait(func + "t"); return;
                case "U": SendKeys.SendWait(func + "u"); return;
                case "V": SendKeys.SendWait(func + "v"); return;
                case "W": SendKeys.SendWait(func + "w"); return;
                case "X": SendKeys.SendWait(func + "x"); return;
                case "Y": SendKeys.SendWait(func + "y"); return;
                case "Z": SendKeys.SendWait(func + "z"); return;
                case " ": SendKeys.SendWait(" "); return;

                case "`": SendKeys.SendWait(func + "{`}"); return;
                case "-": SendKeys.SendWait(func + "{-}"); return;
                case "=": SendKeys.SendWait(func + "{=}"); return;
                case "[": SendKeys.SendWait(func + "{[}"); return;
                case "]": SendKeys.SendWait(func + "{]}"); return;
                case "\\": SendKeys.SendWait(func + "{\\}"); return;
                case ";": SendKeys.SendWait(func + "{;}"); return;
                case "'": SendKeys.SendWait(func + "{'}"); return;
                case ",": SendKeys.SendWait(func + "{,}"); return;
                case ".": SendKeys.SendWait(func + "{.}"); return;
                case "/": SendKeys.SendWait(func + "{/}"); return;
            }
        }

        private void Button_Lang_Click(object sender, RoutedEventArgs e)
        {
            _currentInputIndex++;
            if (_currentInputIndex >= _allInputs.Count)
            {
                _currentInputIndex = 0;
            }
            InputLanguage.CurrentInputLanguage = _allInputs[_currentInputIndex];
            Button_Lang.Content = InputLanguage.CurrentInputLanguage.LayoutName;
        }

    }
}
