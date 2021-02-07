using DocumentClassifierGUI.DocumentClassSelectionControls;
using DocumentClassifierGUI.DocumentSelectorControls;
using DocumentClassifierGUI.OnScreenMarkedItemsControls;
using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace DocumentClassifierGUI
{
    /// <summary>
    /// Logika interakcji dla MainWindow.xaml, glownego widoku programu.
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Widok elementu interfejsu pozwalajacego na oznaczanie dokumentu.
        /// </summary>
        IDocumentMarkingView documentMarkingView;
        /// <summary>
        /// Widok elementu interfejsu pozwalajacego na wybor typu aktualnie oznaczanego elementu.
        /// </summary>
        IDocumentClassSelectionView documentClassSelectionView;
        /// <summary>
        /// Widok elementu interfejsu pozwalajacego na zarzadzanie oznaczeniami na aktualnym dokumencie.
        /// </summary>
        IOnScreenMarkedItemsView onScreenMarkedItemsView;
        /// <summary>
        /// Widok elementu interfejsu pozwalajacego na wybor oznaczanego dokumentu.
        /// </summary>
        IDocumentSelectorView documentSelectorView;

        /// <summary>
        /// Konstruktor przygotowujacy wszystkie elementy interfejsu.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DocumentMarkingViewSetup();
            DocumentClassSelectionSetup();
            OnScreenMarkedItemsViewSetup();
            DocumentSelectorViewSetup();
        }

        /// <summary>
        /// Przygotowywanie elementu interfejsu pozwalajacego na wybor oznaczanego dokumentu.
        /// </summary>
        private void DocumentSelectorViewSetup()
        {
            documentSelectorView = new DocumentSelectorView();

            documentSelectorView.DocumentSelectionChagned += (s, e) => documentMarkingView.ChangeDocument(e.document);

            DocumentSelectorControl.Content = documentSelectorView;
        }

        /// <summary>
        /// Przygotowywanie elementu interfejsu pozwalajacego na zarzadzanie oznaczeniami na aktualnym dokumencie.
        /// </summary>
        private void OnScreenMarkedItemsViewSetup()
        {
            onScreenMarkedItemsView = new OnScreenMarkedItemsView();

            onScreenMarkedItemsView.MarkedItemDeleted += (s, e) => documentMarkingView.DeleteMarkedItem(e.deletedItem);

            OnScreenMarkedItemsControl.Content = onScreenMarkedItemsView;
        }

        /// <summary>
        /// Przygotowywanie elementu interfejsu pozwalajacego na wybor typu aktualnie oznaczanego elementu.
        /// </summary>
        private void DocumentClassSelectionSetup()
        {
            documentClassSelectionView = new DocumentClassSelectionView();

            documentClassSelectionView.SelectionChanged += DocumentClassSelectionView_SelectionChanged;

            DocumentClassSelectionControl.Content = documentClassSelectionView;
        }

        /// <summary>
        /// Obsluga zdarzenia zmiany wyboru typu dokumentu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentClassSelectionView_SelectionChanged(object sender, DocumentClassSelectionChangedEventAgrs e)
        {
            documentMarkingView.SetActualDocumentClass(e.NewClass);
        }

        /// <summary>
        /// Przygotowanie elementu interfejsu pozwalajacego na oznaczanie dokumentu.
        /// </summary>
        private void DocumentMarkingViewSetup()
        {
            documentMarkingView = new DocumentMarkingView();

            documentMarkingView.MarkedItemsChanged += DocumentMarkingView_MarkedItemsChanged;

            var zoomableView = new ZoomableControlView(documentMarkingView);
            DocumentMarkingZoomableControl.Content = zoomableView;
        }

        /// <summary>
        /// Obluga zdarzenia zmiany listy aktualnych oznaczen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentMarkingView_MarkedItemsChanged(object sender, MarkedItemsChangedEventArgs e)
        {
            onScreenMarkedItemsView.SetMarkedItems(e.Items);
        }

        /// <summary>
        /// Obsluga zdarzenia klawiszy enter oraz tabulacji.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                documentMarkingView.SaveActualElement();
            else if (e.Key == Key.Tab)
                documentMarkingView.SaveMaskToFile(new Uri("mask.png", UriKind.Relative));
        }

        /// <summary>
        /// Obsluga zdarzenia wcisniecia przycisku wyswietlajacego okienko z informacjami dla uzytkownika aplikacji.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void InstructionsButton_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("Instructions", 
                "LPM click or draw - place new point\n" +
                "PPM - delete last drawn point/section\n" +
                "Scroll - zoom in/out\n" +
                "Hold middle mouse button - move zoomed in document\n" +
                "Enter - finish drawing actual mask\n" +
                "Tab - generate mask for actual document\n\n" +
                "Place documents to mark in directory \"documents\" next to programs .exe\n" +
                "Masks are saved in directory \"masks\"\n\n" +
                "Warning:\n" +
                "Changing screen size will break masks on actually edited document\n" +
                "Sometimes saving mask breaks (enter), to fix, change mask type to another and back to the desired one\n\n" +
                "Statuses:\n" +
                "Red document: mask not generated, no temp masks\n" +
                "Yellow document: mask not generated, document is being edited\n" +
                "Green document: mask generated and saved to file");
        }
    }
}
