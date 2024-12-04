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

namespace Paint
{
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private Line straightLine = new Line();
        private Brush brush = Brushes.Black;
        private int thickness = 2;
        private string mode = "Free Draw";
        private Point previousPoint = new Point(-1, -1);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Paint.Children.Clear();
        }

        private void SizeChange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] strings = SizeChange.SelectedItem.ToString()!.Split(' ');
            thickness = int.Parse(strings[1]);
        }

        private void ColorChanged_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] strings = ColorChanged.SelectedItem.ToString()!.Split(' ');
            switch (strings[1])
            {
                case "Red":
                    brush = Brushes.Red;
                    break;
                case "Green":
                    brush = Brushes.Green;
                    break;
                case "Yellow":
                    brush = Brushes.Yellow;
                    break;
                case "Blue":
                    brush = Brushes.Blue;
                    break;
                default:
                    brush = Brushes.Black;
                    break;
            }
        }

        private void Mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] strings = Mode.SelectedItem.ToString()!.Split(' ');
            mode = strings[1];
        }

        private void Paint_MouseMove(object sender, MouseEventArgs e)
        {
            if (mode == "FreeDraw")
            {
                if (isDrawing)
                {
                    Point currentPoint;
                    currentPoint = e.GetPosition(Paint);
                    Line line = new Line();
                    if (previousPoint.X != -1 && previousPoint.Y != -1)
                    {
                        line.Stroke = brush;
                        line.StrokeThickness = thickness;
                        line.X1 = previousPoint.X;
                        line.Y1 = previousPoint.Y;
                        line.X2 = currentPoint.X;
                        line.Y2 = currentPoint.Y;
                        Paint.Children.Add(line);
                    }

                    previousPoint = currentPoint;
                }
            }
            else if (mode == "StraightLine")
            {
                if (isDrawing)
                {
                    Point currentPoint;
                    currentPoint = e.GetPosition(Paint);
                    if (previousPoint.X != -1 && previousPoint.Y != -1)
                    {
                        straightLine.X2 = currentPoint.X;
                        straightLine.Y2 = currentPoint.Y;
                    }
                }
            }
        }

        private void Paint_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            previousPoint = new Point(-1, -1);
        }

        private void Paint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            if (mode == "StraightLine")
            {
                if (straightLine != null)
                {
                    straightLine = new Line();
                }
                previousPoint = e.GetPosition(Paint);
                straightLine!.Stroke = brush;
                straightLine.StrokeThickness = thickness;
                straightLine.X1 = previousPoint.X;
                straightLine.Y1 = previousPoint.Y;
                straightLine.X2 = previousPoint.X;
                straightLine.Y2 = previousPoint.Y;

                Paint.Children.Add(straightLine);
            }
        }
    }
}