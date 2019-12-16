using System.Collections.Generic;

namespace DocumentClassifierGUI.OnScreenMarkedItemsControls
{
    public interface IOnScreenMarkedItemsView
    {
        public void SetMarkedItems(List<MarkedItem> markedItems);
    }
}