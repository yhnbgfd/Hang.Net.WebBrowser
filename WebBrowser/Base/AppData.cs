using System;
using System.IO;

namespace WebBrowserWPF.Base
{
    public static class AppData
    {
        /// <summary>
        /// 弹出窗地址
        /// </summary>
        public static string[] PopupUrl = new string[0];

        static AppData()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "弹出窗地址.txt");
            if (File.Exists(file))
            {
                PopupUrl = File.ReadAllLines(file);
            }
        }
    }
}
