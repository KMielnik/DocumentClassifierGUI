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

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    /// <summary>
    /// Interaction logic for DocumentSelectorView.xaml
    /// </summary>
    public partial class DocumentSelectorView : UserControl, IDocumentSelectorView
    {
        public ObservableCollection<Document> documents { get; set; } = new ObservableCollection<Document>();
        public DocumentSelectorView()
        {
            InitializeComponent();
        }
    }
}
