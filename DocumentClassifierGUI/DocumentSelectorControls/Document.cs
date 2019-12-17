using System.Collections.Generic;
using System.ComponentModel;

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    public class Document : INotifyPropertyChanged
    {
        private Status documentStatus;

        public List<MarkedItem> MarkedItems { get; private set; }

        public string Name { get; private set; }
        public Status DocumentStatus
        {
            get => documentStatus;
            private set
            {
                documentStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DocumentStatus"));
            }
        }

        public Document(string name, List<MarkedItem> markedItems) : this(name)
        {
            MarkedItems.AddRange(markedItems);
        }

        public Document(string name)
        {
            MarkedItems = new List<MarkedItem>();
            Name = name;
            DocumentStatus = Status.New;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ResetMarkedItems()
        {
            MarkedItems.Clear();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MarkedItems"));

        }

        public void SetMarkedItems(List<MarkedItem> markedItems)
        {
            MarkedItems.Clear();
            MarkedItems.AddRange(markedItems);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MarkedItems"));
        }

        public void AddMarkedItem(MarkedItem markedItem)
        {
            MarkedItems.Add(markedItem);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MarkedItems"));
        }

        public void RemoveMarkedItem(MarkedItem markedItem)
        {
            MarkedItems.Remove(markedItem);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MarkedItems"));
        }

        public void SetAsNew()
            => DocumentStatus = Status.New;

        public void SetAsMarked()
            => DocumentStatus = Status.Marked;

        public void SetAsMaskGenerated()
            => DocumentStatus = Status.MaskGenerated;


        public enum Status
        {
            New,
            Marked,
            MaskGenerated
        }
    }
}
