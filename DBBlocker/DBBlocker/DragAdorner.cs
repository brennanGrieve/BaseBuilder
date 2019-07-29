using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DBBlocker
{
    public class DragAdorner : Adorner
    {

        private Point _offset;
        private Point _location;
        //Removed : Redundant _eventOffset field
        private VisualBrush _visBrush;
        private Size _adornSize;

        public DragAdorner(UIElement adorn, Point adornOffset) : base(adorn)
        {
            _adornSize = adorn.RenderSize;
            _visBrush = new VisualBrush(adorn)
            {
                Opacity = 1
            };
            IsHitTestVisible = false;
        }

        // Get the position of the element both within the mainwindow and screen to use as an offset

        public void UpdatePosition(Point newlocation, Point eLocation)
        {
            _location = newlocation;

            AdornerLayer adorn = (AdornerLayer)Parent;

            if (adorn != null)
            {
                adorn.Update(AdornedElement);
            }

            //Generate offsets to prevent the relative position of the application window -> screen 
            //and element -> application window from causing issues with how the Adorner tracks the mouse.
            //The static values being added to the offset are to handle the offset created by the margins on UI elements.

            //TODO: maybe rig the static offset value so it can be found at runtime? Would be a lot of work for no real gain.
            //Regardless of how automated it would be, because it is tightly coupled to the UI layout by neccesity no amount of abstraction
            //could make this code 100% recyclable. 

            _offset.X = Application.Current.MainWindow.PointToScreen(new Point(0, 0)).X + eLocation.X + 50;
            _offset.Y = Application.Current.MainWindow.PointToScreen(new Point(0, 0)).Y + eLocation.Y + 50;

            this.InvalidateVisual();

        }

        protected override void OnRender(DrawingContext dc)
        {

            var p = _location;
            p.Offset(-_offset.X, -_offset.Y);
            dc.DrawRectangle(_visBrush, null, new Rect(p, _adornSize));
        }

    }
}

