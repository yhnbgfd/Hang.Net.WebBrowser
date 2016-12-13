using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace WebBrowserWPF.Base
{
    public static class Ini
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private static string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.ini");

        public static void WriteValue(string section, string key, string value)
        {
            // section=配置节，key=键名，value=键值，path=路径
            WritePrivateProfileString(section, key, value, _path);
        }

        public static string ReadValue(string section, string key, string file)
        {
            var temp = new StringBuilder(255); // 每次从ini中读取多少字节
            GetPrivateProfileString(section, key, "", temp, 255, file);
            return temp.ToString();
        }

        public static string ReadValue(string section, string key)
        {
            return ReadValue(section, key, _path);
        }

    }
}
