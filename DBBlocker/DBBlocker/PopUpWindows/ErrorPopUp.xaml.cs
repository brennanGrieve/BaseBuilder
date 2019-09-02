using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DBBlocker
{
    /// <summary>
    /// Interaction logic for ErrorPopUp.xaml
    /// </summary>
    public partial class ErrorPopUp : Window
    {
        public ErrorPopUp()
        {
            InitializeComponent();
            SystemSounds.Asterisk.Play();
            WarningImage.Source = Imaging.CreateBitmapSourceFromHIcon(
            SystemIcons.Warning.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
