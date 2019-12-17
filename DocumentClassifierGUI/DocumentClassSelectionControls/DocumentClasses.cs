using System.Windows.Media;

namespace DocumentClassifierGUI
{
    public static class DocumentClasses
    {
        public static (string Name, Brush Color) Stamp => ("Stamp", Brushes.Green);
        public static (string Name, Brush Color) Text => ("Text", Brushes.Blue);
        public static (string Name, Brush Color) Sign => ("Sign", Brushes.Orange);
        public static (string Name, Brush Color) Table => ("Table", Brushes.Pink);
        public static (string Name, Brush Color) Data => ("Data", Brushes.Purple);
    }
}
