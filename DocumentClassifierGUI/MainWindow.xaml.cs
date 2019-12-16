using DocumentClassifierGUI.DocumentClassSelectionControls;
using DocumentClassifierGUI.OnScreenMarkedItemsControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DocumentClassifierGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDocumentMarkingView documentMarkingView;
        IDocumentClassSelectionView documentClassSelectionView;
        IOnScreenMarkedItemsView onScreenMarkedItemsView;
        public MainWindow()
        {
            InitializeComponent();

            DocumentMarkingViewSetup();
            DocumentClassSelectionSetup();
            OnScreenMarkedItemsViewSetup();
        }

        private void OnScreenMarkedItemsViewSetup()
        {
            onScreenMarkedItemsView = new OnScreenMarkedItemsView();

            onScreenMarkedItemsView.MarkedItemDeleted += (s, e) => documentMarkingView.DeleteMarkedItem(e.deletedItem);

            OnScreenMarkedItemsControl.Content = onScreenMarkedItemsView;
        }

        private void DocumentClassSelectionSetup()
        {
            documentClassSelectionView = new DocumentClassSelectionView();

            documentClassSelectionView.SelectionChanged += DocumentClassSelectionView_SelectionChanged;

            DocumentClassSelectionControl.Content = documentClassSelectionView;
        }

        private void DocumentClassSelectionView_SelectionChanged(object sender, DocumentClassSelectionChangedEventAgrs e)
        {
            documentMarkingView.SetActualDocumentClass(e.NewClass);
        }

        private void DocumentMarkingViewSetup()
        {
            documentMarkingView = new DocumentMarkingView();

            documentMarkingView.MarkedItemsChanged += DocumentMarkingView_MarkedItemsChanged;

            var zoomableView = new ZoomableControlView(documentMarkingView);
            DocumentMarkingZoomableControl.Content = zoomableView;
        }

        private void DocumentMarkingView_MarkedItemsChanged(object sender, MarkedItemsChangedEventArgs e)
        {
            onScreenMarkedItemsView.SetMarkedItems(e.Items);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                documentMarkingView.SaveActualElement();
            else if (e.Key == Key.Tab)
                documentMarkingView.SaveMaskToFile(new Uri("mask.png", UriKind.Relative));
        }
    }
}
