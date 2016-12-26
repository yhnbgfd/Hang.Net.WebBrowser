using Microsoft.Win32;
using System;
using System.Windows;

namespace WebBrowserWPF.Base
{
    /// <summary>
    /// 注册表工具类
    /// </summary>
    public static class RegeditHelper
    {
        public static bool? ReadUSBState()
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine; //读取注册列表HKEY_LOCAL_MACHINE  
                string keyPath = @"SYSTEM\CurrentControlSet\Services\USBSTOR"; //USB 大容量存储驱动程序  
                RegistryKey openKey = regKey.OpenSubKey(keyPath, true);
                var value = openKey.GetValue("Start").ToString();
                if (value == "3")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool RegToRunUSB()
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine; //读取注册列表HKEY_LOCAL_MACHINE  
                string keyPath = @"SYSTEM\CurrentControlSet\Services\USBSTOR"; //USB 大容量存储驱动程序  
                RegistryKey openKey = regKey.OpenSubKey(keyPath, true);
                openKey.SetValue("Start", 3); //设置键值对（3）为开启USB（4）为关闭  
                openKey.Close(); //关闭注册列表读写流  
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("请以管理员权限运行程序。");
                return false;
            }
        }

        /// <summary>  
        /// 通过注册表禁用USB  
        /// </summary>  
        /// <returns></returns>  
        public static bool RegToStopUSB()
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine;
                string keyPath = @"SYSTEM\CurrentControlSet\Services\USBSTOR";
                RegistryKey openKey = regKey.OpenSubKey(keyPath, true);
                openKey.SetValue("Start", 4);
                openKey.Close();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("请以管理员权限运行程序。");
                return false;
            }
        }
    }
}
