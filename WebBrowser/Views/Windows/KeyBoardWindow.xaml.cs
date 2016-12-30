using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WebBrowserWPF.Views.Windows
{
    public partial class KeyBoardWindow : Window
    {
        public KeyBoardWindow()
        {
            InitializeComponent();

            Top = SystemParameters.PrimaryScreenHeight - Height - 50;
            Left = SystemParameters.PrimaryScreenWidth - Width - 50;

            Popup_KeyBoard.CustomPopupPlacementCallback = new CustomPopupPlacementCallback(placePopup);
        }

        private CustomPopupPlacement[] placePopup(Size popupSize, Size targetSize, Point offset)
        {
            CustomPopupPlacement placement1 = new CustomPopupPlacement(new Point(0, 0), PopupPrimaryAxis.None);

            CustomPopupPlacement[] ttplaces = new CustomPopupPlacement[] { placement1 };

            return ttplaces;
        }

        private void Button_KeyBoard_Click(object sender, RoutedEventArgs e)
        {
            Popup_KeyBoard.IsOpen = !Popup_KeyBoard.IsOpen;
        }
    }
}
