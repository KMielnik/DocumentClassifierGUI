using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
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

namespace DocumentClassifierGUI
{
    /// <summary>
    /// Interaction logic for DocumentMarkingView.xaml
    /// </summary>
    public partial class DocumentMarkingView : UserControl, IDocumentMarkingView
    {
        private Polygon actualPolygon;
        private (string Name, Brush Color) actualItemClass;

        private ObservableCollection<Point> actualPolygonCheckpoints = new ObservableCollection<Point>();
        private List<Ellipse> points = new List<Ellipse>();

        private ObservableCollection<MarkedItem> markedItems = new ObservableCollection<MarkedItem>();

        public event EventHandler<MarkedItemsChangedEventArgs> MarkedItemsChanged;

        public DocumentMarkingView()
        {
            InitializeComponent();

            actualItemClass = DocumentClasses.Text;
            resetActualPolygon();

            DocumentSurface.Background = new ImageBrush(new BitmapImage(new Uri(@"test_document.jpg", UriKind.Relative))) { Stretch = Stretch.Fill };

            actualPolygonCheckpoints.CollectionChanged += ActualPolygonCheckpoints_CollectionChanged;
            markedItems.CollectionChanged += MarkedItems_CollectionChanged;
            markedItems.CollectionChanged += (s,e) => MarkedItemsChanged?.Invoke(s, new MarkedItemsChangedEventArgs(markedItems));
        }

        private void MarkedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (MarkedItem newItem in e.NewItems)
                    DocumentSurface.Children.Add(newItem.polygon);
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (MarkedItem deletedItem in e.OldItems)
                    DocumentSurface.Children.Remove(deletedItem.polygon);
            }
        }

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

        private void DocumentSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                actualPolygon.Points.Add(e.GetPosition(this));
        }

        public void SaveActualElement()
        {
            var polygon = new Polygon()
            {
                Fill = actualItemClass.Color,
                FillRule = FillRule.Nonzero
            };

            foreach (var point in actualPolygon.Points)
                polygon.Points.Add(point);

            resetActualPolygon();

            markedItems.Add(new MarkedItem(actualItemClass, polygon));
        }

        public void SetActualDocumentClass((string Name, Brush Color) newItemClass)
        {
            actualItemClass = newItemClass;

            actualPolygon.Fill = actualItemClass.Color;
        }

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

            var resized = new TransformedBitmap(rtb, new ScaleTransform(
                595 / DocumentSurface.RenderSize.Width,
                726 / DocumentSurface.RenderSize.Height));

            var pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(resized));
            

            using(var fs = System.IO.File.OpenWrite("mask.png"))
            {
                pngEncoder.Save(fs);
            }

            DocumentSurface.Background = background;
        }
    }
}
