using System;

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    public class DocumentSelectionChagnedEventArgs: EventArgs
    {
        public Document document { get; private set; }

        public DocumentSelectionChagnedEventArgs(Document selectedDocument)
        {
            document = selectedDocument;
        }
    }
}