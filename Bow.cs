using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BowAndArrowWPF
{
    public class Bow
    {
        public Rectangle Rectangle { get; set; }
        public Rect Hitbox { get; set; }
        public double Width { get; set; } = 25;
        public double Height { get; set; } = 100;
        public double Speed { get; set; } = 10;
        public Arrow arrow { get; set; }
        public uint arrowCooldown { get; set; } = 0;

        public Bow() 
        {
            this.Rectangle = new Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = new SolidColorBrush(Colors.Red)
            };
            this.Hitbox = new Rect();

            MainWindow.GameCanvas.Children.Add(this.Rectangle);

            EventHub.moveBowUp += MoveUp;
            EventHub.moveBowDown += MoveDown;

            ResetPosition();

        }

        public void ResetPosition()
        {
            double CanvasHeight = MainWindow.GameCanvas.ActualHeight;

            SetTop(CanvasHeight / 2 - Height / 2);
            SetLeft(10);
        }

        public void ArrowFire()
        {
            if (arrowCooldown != 0) return;

            arrowCooldown = 20;

            arrow = new Arrow(GetCurrentTop() + Height / 2);
        }

        public void ArrowCooldownTick(object sender, EventArgs e)
        {
            if (arrowCooldown == 0) return;
            arrowCooldown--;
        }

        public void MoveUp()
        {
            SetTop(GetCurrentTop() - Speed);

            if (GetCurrentTop() < 0)
            {
                SetTop(0);
            }
        }

        public void MoveDown()
        {
            double CanvasHeight = MainWindow.GameCanvas.ActualHeight;

            SetTop(GetCurrentTop() + Speed);

            if (GetCurrentTop() > CanvasHeight - Height)
            {
                SetTop(CanvasHeight - Height);
            }
        }

        private double GetCurrentTop()
        {
            return Canvas.GetTop(Rectangle);
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
