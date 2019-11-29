using System;
using System.Collections.Generic;
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
        private Point currentPoint;
        public DocumentMarkingView()
        {
            InitializeComponent();

            resetActualPolygon();

            DocumentSurface.Background = new ImageBrush(new BitmapImage(new Uri(@"test_document.jpg", UriKind.Relative))) { Stretch = Stretch.Fill };
            DocumentSurface.Children.Add(actualPolygon);
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
        }

        private void DocumentSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                actualPolygon.Points.Add(e.GetPosition(this));
        }

        private void DocumentSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                actualPolygon.Points.Add(e.GetPosition(this));
        }
    }
}
