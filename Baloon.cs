using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BowAndArrowWPF
{
    public class Baloon
    {
        public Ellipse Ellipse { get; set; }
        public double Width { get; set; } = 40;
        public double Height { get; set; } = 50;
        public double Speed { get; set; } = 2;
        Random random = new Random();

        public Baloon()
        {
            this.Ellipse = new Ellipse
            {
                Height = Height,
                Width = Width,
                Fill = new SolidColorBrush(Colors.Green),
                Tag = "baloon"
            };

            MainWindow.GameCanvas.Children.Add(this.Ellipse);

            RegisterEvents();

            ResetPosition();
        }

        public void ResetPosition()
        {
            double CanvasWidth = MainWindow.GameCanvas.ActualWidth;
            double CanvasHeight = MainWindow.GameCanvas.ActualHeight;

            SetTop(CanvasHeight);
            SetLeft(random.Next((int)(CanvasWidth / 2), (int)(CanvasWidth - Width))); // to tidy up
        }

        public void RegisterEvents()
        {
            EventHub.moveBaloonsUp += MoveUp;
        }

        public void UnRegisterEvents()
        {
            EventHub.moveBaloonsUp -= MoveUp;
        }

        public void RemoveBaloon()
        {
            UnRegisterEvents();
            MainWindow.GameCanvas.Children.Remove(this.Ellipse);
        }

        public void MoveUp()
        {
            SetTop(GetCurrentTop() - Speed);

            if (GetCurrentTop() < -Height)
            {
                MainWindow.GameCanvas.Children.Remove(this.Ellipse);
            }
        }

        private double GetCurrentTop()
        {
            return Canvas.GetTop(Ellipse);
        }

        private void SetTop(double top)
        {
            Canvas.SetTop(Ellipse, top);
        }

        private void SetLeft(double left)
        {
            Canvas.SetLeft(Ellipse, left);
        }

    }
}
