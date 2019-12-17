using System.Windows.Media;
using System.Windows.Shapes;

namespace DocumentClassifierGUI
{
    public class MarkedItem
    {
        public string Type { get; private set; }
        public Brush Color { get; private set; }
        public Polygon polygon { get; private set; }

        public MarkedItem((string Name, Brush Color) documentClass, Polygon polygon)
        {
            Type = documentClass.Name;
            Color = documentClass.Color;

            this.polygon = polygon;
        }
    }
}
