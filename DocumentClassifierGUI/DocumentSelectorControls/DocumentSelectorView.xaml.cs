using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DocumentClassifierGUI.DocumentSelectorControls
{
    /// <summary>
    /// Interaction logic for DocumentSelectorView.xaml
    /// </summary>
    public partial class DocumentSelectorView : UserControl, IDocumentSelectorView
    {
        public readonly string DocumentsFolderName = @"documents";
        public readonly string MasksFolderName = @"masks";

        public event EventHandler<DocumentSelectionChagnedEventArgs> DocumentSelectionChagned;

        public ObservableCollection<Document> documents { get; set; } = new ObservableCollection<Document>();
        public DocumentSelectorView()
        {
            InitializeComponent();

            LoadDocuments();
        }

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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DocumentSelectionChagned?.Invoke(this, new DocumentSelectionChagnedEventArgs(e.AddedItems[0] as Document));
        }
    }
}
