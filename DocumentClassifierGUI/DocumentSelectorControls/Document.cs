using System.Collections.Generic;

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    public class Document
    {
        public List<MarkedItem> MarkedItems { get; private set; }
        public string Name { get; private set; }
        public Status DocumentStatus { get; private set; }

        public Document(List<MarkedItem> markedItems, string Name)
        {

        }

        public enum Status
        {
            New,
            Marked,
            MaskGenerated
        }
    }
}
