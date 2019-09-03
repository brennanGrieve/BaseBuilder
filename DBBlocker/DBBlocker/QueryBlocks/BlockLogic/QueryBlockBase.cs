using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Resources;

namespace DBBlocker
{
    public abstract class QueryBlockBase : UserControl
    {
        protected Grid contentGrid;
        protected QueryBlockBase() { }
        private DragAdorner blockAdorner = null;
        Point _startPoint;
        private bool isFirst = false;
        public Point StartPoint { get => _startPoint; set => _startPoint = value; }
        public bool IsFirstBlock { get => isFirst; set => isFirst = value; }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Handled == false)
            {
                e.Handled = true;
                Point _currentPos = NativeMethods.GetMousePosition();
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (Math.Abs(_currentPos.X - StartPoint.X) > 15 || Math.Abs(_currentPos.Y - StartPoint.Y) > 15)
                    {                 
                        DataObject blockData = new DataObject();
                        blockData.SetData("QueryBlockBase", this);
                        blockData.SetData("Panel", Parent);
                        blockAdorner = new DragAdorner(this, e.GetPosition(this));

                        /*
                         * Force GetAdornerLayer to find the highest possible AdornerLayer to render adorners ontop of all
                         * UIElements in the Visual Tree. Then use that layer to show the Block Adorner.
                         */

                        Visual container = (Visual)Application.Current.MainWindow.Content;
                        AdornerLayer.GetAdornerLayer(container).Add(blockAdorner);
                        DragDrop.DoDragDrop(this, blockData, DragDropEffects.Copy);
                        AdornerLayer.GetAdornerLayer(container).Remove(blockAdorner);
                    }
                }
                else{
                    StartPoint = NativeMethods.GetMousePosition();
                }
            }
        }



        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
            if (e.Handled == false)
            {
                if (blockAdorner != null)
                {
                    blockAdorner.UpdatePosition(NativeMethods.GetMousePosition(), this.TranslatePoint(new Point(0, 0), Application.Current.MainWindow));
                }

                if (e.Effects.HasFlag(DragDropEffects.Copy))
                {
                    StreamResourceInfo grabCurs = Application.GetResourceStream(new Uri("Shapes/grabCursor.cur", UriKind.Relative));
                    Mouse.SetCursor(new Cursor(grabCurs.Stream));
                    //Mouse.SetCursor(Cursors.Hand);
                }
                else
                {
                    Mouse.SetCursor(Cursors.No);
                }
                e.Handled = true;
            }
        }



        



        /*
         * Code to fetch all string data from relevant UIElement objects 
         * (TextBlock, Combobox.SelectedItem, TextBox) will go here.
         * Iteration based on method found for EnableInput.
         * @return String blockSQL - This block's component of the whole query to be concatenated
         * with the data stored in other blocks.
         */

        public string ExtractSQL() {
            String blockSQL = "";
            foreach (UIElement element in contentGrid.Children)
            {
                if (element is TextBlock tbEle)
                {
                    blockSQL += tbEle.Text + " ";
                }
                if (element is TextBox tboxEle)
                {
                    blockSQL += tboxEle.Text + " ";
                }
                if(element is ComboBox cbEle)
                {
                    blockSQL += cbEle.SelectedValue + " ";
                }
                
            }
            return blockSQL;
        }

        /* 
         * Set a useable reference to this block's grid by casting the return from the Content property
         * This will be re-used by ExtractSQL, as it will always be called AFTER EnableInput whenever
         * it is possible for it to be called and thus the timing is safe.
         * Afterward, iterate through all the UIElement children of the grid and enable them all.
         */

        public void EnableInput()
        {
            contentGrid = (Grid)Content;
            foreach (UIElement ef in contentGrid.Children)
            {
                ef.IsEnabled = true;
            }
        }
    }
}
