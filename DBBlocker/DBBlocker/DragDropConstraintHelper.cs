using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DBBlocker
{
    class DragDropConstraintHelper
    {
        public static bool queryStarted;

        DragDropConstraintHelper() { }

        /**
         * public static void ProcessToolAndTrashDragDrop
         * Implements proper constraints on deletion by dragging blocks into the Toolbox or Trash, then performs the
         * appropriate deletion operation.
         * @param[in] originalParent - The Parent of the block being dragged
         * @param[in] _eleBlock - The block being dragged
         */


        public static void ProcessToolAndTrashDragDrop(Panel originalParent, QueryBlockBase _eleBlock)
        {
            if (originalParent is DesignerPanel)
            {
                if (_eleBlock.GetType().BaseType.Name == "InitialBlockBase")
                {
                    queryStarted = false;
                    if (_eleBlock.IsFirstBlock)
                    {
                        originalParent.Children.RemoveRange(0, originalParent.Children.Count);
                        return;
                    }
                }
                if (_eleBlock.GetType().Name == "FromNestBlock")
                {
                    queryStarted = true;
                    int index = originalParent.Children.IndexOf(_eleBlock);
                    originalParent.Children.RemoveRange(index, originalParent.Children.Count - index);
                }
                originalParent.Children.Remove(_eleBlock);
            }
        }

        /**
         * public static void ProcessDesignerDragDrop
         * Processes all constraints currently applied to the DesignerPanel and executes the appropriate 
         * drag + drop operation. Called from the Designer_Drop handler in MainWindow.xaml.cs. Returns no values.
         * @param[in] _panel - The panel that fired the drop event handler.
         * @param[in] _element - The QueryBlockBase type UIElement that is the target of the drag/drop operation
         **/

        public static void ProcessDesignerDragDrop(Panel _panel, Panel originalParent, QueryBlockBase _element)
        {
            Type blockType = _element.GetType();
            var newBlock = (QueryBlockBase)Activator.CreateInstance(blockType);
            newBlock.StartPoint = _element.StartPoint;
            newBlock.EnableInput();
            bool isInitial = newBlock.GetType().BaseType.Name == "InitialBlockBase";
            if (!queryStarted)
            {
                if (isInitial)
                {
                    if (_panel.Children.Count == 0) { newBlock.IsFirstBlock = true; }
                    queryStarted = true;
                    _panel.Children.Add(newBlock);
                }
                else { System.Windows.MessageBox.Show("Please use a Red block (Select/Update/Add Etc.) to begin a Query.", "Invalid Block Type", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning); }
            }
            else if (queryStarted)
            {
                if (originalParent is DesignerPanel == false)
                {
                    if (!isInitial)
                    {
                        if (queryStarted)
                        {
                            if (newBlock.GetType().Name == "FromNestBlock")
                            {
                                queryStarted = false;
                            }
                            _panel.Children.Add(newBlock);
                        }
                    }
                    else { System.Windows.MessageBox.Show("Red blocks can only be used to commence a Query or SubQuery.", "Invalid Block Type", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning); }
                }
            }
        }
    }
}
