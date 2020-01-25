using System.Collections.Generic;
using System.ComponentModel;

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    /// <summary>
    /// Klasa przedstawiająca dokument, możliwy do oznaczania oraz zawierajacy informacje potrzebne do wygenerowania dla niego maski.
    /// </summary>
    public class Document : INotifyPropertyChanged
    {
        private Status documentStatus;

        /// <summary>
        /// zwraca liste oznaczeń na dokumencie.
        /// </summary>
        public List<MarkedItem> MarkedItems { get; private set; }

        /// <summary>
        /// Nazwa dokumentu.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Status dokumentu.
        /// </summary>
        public Status DocumentStatus
        {
            get => documentStatus;
            private set
            {
                documentStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DocumentStatus"));
            }
        }

        /// <summary>
        /// Tworzy dokument z istniejacymi na nim oznaczeniami.
        /// </summary>
        /// <param name="name">Nazwa dokumentu.</param>
        /// <param name="markedItems">Oznaczenia.</param>
        public Document(string name, List<MarkedItem> markedItems) : this(name)
        {
            MarkedItems.AddRange(markedItems);
        }

        /// <summary>
        /// Tworzy pusty - nieoznaczony dokument.
        /// </summary>
        /// <param name="name">Nazwa dokumentu.</param>
        public Document(string name)
        {
            MarkedItems = new List<MarkedItem>();
            Name = name;
            DocumentStatus = Status.New;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Usuwa oznaczone elementy w dokumencie.
        /// </summary>
        public void ResetMarkedItems()
        {
            MarkedItems.Clear();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MarkedItems"));

        }

        /// <summary>
        /// Ustawia oznaczone elementy w dokumencie na te przekazane do funkcji.
        /// </summary>
        /// <param name="markedItems">Nowe oznaczenia</param>
        public void SetMarkedItems(List<MarkedItem> markedItems)
        {
            MarkedItems.Clear();
            MarkedItems.AddRange(markedItems);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MarkedItems"));
        }

        /// <summary>
        /// Dodaje jedno oznaczenie do dokumentu.
        /// </summary>
        /// <param name="markedItem">Oznaczonyt element</param>
        public void AddMarkedItem(MarkedItem markedItem)
        {
            MarkedItems.Add(markedItem);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MarkedItems"));
        }

        /// <summary>
        /// Usuwa podany w parametrze oznaczony element z dokumentu.
        /// </summary>
        /// <param name="markedItem">Element do usuniecia</param>
        public void RemoveMarkedItem(MarkedItem markedItem)
        {
            MarkedItems.Remove(markedItem);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MarkedItems"));
        }

        /// <summary>
        /// Zmienia status dokumentu na nowy - nieoznaczony.
        /// </summary>
        public void SetAsNew()
            => DocumentStatus = Status.New;

        /// <summary>
        /// Zmienia status dokumentu na oznaczony.
        /// </summary>
        public void SetAsMarked()
            => DocumentStatus = Status.Marked;

        /// <summary>
        /// Zmienia status dokumentu na dokument z maska wygenerowana do pliku.
        /// </summary>
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
