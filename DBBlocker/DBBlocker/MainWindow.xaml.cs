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
            if (e.Data.GetDataPresent("Object"))
            {
                // These Effects values are used in the drag source's
                // GiveFeedback event handler to determine which cursor to display.
                e.Effects = DragDropEffects.Copy;
            }
        }


        private void Toolbox_Drop(object sender, DragEventArgs e)
        {
            if(e.Handled == false)
            {
                Panel _senderPanel = (Panel)sender;
                QueryBlockBase _eleBlock = (QueryBlockBase)e.Data.GetData("QueryBlockBase");
                Panel originalParent = (Panel)e.Data.GetData("Panel");
                
                if(_senderPanel != null && _eleBlock != null)
                {
                    if (originalParent is DesignerPanel)
                    {
                        originalParent.Children.Remove(_eleBlock);
                        if(_eleBlock.GetType().BaseType.Name == "InitialBlockBase")
                        {
                            DesignerConstraintHelper.queryStarted = false;
                        }
                    }
                }
            }
        }


        private void Designer_Drop(object sender, DragEventArgs e)
        {
            // If an element in the panel has already handled the drop,
            // the panel should not also handle it.
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                QueryBlockBase _element = (QueryBlockBase)e.Data.GetData("QueryBlockBase");

                if (_panel != null && _element != null)
                {
                    //Copy the Block to its new home on the designer panel
                        
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            //Determine the type of block being dragged
                            Type blockType = _element.GetType();
                         
                            //need to create new block from the old one to copy into the children of the new panel

                            var newBlock = (QueryBlockBase)Activator.CreateInstance(blockType);
                            newBlock.StartPoint = _element.StartPoint;
                            newBlock.EnableInput();
                            // Check if the block being dragged is an initial query statement
                            // If it isn't appropriate to allow another Initial Statement at this time, ignore the drop
                            if (newBlock.GetType().BaseType.Name == "InitialBlockBase") { 
                                if(DesignerConstraintHelper.queryStarted == true)
                                {
                                    return;
                                }
                                else
                                {
                                    DesignerConstraintHelper.queryStarted = true;
                                }
                            }

                            // Nestable from blocks allow you to use more Initial statements.

                            if(newBlock.GetType().Name == "FromNestBlock")
                            {
                                DesignerConstraintHelper.queryStarted = false;
                            }
                            // Inspect the following; it works but shouldn't. May cause problems down the road.

                            if(_panel is DesignerPanel)
                            {
                                //Do not allow blocks to duplicate themselves by dragging from Designer to Designer
                                _panel.Children.Remove(_element);
                            }
                            _panel.Children.Add(newBlock);
                            // set the value to return to the DoDragDrop call
                            e.Effects = DragDropEffects.Copy;
                        } 
                }
            }
        }
    }
}
