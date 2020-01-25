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
            await this.ShowMessageAsync("Instrukcje", "Lewym oznaczanie\nPrawym uswanie ostatniego punktu\nScroll myszki dziala do przyblizania\nSrodkowym myszki trzymanym mozna przesuwac dokument\nEnter potwierdza aktualny obiekt\nTab zapisuje maske do pliku\n\nNIE ZMIENIAC WIELKOSCI OKNA PODCZAS OZNACZANIA BO SIE ZBUGUJE\nElementy sie rozjada bo ich nie skaluje przy zmianie okna :)\n\nJak czasami enter nie dziala, zmienic maske na inna i spowrotem, odblokuje sie.\nW podgladzie plikow oznaczone z zapisana maska sa na zielono, na zolto aktualnie oznaczane w programie na czerwono te do zrobienia jeszcze.");
        }
    }
}
