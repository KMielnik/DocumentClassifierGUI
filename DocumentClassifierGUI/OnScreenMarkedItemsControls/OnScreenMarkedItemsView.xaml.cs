using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public void SetMarkedItems(List<MarkedItem> markedItems)
        {
            MarkedItems.Clear();
            markedItems.ForEach(x => MarkedItems.Add(x));
        }
    }
}
