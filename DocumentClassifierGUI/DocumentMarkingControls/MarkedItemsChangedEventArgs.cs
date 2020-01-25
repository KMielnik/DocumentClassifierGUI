using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DocumentClassifierGUI
{
    /// <summary>
    /// Argumenty zdarzenia zmiany zawartosci listy oznaczen w dokumencie.
    /// </summary>
    public class MarkedItemsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Aktualne oznaczenia.
        /// </summary>
        public List<MarkedItem> Items;

        /// <summary>
        /// Konstruktor klasy.
        /// </summary>
        /// <param name="items">Aktualne oznaczenia.</param>
        public MarkedItemsChangedEventArgs(ObservableCollection<MarkedItem> items)
        {
            Items = items.ToList();
        }
    }
}