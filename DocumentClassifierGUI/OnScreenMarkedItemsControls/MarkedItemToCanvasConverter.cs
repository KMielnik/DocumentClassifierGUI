using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace DocumentClassifierGUI.OnScreenMarkedItemsControls
{
    class MarkedItemToCanvasConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            (double X, double Y) desiredSize = (50, 50);

            var markedItem = value as MarkedItem;

            var canvas = new Canvas();

            var polygon = new Polygon()
            {
                Fill = markedItem.polygon.Fill,
                FillRule = markedItem.polygon.FillRule
            };

            var xmin = markedItem.polygon.Points.Min(x => x.X);
            var xmax = markedItem.polygon.Points.Max(x => x.X);

            var ymin = markedItem.polygon.Points.Min(x => x.Y);
            var ymax = markedItem.polygon.Points.Max(x => x.Y);

            var xrange = xmax - xmin;
            var yrange = ymax - ymin;

            if (xrange > yrange)
                desiredSize.Y *= yrange / xrange;
            else
                desiredSize.X *= xrange / yrange;


            foreach (var point in markedItem.polygon.Points)
            {
                var t_point = new System.Windows.Point(point.X, point.Y);

                t_point.X -= xmin;
                t_point.Y -= ymin;

                t_point.X = t_point.X / xrange * desiredSize.X;
                t_point.Y = t_point.Y / yrange * desiredSize.Y;

                if (xrange > yrange)
                    t_point.Y += (50 - desiredSize.Y) / 2;
                else
                    t_point.X += (50 - desiredSize.X) / 2;

                polygon.Points.Add(t_point);
            }

            canvas.Children.Add(polygon);

            return canvas;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
