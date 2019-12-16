using System;
using System.Windows.Media;

namespace DocumentClassifierGUI
{
    public interface IDocumentMarkingView
    {
        public void SaveActualElement();
        public void SaveMaskToFile(Uri location);
        public void SetActualDocumentClass((string Name, Brush Color) newItemClass);
        public event EventHandler<MarkedItemsChangedEventArgs> MarkedItemsChanged;
    }
}