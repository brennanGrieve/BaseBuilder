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
    class NestableBlock : Shape
    {
        protected override Geometry DefiningGeometry
        {
            get
            {
                CombinedGeometry newShape = new CombinedGeometry
                {
                    Geometry1 = new RectangleGeometry(new Rect(0, 0, 150, 75)),
                    Geometry2 = new RectangleGeometry(new Rect(115, -10, 50, 95)),
                    GeometryCombineMode = GeometryCombineMode.Union
                };
                CombinedGeometry newShapeComponent = new CombinedGeometry
                {
                    Geometry1 = newShape,
                    Geometry2 = new RectangleGeometry(new Rect(135, 0, 50, 75)),
                    GeometryCombineMode = GeometryCombineMode.Exclude
                };
                CombinedGeometry finalNewShape = new CombinedGeometry
                {
                    GeometryCombineMode = GeometryCombineMode.Exclude,
                    Geometry1 = newShapeComponent,
                    Geometry2 = new EllipseGeometry(new Point(0, 37.5), 25, 25)

                };
                return finalNewShape;
            }
        }
    }
}
