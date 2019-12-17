using System;

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    public interface IDocumentSelectorView
    {
        public event EventHandler<DocumentSelectionChagnedEventArgs> DocumentSelectionChagned;
    }
}