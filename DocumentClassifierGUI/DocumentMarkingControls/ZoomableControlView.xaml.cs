using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DocumentClassifierGUI
{
    /// <summary>
    /// Logika interakcji dla ZoomableControlView.xaml, pozwalajacej na przyblizanie widoku.
    /// </summary>
    public partial class ZoomableControlView : UserControl
    {
        /// <summary>
        /// Zawartosc do przyblizania
        /// </summary>
        public new object Content { private set; get; }
        /// <summary>
        /// Predkosc przyblizania widoku.
        /// </summary>
        private const double ZoomFactor = 1.1;
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="control">Widok do umieszczenia w przyblizalnej kontrolce.</param>
        public ZoomableControlView(object control)
        {
            InitializeComponent();

            Content = control;

            ZoomableControl.Content = control;
        }

        /// <summary>
        /// Obsluga przyblizania/oddalania myszka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                scaleTransform.ScaleX *= ZoomFactor;
                scaleTransform.ScaleY *= ZoomFactor;
            }

            if (e.Delta < 0)
            {
                scaleTransform.ScaleX = 1;
                scaleTransform.ScaleY = 1;

                translateTransform.X = 0;
                translateTransform.Y = 0;
            }
        }
        /// <summary>
        /// Punkt pomagajacy przy operacji przesuwania widoku.
        /// </summary>
        private Point previousPosition;
        /// <summary>
        /// obsluga zdarzenia poruszenia myszka, przesuwajaca widok.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Obsluga zdarzenia wcisniecia srodkowego przycisku myszki, pozwalajacego na przesuwanie widoku.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomableControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
                previousPosition = e.GetPosition(this);
        }
    }
}
