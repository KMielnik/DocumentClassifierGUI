using System;
using System.Windows.Media;

namespace DocumentClassifierGUI.DocumentClassSelectionControls
{
    public class DocumentClassSelectionChangedEventAgrs : EventArgs
        {
            public (string Name, Brush Color) NewClass { get; private set; }
            public DocumentClassSelectionChangedEventAgrs((string Name, Brush Color) newClass)
            {
                NewClass = newClass;
            }
        }
}