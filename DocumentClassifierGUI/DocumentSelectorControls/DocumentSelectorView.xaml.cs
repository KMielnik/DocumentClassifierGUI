using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    /// <summary>
    /// Logika interakcji dla DocumentSelectorView.xaml, umożliwiajaca wybieranie dokumentu do oznaczania z folderu documents.
    /// </summary>
    public partial class DocumentSelectorView : UserControl, IDocumentSelectorView
    {
        /// <summary>
        /// Folder z dokumentami do oznaczenia.
        /// </summary>
        public readonly string DocumentsFolderName = @"documents";
        /// <summary>
        /// Folder z maskami dla oznaczonych dokumentow.
        /// </summary>
        public readonly string MasksFolderName = @"masks";

        /// <summary>
        /// Zdarzenie wybrania nowego dokumentu z listy.
        /// </summary>
        public event EventHandler<DocumentSelectionChagnedEventArgs> DocumentSelectionChagned;

        /// <summary>
        /// Dokumenty dostepne w folderze dokumentow.
        /// </summary>
        public ObservableCollection<Document> documents { get; set; } = new ObservableCollection<Document>();
        public DocumentSelectorView()
        {
            InitializeComponent();

            LoadDocuments();
        }

        /// <summary>
        /// Ładowanie dokumentow z folderu dokumentów do listy, wraz z ich statusem.
        /// </summary>
        private void LoadDocuments()
        {
            Directory.CreateDirectory(DocumentsFolderName);
            Directory.CreateDirectory(MasksFolderName);

            Directory.GetFiles(DocumentsFolderName)
                .Select(x => Regex.Match(x, @"[\\].+[.]").Value)
                .Select(x => x.Replace(".", string.Empty))
                .Select(x => x.Replace("\\", string.Empty))
                .OrderBy(x => { int.TryParse(x, out int o); return o; })
                .Select(x => new Document(x))
                .ToList()
                .ForEach(x => documents.Add(x));

            Directory.GetFiles(MasksFolderName)
                .Select(x => Regex.Match(x, @"[\\].+[.]").Value)
                .Select(x => x.Replace(".", string.Empty))
                .Select(x => x.Replace("\\", string.Empty))
                .ToList()
                .ForEach(x =>
                {
                    var found = documents.FirstOrDefault(document => document.Name == x);
                    if (found != null)
                        found.SetAsMaskGenerated();
                });
        }

        /// <summary>
        /// Oblsuga zdarzenia zmiany wybranego dokumentu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DocumentSelectionChagned?.Invoke(this, new DocumentSelectionChagnedEventArgs(e.AddedItems[0] as Document));
        }
    }
}
