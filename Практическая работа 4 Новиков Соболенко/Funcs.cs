using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Практическая_работа_4_Новиков_Соболенко
{
    public static class Funcs
    {
        public static double Func1(double x, double y, double z)
        {
            double part1 = Math.Log(Math.Pow(y, -Math.Sqrt(Math.Abs(x))));
            double part2 = x - (y / 2.0);
            double part3 = Math.Pow(Math.Sin(Math.Atan(z)), 2);

            return (part1 * part2) + part3;
        }

        public static double Func2(double x, int i, double fx)
        {
            bool isOdd = (i % 2 != 0);

            if (isOdd && x > 0)
            {
                return i * Math.Sqrt(fx);
            }
            else if (!isOdd && x < 0)
            {
                return (i / 2.0) * Math.Sqrt(Math.Abs(fx));
            }
            else
            {
                return Math.Sqrt(Math.Abs(i * fx));
            }
        }

        public static List<Point> Func3(double x0, double xk, double dx, double b)
        {
            var points = new List<Point>();

            for (double x = x0; x <= xk + 0.0001; x += dx)
            {
                double y = Math.Pow(x, 4) + Math.Cos(2 + Math.Pow(x, 3) - b);
                points.Add(new Point(x, y));
            }

            return points;
        }

        public static void DrawGraph(Canvas canvas, List<Point> points, double minX, double maxX, double minY, double maxY)
        {
            canvas.Children.Clear();

            Polyline polyline = new Polyline
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };

            double canvasWidth = canvas.ActualWidth > 0 ? canvas.ActualWidth : 400;
            double canvasHeight = canvas.ActualHeight > 0 ? canvas.ActualHeight : 300;

            foreach (var p in points)
            {
                double screenX = (p.X - minX) / (maxX - minX) * canvasWidth;
                double screenY = canvasHeight - ((p.Y - minY) / (maxY - minY) * canvasHeight);

                polyline.Points.Add(new Point(screenX, screenY));
            }

            canvas.Children.Add(polyline);
        }
    }
}
