using System.Drawing;
using System.Media;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace DBBlocker
{
    /// <summary>
    /// Interaction logic for DeletePrompt.xaml
    /// </summary>
    public partial class DeletePrompt : Window
    {

        
        public DeletePrompt()
        {
            
            InitializeComponent();
            SystemSounds.Asterisk.Play();
            ShowAgainBox.IsChecked = Properties.Settings.Default.SuppressDeleteWarning;
            WarningImage.Source = Imaging.CreateBitmapSourceFromHIcon(
            SystemIcons.Warning.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SuppressDeleteWarning = ShowAgainBox.IsChecked ?? false;
            DialogResult = true;
        }
        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SuppressDeleteWarning = ShowAgainBox.IsChecked ?? false;
            DialogResult = false;
        }
    }
}
