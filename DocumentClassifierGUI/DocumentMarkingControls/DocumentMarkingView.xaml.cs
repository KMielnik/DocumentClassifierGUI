using DocumentClassifierGUI.DocumentSelectorControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DocumentClassifierGUI
{
    /// <summary>
    /// Logika interakcji dla DocumentMarkingView.xaml, umożliwiajacego oznaczanie dokumentu.
    /// </summary>
    public partial class DocumentMarkingView : UserControl, IDocumentMarkingView
    {
        /// <summary>
        /// aktualnie oznaczany dokument.
        /// </summary>
        private Document actualDocument;
        /// <summary>
        /// aktualne wykonywane oznaczenie.
        /// </summary>
        private Polygon actualPolygon;
        /// <summary>
        /// Typ atkualnego oznaczenia.
        /// </summary>
        private (string Name, Brush Color) actualItemClass;

        /// <summary>
        /// Punkty wykonanej interakcji od uzytkownika, uzywane podczas operacji cofania.
        /// </summary>
        private ObservableCollection<Point> actualPolygonCheckpoints = new ObservableCollection<Point>();
        /// <summary>
        /// Punkty do wyswietlania w GUI
        /// </summary>
        private List<Ellipse> points = new List<Ellipse>();

        /// <summary>
        /// Aktualnie znajdujace sie oznaczenia na ekranie.
        /// </summary>
        private ObservableCollection<MarkedItem> markedItems = new ObservableCollection<MarkedItem>();

        /// <summary>
        /// Event wyzwalany w przypadku zmiany listy oznaczen.
        /// </summary>
        public event EventHandler<MarkedItemsChangedEventArgs> MarkedItemsChanged;

        /// <summary>
        /// Inicjalizacja klasy.
        /// </summary>
        public DocumentMarkingView()
        {
            InitializeComponent();

            actualItemClass = DocumentClasses.Text;
            resetActualPolygon();


            actualPolygonCheckpoints.CollectionChanged += ActualPolygonCheckpoints_CollectionChanged;
            markedItems.CollectionChanged += MarkedItems_CollectionChanged;
            markedItems.CollectionChanged += (s, e) => MarkedItemsChanged?.Invoke(s, new MarkedItemsChangedEventArgs(markedItems));
        }

        /// <summary>
        /// Obsluga zmiany listy oznaczen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (MarkedItem newItem in e.NewItems)
                {
                    DocumentSurface.Children.Add(newItem.polygon);
                    actualDocument.AddMarkedItem(newItem);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (MarkedItem deletedItem in e.OldItems)
                {
                    DocumentSurface.Children.Remove(deletedItem.polygon);
                    actualDocument.RemoveMarkedItem(deletedItem);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                DocumentSurface.Children
                    .OfType<Polygon>()
                    .ToList()
                    .ForEach(x => DocumentSurface.Children.Remove(x));
            }
        }

        /// <summary>
        /// Obsluga zdarzenia interakcji od uzytkownika dodajacej punkt kontrolny.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActualPolygonCheckpoints_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var point in points)
                DocumentSurface.Children.Remove(point);

            points.Clear();

            foreach (var point in actualPolygonCheckpoints)
            {
                var elipse = new Ellipse()
                {
                    Width = 6,
                    Height = 6,
                    Stroke = Brushes.Blue,
                    Fill = Brushes.Red,
                    StrokeThickness = 3
                };

                points.Add(elipse);

                DocumentSurface.Children.Add(elipse);

                Canvas.SetLeft(elipse, point.X - elipse.Width / 2);
                Canvas.SetTop(elipse, point.Y - elipse.Height / 2);
            }
        }
        /// <summary>
        /// Resetuje aktualne oznaczenie.
        /// </summary>
        private void resetActualPolygon()
        {
            DocumentSurface.Children.Remove(actualPolygon);

            actualPolygon = new Polygon()
            {
                Stroke = Brushes.Black,
                Fill = actualItemClass.Color,
                FillRule = FillRule.Nonzero,
                Opacity = 0.6
            };

            actualPolygonCheckpoints.Clear();
            DocumentSurface.Children.Add(actualPolygon);
        }

        /// <summary>
        /// Obsluga zdarzenia klikniecia myszka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var newPoint = e.GetPosition(this);

                actualPolygon.Points.Add(newPoint);
                actualPolygonCheckpoints.Add(newPoint);
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                if (actualPolygonCheckpoints.Count != 0)
                {
                    var lastPoint = actualPolygonCheckpoints.Last();
                    actualPolygonCheckpoints.Remove(lastPoint);

                    var index = actualPolygon.Points.IndexOf(lastPoint);

                    for (int i = actualPolygon.Points.Count - 1; i >= index; i--)
                        actualPolygon.Points.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Obsluga zdarzenia poruszenia myszka. Dodaje punkty w przypadku trzymania lewego klawisza myszki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                actualPolygon.Points.Add(e.GetPosition(this));
        }

        /// <summary>
        /// Zapisuje aktualne oznaczenie do listy po czym resetuje aktualne oznaczenie, przygotowujac sie na kolejne.
        /// </summary>
        public void SaveActualElement()
        {
            if (actualDocument == null)
                return;

            if (actualPolygon.Points.Count <= 4)
                return;

            var polygon = new Polygon()
            {
                Fill = actualItemClass.Color,
                FillRule = FillRule.Nonzero
            };

            foreach (var point in actualPolygon.Points)
                polygon.Points.Add(point);

            resetActualPolygon();

            markedItems.Add(new MarkedItem(actualItemClass, polygon));

            if (actualDocument.DocumentStatus != Document.Status.MaskGenerated)
                actualDocument.SetAsMarked();
        }

        /// <summary>
        /// Zmiana typu aktualnego oznaczenia.
        /// </summary>
        /// <param name="newItemClass">Nowy typ oznaczenia.</param>
        public void SetActualDocumentClass((string Name, Brush Color) newItemClass)
        {
            actualItemClass = newItemClass;

            actualPolygon.Fill = actualItemClass.Color;
        }

        /// <summary>
        /// Funkcja generujaca maske dla aktualnego dokumentu.
        /// </summary>
        public void SaveMaskToFile(Uri location)
        {
            var rtb = new RenderTargetBitmap(
                (int)DocumentSurface.RenderSize.Width,
                (int)DocumentSurface.RenderSize.Height,
                96d, 96d, System.Windows.Media.PixelFormats.Default);

            resetActualPolygon();
            var background = DocumentSurface.Background;
            DocumentSurface.Background = Brushes.White;
            DocumentSurface.UpdateLayout();

            rtb.Render(DocumentSurface);

            var image = new BitmapImage(new Uri(@$"documents/{actualDocument.Name}.jpg", UriKind.Relative));

            var resized = new TransformedBitmap(rtb, new ScaleTransform(
                image.PixelWidth / DocumentSurface.RenderSize.Width,
                image.PixelHeight / DocumentSurface.RenderSize.Height));

            var pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(resized));


            using (var fs = System.IO.File.OpenWrite(@$"masks/{actualDocument.Name}.png"))
            {
                pngEncoder.Save(fs);
            }

            DocumentSurface.Background = background;

            actualDocument.SetAsMaskGenerated();
        }

        /// <summary>
        /// Usuwa podany oznaczony element z listy
        /// </summary>
        /// <param name="markedItem">Oznaczenie do usuniecia.</param>
        public void DeleteMarkedItem(MarkedItem markedItem)
        {
            markedItems.Remove(markedItem);

            if (actualDocument.MarkedItems.Count == 0 && actualDocument.DocumentStatus != Document.Status.MaskGenerated)
                actualDocument.SetAsNew();
        }

        /// <summary>
        /// Zmiana aktualnie oznaczanego dokumentu. Wczytuje istniejace dla aktualnego dokumentu oznaczenia na ekran oraz resetuje aktualne oznaczenie.
        /// </summary>
        /// <param name="newDocument">Nowy dokument do oznaczania.</param>
        public void ChangeDocument(Document newDocument)
        {
            actualItemClass = DocumentClasses.Text;

            actualDocument = newDocument;
            markedItems.Clear();
            resetActualPolygon();

            DocumentSurface.Background = new ImageBrush(new BitmapImage(new Uri(@$"documents/{actualDocument.Name}.jpg", UriKind.Relative))) { Stretch = Stretch.Fill };

            var existingItems = actualDocument.MarkedItems
                .ToList();

            actualDocument.MarkedItems
                .Clear();

            existingItems
                .ForEach(x => markedItems.Add(x));
        }
    }
}
