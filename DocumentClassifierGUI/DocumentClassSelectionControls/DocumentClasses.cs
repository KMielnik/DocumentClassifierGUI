using System.Windows.Media;

namespace DocumentClassifierGUI
{
    /// <summary>
    /// Klasa zawierajaca możliwe typy oznaczen dokumentu.
    /// </summary>
    public static class DocumentClasses
    {
        /// <summary>
        /// Pieczatka
        /// </summary>
        public static (string Name, Brush Color) Stamp => ("Stamp", Brushes.Green);
        /// <summary>
        /// Tekst drukowany.
        /// </summary>
        public static (string Name, Brush Color) Text => ("Text", Brushes.Blue);
        /// <summary>
        /// Pieczatka/Obrazek.
        /// </summary>
        public static (string Name, Brush Color) Sign => ("Sign", Brushes.Orange);
        /// <summary>
        /// Tabela.
        /// </summary>
        public static (string Name, Brush Color) Table => ("Table", Brushes.Pink);
        /// <summary>
        /// Data.
        /// </summary>
        public static (string Name, Brush Color) Data => ("Data", Brushes.Purple);
    }
}
