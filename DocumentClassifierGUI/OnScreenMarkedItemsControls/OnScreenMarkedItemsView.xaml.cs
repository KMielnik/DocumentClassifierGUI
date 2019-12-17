using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DocumentClassifierGUI.OnScreenMarkedItemsControls
{
    /// <summary>
    /// Interaction logic for OnScreenMarkedItemsView.xaml
    /// </summary>
    public partial class OnScreenMarkedItemsView : UserControl, IOnScreenMarkedItemsView
    {
        public ObservableCollection<MarkedItem> MarkedItems { get; set; } = new ObservableCollection<MarkedItem>();
        public OnScreenMarkedItemsView()
        {
            InitializeComponent();
        }

        public event EventHandler<MarkedItemDeletedEventArgs> MarkedItemDeleted;

        public void SetMarkedItems(List<MarkedItem> markedItems)
        {
            MarkedItems.Clear();
            markedItems.ForEach(x => MarkedItems.Add(x));
        }

        private void TryToDeleteSelectedItem()
        {
            var selectedItem = MarkedItemsListBox.SelectedItem;

            if (selectedItem != null)
                MarkedItemDeleted?.Invoke(this, new MarkedItemDeletedEventArgs(MarkedItems[MarkedItemsListBox.Items.IndexOf(selectedItem)]));
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                TryToDeleteSelectedItem();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TryToDeleteSelectedItem();
        }
    }
}
