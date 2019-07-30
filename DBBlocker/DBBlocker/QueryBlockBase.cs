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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace DBBlocker
{
    public abstract class QueryBlockBase : UserControl
    {

        protected QueryBlockBase() { }

        DragAdorner blockAdorner = null;



        protected DataObject PackageData(QueryBlockBase toExtract)
        {
            DataObject blockData = new DataObject();
            blockData.SetData(DataFormats.StringFormat, toExtract.ExtractSQL());
            blockData.SetData("Object", this);
            return blockData;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject blockData = new DataObject();
                //blockData.SetData(DataFormats.StringFormat, ExtractSQL());
                blockData.SetData("Object", this);

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

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);

            if (blockAdorner != null)
            {
                blockAdorner.UpdatePosition(GetMousePosition(), this.TranslatePoint(new Point(0, 0), Application.Current.MainWindow));
            }

            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                Mouse.SetCursor(Cursors.Cross);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        public abstract string ExtractSQL();

        public abstract void EnableInput();
    }
}
