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
    public class Arrow
    {
        public Rectangle Rectangle { get; set; }
        public double Width { get; set; } = 40;
        public double Height { get; set; } = 5;
        public double Speed { get; set; } = 20;

        public Arrow(double YCoord)
        {
            this.Rectangle = new Rectangle
            {
                Height = Height,
                Width = Width,
                Fill = new SolidColorBrush(Colors.Black),
                Tag = "arrow"
            };

            MainWindow.GameCanvas.Children.Add(this.Rectangle);

            RegisterEvents();

            ResetPosition(YCoord);
        }

        public void ResetPosition(double YCoord)
        {
            SetTop(YCoord);
            SetLeft(10);
        }

        public void RegisterEvents()
        {
            EventHub.moveArrowRight += MoveRight;
        }

        public void UnRegisterEvents()
        {
            EventHub.moveArrowRight -= MoveRight;
        }

        public void RemoveArrow()
        {
            UnRegisterEvents();
            MainWindow.GameCanvas.Children.Remove(this.Rectangle);
        }

        public void MoveRight()
        {
            double CanvasWidth = MainWindow.GameCanvas.ActualWidth;

            SetLeft(GetCurrentLeft() + Speed);

            if (GetCurrentLeft() > CanvasWidth)
            {
                RemoveArrow();
            }
        }

        private double GetCurrentTop()
        {
            return Canvas.GetTop(Rectangle);
        }

        private double GetCurrentLeft()
        {
            return Canvas.GetLeft(Rectangle);
        }

        private void SetTop(double top)
        {
            Canvas.SetTop(Rectangle, top);
        }

        private void SetLeft(double left)
        {
            Canvas.SetLeft(Rectangle, left);
        }
    }
}
