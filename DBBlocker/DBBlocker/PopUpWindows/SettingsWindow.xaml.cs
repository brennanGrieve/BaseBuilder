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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateSources(SettingsPanel);
            Close();
        }



        private void UpdateSources(DependencyObject current)
        {
            for(int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(current, i);
                LocalValueEnumerator props = child.GetLocalValueEnumerator();
                while (props.MoveNext())
                {
                    BindingExpression binding = BindingOperations.GetBindingExpression(child, props.Current.Property);
                    if (binding != null)
                    {
                        binding.UpdateSource();
                    }
                }
                UpdateSources(child);
            }
        }
    }
}
