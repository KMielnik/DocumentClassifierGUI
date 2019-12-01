using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace DocumentClassifierGUI
{
    public static class DocumentClasses
    {
        public static (string Name, Brush Color) Stamp => ("Stamp", Brushes.Green);
        public static (string Name, Brush Color) Text => ("Text", Brushes.Blue);
    }
}
