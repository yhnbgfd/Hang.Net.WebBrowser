using System.Diagnostics;
using System.Windows;

namespace WebBrowserWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            {
                MessageBox.Show(thisProc.ProcessName + " 已经运行。");
                Current.Shutdown();
                return;
            }
            base.OnStartup(e);
        }
    }
}
