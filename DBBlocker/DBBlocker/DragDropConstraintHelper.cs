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


        public static void ProcessToolBoxDragDrop(Panel originalParent, QueryBlockBase _eleBlock)
        {
            if (originalParent is DesignerPanel)
            {
                /** 
                 * When a nestable FROM block is removed, all blocks afterward should be removed.
                 * Iterate through each, and once the nested from block is found begin removing it and all
                 *subsequent blocks.
                 */
                if (_eleBlock.GetType().BaseType.Name == "InitialBlockBase")
                {
                    queryStarted = false;
                    if (_eleBlock.IsFirstBlock)
                    {
                        //remove all blocks from the query then return as there is nothing left to do
                        originalParent.Children.RemoveRange(0, originalParent.Children.Count);
                        return;
                    }
                }
                if (_eleBlock.GetType().Name == "FromNestBlock")
                {
                    queryStarted = true;
                    int index = originalParent.Children.IndexOf(_eleBlock);
                    index++;
                    if (originalParent.Children.Count > index)
                    {
                        originalParent.Children.RemoveAt(index);
                    }
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

        public static void ProcessDesignerDragDrop(Panel _panel, QueryBlockBase _element)
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
                    if(_panel.Children.Count == 0) { newBlock.IsFirstBlock = true; }
                    int foo = _panel.Children.Count;
                    queryStarted = true;
                    _panel.Children.Add(newBlock);
                }
            }
            else if (queryStarted)
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
            }

            if (_panel is DesignerPanel && !isInitial)
            {
                //Do not allow blocks to duplicate themselves by dragging from Designer to Designer
                _panel.Children.Remove(_element);
            }
        }
    }
}
