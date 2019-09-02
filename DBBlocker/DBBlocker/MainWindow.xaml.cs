using System;
using System.Collections;
using System.Data;
using System.Data.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
                    DesignerPanelHelper.ProcessDeleteDragDrop(originalParent, _eleBlock);
                }
            }
        }


        private void Designer_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                QueryBlockBase _element = (QueryBlockBase)e.Data.GetData("QueryBlockBase");
                Panel originalParent = (Panel)e.Data.GetData("Panel");
                if (_panel != null && _element != null)
                {
                    if (e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                    {
                        DesignerPanelHelper.ProcessDesignerDragDrop(_panel, originalParent, _element);
                        e.Effects = DragDropEffects.Copy;
                    } 
                }
            }
        }

        private void ExecuteBtn_Click(object sender, RoutedEventArgs e)
        {
            OutputView.Items.Refresh();
            Button ele = (Button)sender;
            PlayRippleAnim(ele, "RunRipple");
            string executableSQL = "";
            
            DesignerPanel designer = (DesignerPanel)LogicalTreeHelper.FindLogicalNode(ele.Parent, "Designer");
            foreach (QueryBlockBase block in designer.Children)
            {
                executableSQL += block.ExtractSQL();
            }
            if(executableSQL == "")
            {
                
                Application.Current.Resources["ErrorOutput"] = "Please Build a Query using the blocks from the toolbox before running.";
                ErrorPopUp newPopUp = new ErrorPopUp();
                newPopUp.ShowDialog();
                return;
            }
            executableSQL += ";";
            if (executableSQL.StartsWith("SELECT")) { DatabaseHelper.RunReaderSQL(executableSQL); }
            else{ DatabaseHelper.RunSQL(executableSQL); }
        }

        private void Trash_Click(object sender, RoutedEventArgs e)
        {
            Button ele = (Button)sender;
            PlayRippleAnim(ele, "TrashRipple");
            DesignerPanel designer = (DesignerPanel)LogicalTreeHelper.FindLogicalNode(ele.Parent, "Designer");
            if (!Properties.Settings.Default.SuppressDeleteWarning)
            {
                DeletePrompt prompt = new DeletePrompt();
                prompt.ShowDialog();
                if (prompt.result == true)
                {
                    designer.Children.RemoveRange(0, designer.Children.Count);
                    DesignerPanelHelper.queryStarted = false;
                }
            }
            else
            {
                designer.Children.RemoveRange(0, designer.Children.Count);
                DesignerPanelHelper.queryStarted = false;
            }
        }


        protected void PlayRippleAnim(Button toAnimate, string target)
        {
            Grid buttonGrid = (Grid)toAnimate.Content;
            Ellipse toAnim = (Ellipse)LogicalTreeHelper.FindLogicalNode(toAnimate, target);
            toAnim.BeginAnimation(HeightProperty, new DoubleAnimation()
            {
                From = 0,
                To = 70,
                FillBehavior = FillBehavior.Stop,
                Duration = new Duration(TimeSpan.FromSeconds(0.4))

            });
            toAnim.BeginAnimation(WidthProperty, new DoubleAnimation()
            {
                From = 0,
                To = 90,
                FillBehavior = FillBehavior.Stop,

                Duration = new Duration(TimeSpan.FromSeconds(0.4))
            });
            toAnim.BeginAnimation(OpacityProperty, new DoubleAnimation()
            {
                To = 0,
                FillBehavior = FillBehavior.Stop,
                Duration = new Duration(TimeSpan.FromSeconds(0.4))
            });
            toAnim.Height = 0;
            toAnim.Width = 0;
            toAnim.Opacity = 1;
        }

        private void CustomBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            e.Handled = true;
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuUserSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.ShowDialog();
        }
    }
}
