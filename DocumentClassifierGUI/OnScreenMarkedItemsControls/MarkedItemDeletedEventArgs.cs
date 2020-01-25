using System;

namespace DocumentClassifierGUI.OnScreenMarkedItemsControls
{
    /// <summary>
    /// Argumenty zdarzenia wywolywanego podczas usuwania oznaczonego elementu z listy.
    /// </summary>
    public class MarkedItemDeletedEventArgs : EventArgs
    {
        /// <summary>
        /// Element do usuniecia.
        /// </summary>
        public MarkedItem deletedItem;

        /// <summary>
        /// Konstruktor klasy.
        /// </summary>
        /// <param name="markedItem">Element do usuniecia</param>
        public MarkedItemDeletedEventArgs(MarkedItem markedItem)
        {
            deletedItem = markedItem;
        }
    }
}