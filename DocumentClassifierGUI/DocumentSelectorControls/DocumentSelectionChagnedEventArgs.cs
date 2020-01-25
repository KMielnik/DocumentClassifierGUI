using System;

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    /// <summary>
    /// Klasa zawierajaca argumenty dla zdarzenia wybrania dokumentu na liscie.
    /// </summary>
    public class DocumentSelectionChagnedEventArgs : EventArgs
    {
        /// <summary>
        /// Wybrany dokument.
        /// </summary>
        public Document document { get; private set; }

        /// <summary>
        /// Konstruktor przyjmujacy wybrany dokument.
        /// </summary>
        /// <param name="selectedDocument"></param>
        public DocumentSelectionChagnedEventArgs(Document selectedDocument)
        {
            document = selectedDocument;
        }
    }
}