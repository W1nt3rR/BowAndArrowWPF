using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BowAndArrowWPF
{
    public partial class MainWindow : Window
    {
        public static Canvas GameCanvas;
        public bool MoveUp { get; set; } = false;
        public bool MoveDown { get; set; } = false;
        public bool Fire { get; set; } = false;
        public Bow bow { get; set; }
        public List<Baloon> Baloons { get; set; }
        public int Score { get; set; } = 0;
        Random random = new Random();

        DispatcherTimer GameClock;

        public int ClockDivider { get; set; } = 0;

        public MainWindow()
        {
            InitializeComponent();

            // Game Timer
            GameClock = new DispatcherTimer();
            GameClock.Interval = TimeSpan.FromMilliseconds(10);
            GameClock.Tick += Tick;
            GameClock.Start();

            GameCanvas = gameCanvas;
            GameCanvas.Focus();

            Baloons = new List<Baloon>();

            bow = new Bow();

            GameClock.Tick += bow.ArrowCooldownTick;

        }

        private void Tick(object sender, EventArgs e)
        {
            EventHub.MoveBalloonsUp();
            EventHub.MoveArrowRight();

            HandleIntersections();

            if (MoveUp) EventHub.MoveBowUp();
            if (MoveDown) EventHub.MoveBowDown();
            if (Fire) bow.ArrowFire();

            if (ClockDivider == 0)
            {
                ClockDivider = random.Next(10, 100);
                Baloon baloon = new Baloon();
                Baloons.Add(baloon);
            }
            ClockDivider--;
        }

        private void HandleIntersections()
        {
            List<Rectangle> rectangles = GameCanvas.Children.OfType<Rectangle>().ToList();

            foreach (var rectangle in rectangles)
            {
                if ((string)rectangle.Tag == "arrow")
                {
                    Rectangle arrow = rectangle;
                    Rect rectArrow = new Rect(Canvas.GetLeft(arrow), Canvas.GetTop(arrow), arrow.Width, arrow.Height);

                    List<Ellipse> elipses = GameCanvas.Children.OfType<Ellipse>().ToList();

                    foreach (var elipse in elipses)
                    {
                        if ((string)elipse.Tag == "baloon")
                        {
                            Ellipse baloon = elipse;
                            Rect rectBaloon= new Rect(Canvas.GetLeft(baloon), Canvas.GetTop(baloon), baloon.Width, baloon.Height);

                            if (rectArrow.IntersectsWith(rectBaloon))
                            {
                                GameCanvas.Children.Remove(arrow);
                                GameCanvas.Children.Remove(baloon);

                                Score++;
                                ScoreLabel.Content = Score;
                            }
                        }
                    }
                }
            }
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                MoveDown = true;
            }

            if (e.Key == Key.W)
            {
                MoveUp = true;
            }

            if (e.Key == Key.Space)
            {
                Fire = true;
            }
        }

        private void KeyUpHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                MoveDown = false;
            }

            if (e.Key == Key.W)
            {
                MoveUp = false;
            }

            if (e.Key == Key.Space)
            {
                Fire = false;
            }
        }


    }
}
