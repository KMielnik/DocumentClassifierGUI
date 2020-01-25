using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DocumentClassifierGUI.OnScreenMarkedItemsControls
{
    /// <summary>
    /// Logika interakcji dla OnScreenMarkedItemsView.xaml, pozwalajacego na zarzadzanie lista oznaczen na aktualnym dokumencie.
    /// </summary>
    public partial class OnScreenMarkedItemsView : UserControl, IOnScreenMarkedItemsView
    {
        /// <summary>
        /// Lista oznaczen.
        /// </summary>
        public ObservableCollection<MarkedItem> MarkedItems { get; set; } = new ObservableCollection<MarkedItem>();
        /// <summary>
        /// Konstruktor
        /// </summary>
        public OnScreenMarkedItemsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Zdarzenie usuniecia oznaczenia z listy.
        /// </summary>
        public event EventHandler<MarkedItemDeletedEventArgs> MarkedItemDeleted;

        /// <summary>
        /// Ustawienie na liscie aktualnych oznaczen dokumentu.
        /// </summary>
        /// <param name="markedItems">Oznaczenia do umieszczenia na liscie.</param>
        public void SetMarkedItems(List<MarkedItem> markedItems)
        {
            MarkedItems.Clear();
            markedItems.ForEach(x => MarkedItems.Add(x));
        }

        /// <summary>
        /// Proboje usunac dany oznaczony element z dokumentu.
        /// </summary>
        private void TryToDeleteSelectedItem()
        {
            var selectedItem = MarkedItemsListBox.SelectedItem;

            if (selectedItem != null)
                MarkedItemDeleted?.Invoke(this, new MarkedItemDeletedEventArgs(MarkedItems[MarkedItemsListBox.Items.IndexOf(selectedItem)]));
        }

        /// <summary>
        /// Obsluguje usuniecie obiektu przy pomocy klawisza delete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                TryToDeleteSelectedItem();
        }

        /// <summary>
        /// Obsluguje usuniecie obiektu przy pomocy przycisku w GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TryToDeleteSelectedItem();
        }
    }
}
