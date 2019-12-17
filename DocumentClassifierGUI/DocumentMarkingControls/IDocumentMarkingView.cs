using DocumentClassifierGUI.DocumentSelectorControls;
using System;
using System.Windows.Media;

namespace DocumentClassifierGUI
{
    public interface IDocumentMarkingView
    {
        public void ChangeDocument(Document newDocument);
        public void SaveActualElement();
        public void SaveMaskToFile(Uri location);
        public void SetActualDocumentClass((string Name, Brush Color) newItemClass);
        public void DeleteMarkedItem(MarkedItem markedItem);
        public event EventHandler<MarkedItemsChangedEventArgs> MarkedItemsChanged;
    }
}