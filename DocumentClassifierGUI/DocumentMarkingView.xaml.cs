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
        private ObservableCollection<Point> actualPolygonCheckpoints = new ObservableCollection<Point>();
        private List<Ellipse> points = new List<Ellipse>();
        public DocumentMarkingView()
        {
            InitializeComponent();

            resetActualPolygon();

            DocumentSurface.Background = new ImageBrush(new BitmapImage(new Uri(@"test_document.jpg", UriKind.Relative))) { Stretch = Stretch.Fill };
            DocumentSurface.Children.Add(actualPolygon);

            actualPolygonCheckpoints.CollectionChanged += ActualPolygonCheckpoints_CollectionChanged;
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
            actualPolygon = new Polygon()
            {
                Stroke = Brushes.Black,
                Fill = Brushes.Red,
                FillRule = FillRule.Nonzero,
                Opacity = 0.6
            };

            actualPolygonCheckpoints.Clear();
        }

        private void DocumentSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var newPoint = e.GetPosition(this);

                actualPolygon.Points.Add(newPoint);
                actualPolygonCheckpoints.Add(newPoint);
            }
            else if(e.RightButton == MouseButtonState.Pressed)
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
    }
}
