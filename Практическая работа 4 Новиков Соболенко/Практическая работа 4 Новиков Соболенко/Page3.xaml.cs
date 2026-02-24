using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Практическая_работа_4_Новиков_Соболенко
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TxtX0.Text, out double x0) &&
                double.TryParse(TxtXk.Text, out double xk) &&
                double.TryParse(TxtDx.Text, out double dx) &&
                double.TryParse(TxtB.Text, out double b))
            {
                TxtOutput.Clear();
                GraphCanvas.Children.Clear();

                StringBuilder sb = new StringBuilder();
                Polyline polyline = new Polyline
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 2
                };

                var points = new System.Collections.Generic.List<Point>();
                double minX = x0, maxX = xk;
                double minY = double.MaxValue, maxY = double.MinValue;

                for (double x = x0; x <= xk + 0.0001; x += dx) 
                {
                    double y = Math.Pow(x, 4) + Math.Cos(2 + Math.Pow(x, 3) - b);

                    sb.AppendLine($"x={x:F2}  y={y:F2}");

                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                    points.Add(new Point(x, y));
                }

                TxtOutput.Text = sb.ToString();

                double canvasWidth = GraphCanvas.ActualWidth;
                double canvasHeight = GraphCanvas.ActualHeight;

                if (canvasWidth == 0) canvasWidth = 400;
                if (canvasHeight == 0) canvasHeight = 300;

                foreach (var p in points)
                {


                    double screenX = (p.X - minX) / (maxX - minX) * canvasWidth;
                    double screenY = canvasHeight - ((p.Y - minY) / (maxY - minY) * canvasHeight);

                    polyline.Points.Add(new Point(screenX, screenY));
                }

                GraphCanvas.Children.Add(polyline);
            }
            else
            {
                MessageBox.Show("Проверьте введенные данные.");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtOutput.Clear();
            GraphCanvas.Children.Clear();
        }
    }
}