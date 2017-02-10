using System.Windows;

namespace Keyboard
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Top = SystemParameters.PrimaryScreenHeight - Height - 50;
            Left = SystemParameters.PrimaryScreenWidth - Width - 50;
        }

        private void Button_KeyBoard_Click(object sender, RoutedEventArgs e)
        {
            Popup_KeyBoard.IsOpen = !Popup_KeyBoard.IsOpen;
        }
    }
}