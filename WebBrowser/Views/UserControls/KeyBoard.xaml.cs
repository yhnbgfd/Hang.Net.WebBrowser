using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WebBrowserWPF.Views.UserControls
{
    public partial class KeyBoard : UserControl
    {
        private bool isShift = false;
        private bool isCtrl = false;

        public KeyBoard()
        {
            InitializeComponent();
        }

        private void Button_Key_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var c1 = btn.Content.ToString();
            var c2 = string.Empty;
            if (btn.Tag != null)
            {
                c2 = btn.Tag.ToString();
            }

            //功能按键
            switch (c1)
            {
                case "Backspace": System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}"); return;
                case "Tab": System.Windows.Forms.SendKeys.SendWait("{TAB}"); return;
                case "Caps": System.Windows.Forms.SendKeys.SendWait("{CAPSLOCK}"); return;
                case "Enter": System.Windows.Forms.SendKeys.SendWait("{ENTER}"); return;
                case "Shift":
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
                case "中":
                    return;
                case "英":
                    return;
            }

            //值按键
            var func = "";
            if (isShift == true)
            {
                func = "+";
                isShift = false;
                Button_Shift1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                Button_Shift2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            }
            switch (c1)
            {
                case "1": System.Windows.Forms.SendKeys.SendWait(func + "1"); return;
                case "2": System.Windows.Forms.SendKeys.SendWait(func + "2"); return;
                case "3": System.Windows.Forms.SendKeys.SendWait(func + "3"); return;
                case "4": System.Windows.Forms.SendKeys.SendWait(func + "4"); return;
                case "5": System.Windows.Forms.SendKeys.SendWait(func + "5"); return;
                case "6": System.Windows.Forms.SendKeys.SendWait(func + "6"); return;
                case "7": System.Windows.Forms.SendKeys.SendWait(func + "7"); return;
                case "8": System.Windows.Forms.SendKeys.SendWait(func + "8"); return;
                case "9": System.Windows.Forms.SendKeys.SendWait(func + "9"); return;
                case "0": System.Windows.Forms.SendKeys.SendWait(func + "0"); return;
                case "A": System.Windows.Forms.SendKeys.SendWait(func + "a"); return;
                case "B": System.Windows.Forms.SendKeys.SendWait(func + "b"); return;
                case "C": System.Windows.Forms.SendKeys.SendWait(func + "c"); return;
                case "D": System.Windows.Forms.SendKeys.SendWait(func + "d"); return;
                case "E": System.Windows.Forms.SendKeys.SendWait(func + "e"); return;
                case "F": System.Windows.Forms.SendKeys.SendWait(func + "f"); return;
                case "G": System.Windows.Forms.SendKeys.SendWait(func + "g"); return;
                case "H": System.Windows.Forms.SendKeys.SendWait(func + "h"); return;
                case "I": System.Windows.Forms.SendKeys.SendWait(func + "i"); return;
                case "J": System.Windows.Forms.SendKeys.SendWait(func + "j"); return;
                case "K": System.Windows.Forms.SendKeys.SendWait(func + "k"); return;
                case "L": System.Windows.Forms.SendKeys.SendWait(func + "l"); return;
                case "M": System.Windows.Forms.SendKeys.SendWait(func + "m"); return;
                case "N": System.Windows.Forms.SendKeys.SendWait(func + "n"); return;
                case "O": System.Windows.Forms.SendKeys.SendWait(func + "o"); return;
                case "P": System.Windows.Forms.SendKeys.SendWait(func + "p"); return;
                case "Q": System.Windows.Forms.SendKeys.SendWait(func + "q"); return;
                case "R": System.Windows.Forms.SendKeys.SendWait(func + "r"); return;
                case "S": System.Windows.Forms.SendKeys.SendWait(func + "s"); return;
                case "T": System.Windows.Forms.SendKeys.SendWait(func + "t"); return;
                case "U": System.Windows.Forms.SendKeys.SendWait(func + "u"); return;
                case "V": System.Windows.Forms.SendKeys.SendWait(func + "v"); return;
                case "W": System.Windows.Forms.SendKeys.SendWait(func + "w"); return;
                case "X": System.Windows.Forms.SendKeys.SendWait(func + "x"); return;
                case "Y": System.Windows.Forms.SendKeys.SendWait(func + "y"); return;
                case "Z": System.Windows.Forms.SendKeys.SendWait(func + "z"); return;
            }
        }
    }
}
