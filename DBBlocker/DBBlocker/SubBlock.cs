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
    class SubBlock : Shape
    {
        protected override Geometry DefiningGeometry
        {
            get
            {
                CombinedGeometry newShapeComponent = new CombinedGeometry
                {
                    GeometryCombineMode = GeometryCombineMode.Union,
                    Geometry1 = new EllipseGeometry(new Point(600, 150), 100, 100),
                    Geometry2 = new RectangleGeometry(new Rect(0, 0, 600, 300))
                };
                CombinedGeometry finalNewShape = new CombinedGeometry
                {
                    GeometryCombineMode = GeometryCombineMode.Exclude,
                    Geometry1 = newShapeComponent,
                    Geometry2 = new EllipseGeometry(new Point(0, 150), 100, 100)

                };
                return finalNewShape;
            }
        }
    }
}
