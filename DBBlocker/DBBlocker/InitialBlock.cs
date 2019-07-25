using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DBBlocker
{
    class InitialBlock : Shape
    {
        protected override Geometry DefiningGeometry
        {
            get {
                CombinedGeometry newShape = new CombinedGeometry
                {
                    Geometry1 = new EllipseGeometry(new Point(600, 150), 100, 100),
                    Geometry2 = new RectangleGeometry(new Rect(0,0,600,300))
                };
                return newShape;
            }
        }
    }
}
