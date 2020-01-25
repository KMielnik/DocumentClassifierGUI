using System;
using System.Windows.Controls;

namespace DocumentClassifierGUI.DocumentClassSelectionControls
{
    /// <summary>
    /// Logika interackcji dla DocumentClassSelectionView.xaml, pozwalajacego na zmiane typu aktualnego oznaczenia.
    /// </summary>
    public partial class DocumentClassSelectionView : UserControl, IDocumentClassSelectionView
    {
        /// <summary>
        /// Konstruktor klasy,
        /// </summary>
        public DocumentClassSelectionView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Zdarzenie zmiany wyboru typu oznaczenia.
        /// </summary>
        public event EventHandler<DocumentClassSelectionChangedEventAgrs> SelectionChanged;

        /// <summary>
        /// Oblsuga zdarzenia zmiany wyboru typu oznaczenia.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentClassListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBoxItem = (ListBoxItem)e.AddedItems[0];

            var stackpanel = (StackPanel)listBoxItem.Content;

            var text = ((TextBlock)stackpanel.Children[1]).Text;

            var actualItemClass = text switch
            {
                "Text" => DocumentClasses.Text,
                "Sign" => DocumentClasses.Sign,
                "Stamp" => DocumentClasses.Stamp,
                "Table" => DocumentClasses.Table,
                "Data" => DocumentClasses.Data,
                _ => throw new ApplicationException("This document class has not been implemented yet.")
            };

            SelectionChanged?.Invoke(this, new DocumentClassSelectionChangedEventAgrs(actualItemClass));
        }
    }
}
