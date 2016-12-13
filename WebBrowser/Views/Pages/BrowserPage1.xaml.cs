using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WebBrowserWPF.Base;
using WebBrowserWPF.Views.UserControls;
using WebBrowserWPF.Views.Windows;

namespace WebBrowserWPF.Views.Pages
{
    public partial class BrowserPage1 : Page
    {
        public BrowserPage1()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var kernel = Ini.ReadValue("System", "Kernel");

            string[] webs = new string[0];
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "网页地址.txt");
            if (File.Exists(file))
            {
                webs = File.ReadAllLines(file);
            }

            ResourceDictionary rd = new ResourceDictionary()
            {
                Source = new Uri(@"/WebBrowserWPF;component/Themes/TabItemDictionary.xaml", UriKind.RelativeOrAbsolute)
            };
            Style mystyles = rd["TabItemStyle1"] as Style;

            foreach (var web in webs)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(web) && web.Contains('='))
                    {
                        var split = web.Split(new[] { '=' }, 2);

                        IWebBrowser wb;
                        if (kernel == "IE")
                        {
                            wb = new WinFormWebBrowserUserControl()
                            {
                                MainUrl = new Uri(split[1])
                            };
                        }
                        else//Chrome
                        {
                            wb = new CefSharpUserControl()
                            {
                                MainUrl = new Uri(split[1])
                            };
                        }

                        TabControl_Main.Items.Add(new TabItem()
                        {
                            Header = split[0],
                            Padding = new Thickness(15, 10, 15, 10),
                            Content = wb,
                            Style = mystyles,
                        });
                    }
                }
                catch (Exception ex)
                {

                }
            }

            TabControl_Main.SelectedIndex = 0;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Keyboard_Click(object sender, RoutedEventArgs e)
        {
            Process pk = Process.Start("osk.exe");
        }

        private void Button_Home_Click(object sender, RoutedEventArgs e)
        {
            var wb = (TabControl_Main.SelectedItem as TabItem).Content as IWebBrowser;
            wb.Home();
        }
        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            var wb = (TabControl_Main.SelectedItem as TabItem).Content as IWebBrowser;
            wb.Refresh();
        }
        private void Button_GoBack_Click(object sender, RoutedEventArgs e)
        {
            var wb = (TabControl_Main.SelectedItem as TabItem).Content as IWebBrowser;
            wb.GoBack();
        }
        private void Button_GoForward_Click(object sender, RoutedEventArgs e)
        {
            var wb = (TabControl_Main.SelectedItem as TabItem).Content as IWebBrowser;
            wb.GoForward();
        }

        private void Button_Setting_Click(object sender, RoutedEventArgs e)
        {
            var win = new SettingWindow()
            {
                Owner = Application.Current.MainWindow
            };
            win.ShowDialog();
        }

    }
}
