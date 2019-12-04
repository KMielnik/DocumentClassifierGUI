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
    /// Interaction logic for ZoomableControlView.xaml
    /// </summary>
    public partial class ZoomableControlView : UserControl
    {
        public new object Content { private set; get; }
        private const double ZoomFactor = 1.1;
        public ZoomableControlView(object control)
        {
            InitializeComponent();

            Content = control;

            ZoomableControl.Content = control;
        }

        private void ZoomableControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var panel = sender as Panel;

            if (e.Delta > 0)
            {
                var position = e.GetPosition(this);

                double oldCenterX = scaleTransform.CenterX;
                double oldCenterY = scaleTransform.CenterY;

                scaleTransform.CenterX = position.X;
                scaleTransform.CenterY = position.Y;

                translateTransform.X += (scaleTransform.CenterX - oldCenterX) * (scaleTransform.ScaleX - 1);
                translateTransform.Y += (scaleTransform.CenterY - oldCenterY) * (scaleTransform.ScaleY - 1);

                scaleTransform.ScaleX*= ZoomFactor;
                scaleTransform.ScaleY*= ZoomFactor;
            }

            if (e.Delta < 0)
            {
                scaleTransform.ScaleX = 1;
                scaleTransform.ScaleY = 1;

                translateTransform.X = 0;
                translateTransform.Y = 0;
            }
        }

        private Point previousPosition;

        private void ZoomableControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                var delta = e.GetPosition(this) - previousPosition;
                translateTransform.X += delta.X;
                translateTransform.Y += delta.Y;

                previousPosition = e.GetPosition(this);
            }
        }

        private void ZoomableControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
                previousPosition = e.GetPosition(this);
        }
    }
}
