using Keyboard.Base;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using F = System.Windows.Forms;

namespace Keyboard
{
    public partial class MainWindow : Window
    {
        private F.InputLanguageCollection _allInputs = F.InputLanguage.InstalledInputLanguages;
        private int _currentInputIndex = -1;

        public MainWindow()
        {
            InitializeComponent();

            Top = SystemParameters.PrimaryScreenHeight - Height - 20;
            Left = SystemParameters.PrimaryScreenWidth - Width - 30;

            //Popup_KeyBoard.CustomPopupPlacementCallback = new CustomPopupPlacementCallback(placePopup);
        }

        //private CustomPopupPlacement[] placePopup(Size popupSize, Size targetSize, Point offset)
        //{
        //    CustomPopupPlacement placement1 = new CustomPopupPlacement(new Point(0, 0), PopupPrimaryAxis.None);
        //    CustomPopupPlacement[] ttplaces = new CustomPopupPlacement[] { placement1 };
        //    return ttplaces;
        //}

        private void Button_KeyBoard_Click(object sender, RoutedEventArgs e)
        {
            //Popup_KeyBoard.IsOpen = !Popup_KeyBoard.IsOpen;


            var file = @"C:\Program Files\Common Files\microsoft shared\ink\TabTip.exe";
            if (File.Exists(file))
            {
                Process.Start(file);
            }
            else
            {
                var ks = Process.GetProcessesByName("osk.exe");
                foreach (var k in ks)
                {
                    k.Kill();
                }
                Process pk = Process.Start("osk.exe");
            }

            //_currentInputIndex++;
            //if (_currentInputIndex >= _allInputs.Count)
            //{
            //    _currentInputIndex = 0;
            //}
            //F.InputLanguage.CurrentInputLanguage = _allInputs[_currentInputIndex];
            //TextBlock_Content.Text = F.InputLanguage.CurrentInputLanguage.LayoutName;
            //TextBlock_Content.FontSize = 24;

            //new TcpHelper().Send(F.InputLanguage.CurrentInputLanguage.LayoutName);
        }
    }
}
