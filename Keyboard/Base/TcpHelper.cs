using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Keyboard.Base
{
    internal class TcpHelper
    {
        internal void Send(string text)
        {
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 1112);
                    if (tcpClient.Connected) //判断是否连接到服务器
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(text);
                        using (NetworkStream ns = tcpClient.GetStream())
                        {
                            ns.Write(bytes, 0, bytes.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
