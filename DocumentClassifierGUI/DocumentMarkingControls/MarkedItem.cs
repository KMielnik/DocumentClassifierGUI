using System.Windows.Media;
using System.Windows.Shapes;

namespace DocumentClassifierGUI
{
    /// <summary>
    /// Klasa przedstawiająca pojedyńcze oznaczenie na dokumencie.
    /// </summary>
    public class MarkedItem
    {
        /// <summary>
        /// Typ oznaczenia
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Kolor oznaczenia
        /// </summary>
        public Brush Color { get; private set; }
        /// <summary>
        /// Obiekt oznaczenia.
        /// </summary>
        public Polygon polygon { get; private set; }

        /// <summary>
        /// Konstruktor oznaczenia.
        /// </summary>
        /// <param name="documentClass">Typ oznaczenia.</param>
        /// <param name="polygon">Obiekt oznaczenia zawierajacy punkty.</param>
        public MarkedItem((string Name, Brush Color) documentClass, Polygon polygon)
        {
            Type = documentClass.Name;
            Color = documentClass.Color;

            this.polygon = polygon;
        }
    }
}
