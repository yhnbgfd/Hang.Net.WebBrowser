using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowserWPF.Base
{
    internal class TcpHelper
    {
        internal EventHandler<MyEventArgs> OnInputLanguageChange;

        internal void Start()
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Parse("0.0.0.0"), 1112);
                Task.Factory.StartNew(() =>
                {
                    listener.Start();
                    while (true)
                    {
                        try
                        {
                            TcpClient client = listener.AcceptTcpClient();
                            Task.Factory.StartNew(() =>
                            {
                                try
                                {
                                    List<byte> byteList = new List<byte>();
                                    using (NetworkStream stream = client.GetStream())
                                    {
                                        byte[] bytes = new byte[10];
                                        int i;
                                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                                        {
                                            byteList.AddRange(bytes.Take(i));
                                        }
                                    }
                                    if (byteList.Count > 0)
                                    {
                                        string data = Encoding.UTF8.GetString(byteList.ToArray());
                                        OnInputLanguageChange?.Invoke(this, new MyEventArgs { InputLanguage = data });
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                                finally
                                {
                                    client.Close();
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }).ContinueWith((t) => { }, TaskContinuationOptions.OnlyOnFaulted);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
