using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DBBlocker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Designer_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("QueryBlockBase"))
            {
                // These Effects values are used in the drag source's
                // GiveFeedback event handler to determine which cursor to display.
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void ToolAndTrash_Drop(object sender, DragEventArgs e)
        {
            if(e.Handled == false)
            {
                QueryBlockBase _eleBlock = (QueryBlockBase)e.Data.GetData("QueryBlockBase");
                Panel originalParent = (Panel)e.Data.GetData("Panel");       
                if(_eleBlock != null)
                {
                    DragDropConstraintHelper.ProcessToolAndTrashDragDrop(originalParent, _eleBlock);
                }
            }
        }


        private void Designer_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                QueryBlockBase _element = (QueryBlockBase)e.Data.GetData("QueryBlockBase");
                if (_panel != null && _element != null)
                {
                    if (e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                    {
                        DragDropConstraintHelper.ProcessDesignerDragDrop(_panel, _element);
                        e.Effects = DragDropEffects.Copy;
                    } 
                }
            }
        }

        private void ExecuteBtn_Click(object sender, RoutedEventArgs e)
        {
            String executableSQL = "";
            Button executer = (Button)sender;
            Grid designerGrid = (Grid)executer.Parent;
            DesignerPanel designer = (DesignerPanel)designerGrid.Children[1];
            foreach(QueryBlockBase block in designer.Children)
            {
                executableSQL += block.ExtractSQL();
            }
            executableSQL += ";";
        }
    }
}
