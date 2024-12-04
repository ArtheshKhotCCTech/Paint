using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point previousPoint = new Point(-1, -1);
        Brush brush = Brushes.Black;
        int thickness = 2;
        bool isDrawing = false;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Paint.Children.Clear(); 
        }

        private void Paint_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (isDrawing)
            {
                Point currentPosition = e.GetPosition(Paint);
                Line line = new Line();
                if (previousPoint.X != -1 && previousPoint.Y != -1)
                {
                    line.X1 = previousPoint.X;
                    line.Y1 = previousPoint.Y;
                    line.X2 = currentPosition.X;
                    line.Y2 = currentPosition.Y;
                    line.Stroke = brush;
                    line.StrokeThickness = thickness;
                    Paint.Children.Add(line);
                }
                
                previousPoint = currentPosition;
            }

        }


        private void SizeChange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SizeChange.SelectedItem.ToString() != null)
            {
                string[] str = SizeChange.SelectedItem.ToString().Split(' ');
                thickness = int.Parse(str[1]);
            }
        }

        private void Paint_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void Paint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            //previousPoint = e.GetPosition(this);
        }

        private void Paint_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            
            previousPoint.X = -1;
            previousPoint.Y = -1;
        }

        private void ColorChanged_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}