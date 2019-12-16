using System;

namespace DocumentClassifierGUI.OnScreenMarkedItemsControls
{
    public class MarkedItemDeletedEventArgs : EventArgs
        {
            public MarkedItem deletedItem;

            public MarkedItemDeletedEventArgs(MarkedItem markedItem)
            {
                deletedItem = markedItem;
            }
        }
}