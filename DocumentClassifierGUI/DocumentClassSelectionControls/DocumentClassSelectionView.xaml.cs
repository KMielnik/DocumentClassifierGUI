﻿using System;
using System.Windows.Controls;

namespace DocumentClassifierGUI.DocumentClassSelectionControls
{
    /// <summary>
    /// Interaction logic for DocumentClassSelectionView.xaml
    /// </summary>
    public partial class DocumentClassSelectionView : UserControl, IDocumentClassSelectionView
    {
        public DocumentClassSelectionView()
        {
            InitializeComponent();
        }

        public event EventHandler<DocumentClassSelectionChangedEventAgrs> SelectionChanged;

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
