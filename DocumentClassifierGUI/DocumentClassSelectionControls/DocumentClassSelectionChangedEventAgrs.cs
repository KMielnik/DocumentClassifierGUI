using System;
using System.Windows.Media;

namespace DocumentClassifierGUI.DocumentClassSelectionControls
{
    /// <summary>
    /// Argumenty zdarzenia Zmiany wyboru typu aktualnego oznaczenia.
    /// </summary>
    public class DocumentClassSelectionChangedEventAgrs : EventArgs
    {
        /// <summary>
        /// Nowy wybrany typ oznaczenia.
        /// </summary>
        public (string Name, Brush Color) NewClass { get; private set; }
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="newClass">Nowy wybrany typ oznaczenia.</param>
        public DocumentClassSelectionChangedEventAgrs((string Name, Brush Color) newClass)
        {
            NewClass = newClass;
        }
    }
}