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
    /// Interaction logic for DeletePrompt.xaml
    /// </summary>
    public partial class DeletePrompt : Window
    {

        public bool result = false;
        
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
            result = true;
            this.Close();
        }
        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SuppressDeleteWarning = ShowAgainBox.IsChecked ?? false;
            this.Close();
        }
    }
}
