using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Практическая_работа_4_Новиков_Соболенко
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
                tb.BorderBrush = new SolidColorBrush(Color.FromRgb(0xA0, 0xB0, 0xC0));
            if (ErrorText != null)
                ErrorText.Visibility = Visibility.Collapsed;
        }

        private bool ValidateInputs(out double x0, out double xk, out double dx, out double b)
        {
            x0 = xk = dx = b = 0;
            bool valid = true;
            string errors = "";

            if (string.IsNullOrWhiteSpace(TxtX0.Text))
            { HighlightError(TxtX0); errors += "• Поле «X начальное» не заполнено\n"; valid = false; }
            else if (!double.TryParse(TxtX0.Text.Replace('.', ','), out x0))
            { HighlightError(TxtX0); errors += "• Поле «X начальное» — некорректное значение\n"; valid = false; }

            if (string.IsNullOrWhiteSpace(TxtXk.Text))
            { HighlightError(TxtXk); errors += "• Поле «X конечное» не заполнено\n"; valid = false; }
            else if (!double.TryParse(TxtXk.Text.Replace('.', ','), out xk))
            { HighlightError(TxtXk); errors += "• Поле «X конечное» — некорректное значение\n"; valid = false; }

            if (string.IsNullOrWhiteSpace(TxtDx.Text))
            { HighlightError(TxtDx); errors += "• Поле «Шаг dx» не заполнено\n"; valid = false; }
            else if (!double.TryParse(TxtDx.Text.Replace('.', ','), out dx))
            { HighlightError(TxtDx); errors += "• Поле «Шаг dx» — некорректное значение\n"; valid = false; }
            else if (dx <= 0)
            { HighlightError(TxtDx); errors += "• Шаг dx должен быть положительным числом\n"; valid = false; }

            if (string.IsNullOrWhiteSpace(TxtB.Text))
            { HighlightError(TxtB); errors += "• Поле «Параметр b» не заполнено\n"; valid = false; }
            else if (!double.TryParse(TxtB.Text.Replace('.', ','), out b))
            { HighlightError(TxtB); errors += "• Поле «Параметр b» — некорректное значение\n"; valid = false; }

            if (valid && x0 >= xk)
            {
                HighlightError(TxtX0); HighlightError(TxtXk);
                errors += "• X начальное должно быть меньше X конечного\n";
                valid = false;
            }

            if (!valid)
            {
                ErrorText.Text = errors.TrimEnd();
                ErrorText.Visibility = Visibility.Visible;
            }
            return valid;
        }

        private void HighlightError(TextBox tb)
        {
            tb.BorderBrush = Brushes.Red;
            tb.BorderThickness = new Thickness(2);
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs(out double x0, out double xk, out double dx, out double b))
                return;

            TxtOutput.Clear();
            GraphCanvas.Children.Clear();

            var sb = new StringBuilder();
            var points = new System.Collections.Generic.List<Point>();
            double minY = double.MaxValue, maxY = double.MinValue;

            sb.AppendLine(string.Format("{0,8}  {1,12}", "x", "y"));
            sb.AppendLine(new string('-', 24));

            for (double x = x0; x <= xk + 0.0001; x += dx)
            {
                double y = Math.Pow(x, 4) + Math.Cos(2 + Math.Pow(x, 3) - b);
                sb.AppendLine(string.Format("{0,8:F3}  {1,12:F4}", x, y));
                if (y < minY) minY = y;
                if (y > maxY) maxY = y;
                points.Add(new Point(x, y));
            }
            TxtOutput.Text = sb.ToString();

            GraphCanvas.UpdateLayout();
            double cW = GraphCanvas.ActualWidth > 10 ? GraphCanvas.ActualWidth : 450;
            double cH = GraphCanvas.ActualHeight > 10 ? GraphCanvas.ActualHeight : 320;
            double pad = 35;

            double rangeX = Math.Abs(xk - x0) < 1e-10 ? 1 : xk - x0;
            double rangeY = Math.Abs(maxY - minY) < 1e-10 ? 1 : maxY - minY;

            for (int t = 0; t <= 4; t++)
            {
                double sy = (cH - pad) - t * (cH - 2 * pad) / 4.0;
                var gridLine = new Line
                {
                    X1 = pad,
                    Y1 = sy,
                    X2 = cW - pad / 2,
                    Y2 = sy,
                    Stroke = new SolidColorBrush(Color.FromArgb(80, 150, 180, 220)),
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection { 4, 3 }
                };
                GraphCanvas.Children.Add(gridLine);
            }

            GraphCanvas.Children.Add(new Line
            {
                X1 = pad,
                Y1 = cH - pad,
                X2 = cW - pad / 2,
                Y2 = cH - pad,
                Stroke = Brushes.DarkGray,
                StrokeThickness = 1.5
            });
            GraphCanvas.Children.Add(new Line
            {
                X1 = pad,
                Y1 = pad / 2,
                X2 = pad,
                Y2 = cH - pad,
                Stroke = Brushes.DarkGray,
                StrokeThickness = 1.5
            });

            int tickCnt = Math.Min(6, points.Count);
            for (int t = 0; t <= tickCnt; t++)
            {
                double xv = x0 + t * rangeX / tickCnt;
                double sx = pad + (xv - x0) / rangeX * (cW - 2 * pad);
                GraphCanvas.Children.Add(new Line
                {
                    X1 = sx,
                    Y1 = cH - pad - 3,
                    X2 = sx,
                    Y2 = cH - pad + 3,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 1
                });
                var lbl = new TextBlock { Text = xv.ToString("F1"), FontSize = 9, Foreground = Brushes.DimGray };
                Canvas.SetLeft(lbl, sx - 10); Canvas.SetTop(lbl, cH - pad + 5);
                GraphCanvas.Children.Add(lbl);
            }

            for (int t = 0; t <= 4; t++)
            {
                double yv = minY + t * rangeY / 4.0;
                double sy = (cH - pad) - t * (cH - 2 * pad) / 4.0;
                GraphCanvas.Children.Add(new Line
                {
                    X1 = pad - 3,
                    Y1 = sy,
                    X2 = pad + 3,
                    Y2 = sy,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 1
                });
                var lbl = new TextBlock { Text = yv.ToString("F1"), FontSize = 9, Foreground = Brushes.DimGray };
                Canvas.SetLeft(lbl, 0); Canvas.SetTop(lbl, sy - 7);
                GraphCanvas.Children.Add(lbl);
            }

            var polyline = new Polyline
            {
                Stroke = new SolidColorBrush(Color.FromRgb(0x20, 0x60, 0xC0)),
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Round
            };
            foreach (var p in points)
            {
                double sx = pad + (p.X - x0) / rangeX * (cW - 2 * pad);
                double sy = (cH - pad) - (p.Y - minY) / rangeY * (cH - 2 * pad);
                polyline.Points.Add(new Point(sx, sy));
            }
            GraphCanvas.Children.Add(polyline);
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtOutput.Clear();
            GraphCanvas.Children.Clear();
            var defaultBrush = new SolidColorBrush(Color.FromRgb(0xA0, 0xB0, 0xC0));
            var defaultThick = new Thickness(1);
            TxtX0.BorderBrush = TxtXk.BorderBrush = TxtDx.BorderBrush = TxtB.BorderBrush = defaultBrush;
            TxtX0.BorderThickness = TxtXk.BorderThickness =
                TxtDx.BorderThickness = TxtB.BorderThickness = defaultThick;
            ErrorText.Visibility = Visibility.Collapsed;
        }
    }
}