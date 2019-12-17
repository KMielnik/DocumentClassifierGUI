using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DocumentClassifierGUI
{
    public class MarkedItemsChangedEventArgs : EventArgs
    {
        public List<MarkedItem> Items;

        public MarkedItemsChangedEventArgs(ObservableCollection<MarkedItem> items)
        {
            Items = items.ToList();
        }
    }
}