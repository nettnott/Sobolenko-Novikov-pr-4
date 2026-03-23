using System;
using System.Collections.Generic;
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
                List<Point> points = Funcs.Func3(x0, xk, dx, b);

                StringBuilder sb = new StringBuilder();
                double minY = double.MaxValue, maxY = double.MinValue;

                foreach (var p in points)
                {
                    sb.AppendLine($"x={p.X:F2}  y={p.Y:F2}");
                    if (p.Y < minY) minY = p.Y;
                    if (p.Y > maxY) maxY = p.Y;
                }
                TxtOutput.Text = sb.ToString();

                Funcs.DrawGraph(GraphCanvas, points, x0, xk, minY, maxY);
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