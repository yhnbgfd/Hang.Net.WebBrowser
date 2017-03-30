using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using WebBrowserWPF.Base;

namespace WebBrowserWPF.Views.Windows
{
    public partial class SettingWindow : Window
    {
        private string _password = "";

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
            ReadUSBState();

            var pwd = Ini.ReadValue("System", "Password");
            var shutdownPwd = Ini.ReadValue("System", "ShutdownPassword");
            TextBox_NewPassword.Text = pwd;
            TextBox_ShitdownPassword.Text = shutdownPwd;
            TextBox_AutoShutdownTime.Text = Ini.ReadValue("System", "AutoShutdownTime");
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
            var shutdownPwd = Ini.ReadValue("System", "ShutdownPassword");
            var input = TranslateToString(PasswordBox_Pwd.SecurePassword);
            if (input == pwd)
            {
                Grid_Password.Visibility = Visibility.Collapsed;
            }
            else if (input == shutdownPwd)
            {
                Grid_Password.Visibility = Visibility.Collapsed;

                TabItem_基础设置.Visibility = Visibility.Collapsed;
                TabItem_网址设置.Visibility = Visibility.Collapsed;
                TabItem_关闭电脑.Visibility = Visibility.Visible;

                TabCtrl.SelectedItem = TabItem_关闭电脑;
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

        private void Button_EnabledUSB_Click(object sender, RoutedEventArgs e)
        {
            RegeditHelper.RegToRunUSB();
            ReadUSBState();
        }

        private void Button_DisabledUSB_Click(object sender, RoutedEventArgs e)
        {
            RegeditHelper.RegToStopUSB();
            ReadUSBState();
        }

        private void ReadUSBState()
        {
            var ret = RegeditHelper.ReadUSBState();
            if (ret == true)
            {
                TextBlock_USBState.Text = "启用";
            }
            else if (ret == false)
            {
                TextBlock_USBState.Text = "禁用";
            }
            else
            {
                TextBlock_USBState.Text = "读取USB状态需要系统管理员权限";
            }
        }

        private void Button_Num_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var num = btn.Content.ToString();
            _password += num;
            PasswordBox_Pwd.Password = _password;
        }

        private void Button_Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (_password.Length == 0)
            {
                return;
            }
            var btn = sender as Button;
            _password = _password.Substring(0, _password.Length - 1);
            PasswordBox_Pwd.Password = _password;
        }

        private void Button_ChangeShutdownPassword_Click(object sender, RoutedEventArgs e)
        {
            Ini.WriteValue("System", "ShutdownPassword", TextBox_ShitdownPassword.Text.Trim());

            MessageBox.Show("关机密码修改成功。");
        }

        private void Button_Shutdown_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要关机？", "确认", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Process.Start("shutdown.exe", "-s -f");
            }
            else
            {
            }
        }

        private void Button_SaveAutoShutdownTime_Click(object sender, RoutedEventArgs e)
        {
            var time = TextBox_AutoShutdownTime.Text.Trim().Replace("：", ":");
            Ini.WriteValue("System", "AutoShutdownTime", time);
            MessageBox.Show("自动关机时间设置成功。");
        }
    }
}
