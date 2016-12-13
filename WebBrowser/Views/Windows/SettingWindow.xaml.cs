using System;
using System.IO;
using System.Security;
using System.Windows;
using WebBrowserWPF.Base;

namespace WebBrowserWPF.Views.Windows
{
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            Grid_Password.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "网页地址.txt");
                if (File.Exists(file))
                {
                    TextBox_Webs.Text = File.ReadAllText(file);
                }
            }
            {
                var popupFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "弹出窗地址.txt");
                if (File.Exists(popupFile))
                {
                    TextBox_PopupWebs.Text = File.ReadAllText(popupFile);
                }
            }

            PasswordBox_Pwd.Focus();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "网页地址.txt");
            if (!File.Exists(file))
            {
                File.CreateText(file);
            }
            File.WriteAllText(file, TextBox_Webs.Text);

            MessageBox.Show("保存成功。");
        }

        private void Button_ConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            var pwd = Ini.ReadValue("System", "Password");
            var input = TranslateToString(PasswordBox_Pwd.SecurePassword);
            if (input == pwd)
            {
                Grid_Password.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("密码错误。");
            }
        }

        private void Button_CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            Ini.WriteValue("System", "Password", TextBox_NewPassword.Text.Trim());

            MessageBox.Show("密码修改成功。");
        }

        /// <summary>
        /// 翻译SecureString至明文
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string TranslateToString(SecureString password)
        {
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(password);
            return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
        }

        private void Button_SavePopup_Click(object sender, RoutedEventArgs e)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "弹出窗地址.txt");
            if (!File.Exists(file))
            {
                File.CreateText(file);
            }
            File.WriteAllText(file, TextBox_Webs.Text);

            MessageBox.Show("保存成功。");
        }
    }
}
