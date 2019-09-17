using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DBBlocker
{
    /// <summary>
    /// Interaction logic for TutorialModeMenu.xaml
    /// </summary>
    public partial class TutorialModeMenu : Window
    {

        int returnFlag;
        public TutorialModeMenu()
        {
            InitializeComponent();
        }


        public int ReturnFlag { get => returnFlag; set => returnFlag = value; }

        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnFlag = UserSelectionBox.SelectedIndex;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
