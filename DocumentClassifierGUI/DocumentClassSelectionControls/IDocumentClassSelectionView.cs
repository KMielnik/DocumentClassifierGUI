using System;

namespace DocumentClassifierGUI.DocumentClassSelectionControls
{
    public interface IDocumentClassSelectionView
    {
        public event EventHandler<DocumentClassSelectionChangedEventAgrs> SelectionChanged;
    }
}