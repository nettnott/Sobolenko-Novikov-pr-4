using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Практическая_работа_4_Новиков_Соболенко
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Сброс подсветки ошибки при изменении поля
            if (sender is TextBox tb)
                tb.BorderBrush = new SolidColorBrush(Color.FromRgb(0xA0, 0xB0, 0xC0));
            if (ErrorText != null)
                ErrorText.Visibility = Visibility.Collapsed;
        }

        private bool ValidateInputs(out double x, out double y, out double z)
        {
            x = y = z = 0;
            bool valid = true;
            string errors = "";

            if (string.IsNullOrWhiteSpace(TxtX.Text))
            {
                HighlightError(TxtX);
                errors += "• Поле X не заполнено\n";
                valid = false;
            }
            else if (!double.TryParse(TxtX.Text.Replace('.', ','), out x))
            {
                HighlightError(TxtX);
                errors += "• Поле X содержит некорректное значение (нужно число)\n";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(TxtY.Text))
            {
                HighlightError(TxtY);
                errors += "• Поле Y не заполнено\n";
                valid = false;
            }
            else if (!double.TryParse(TxtY.Text.Replace('.', ','), out y))
            {
                HighlightError(TxtY);
                errors += "• Поле Y содержит некорректное значение (нужно число)\n";
                valid = false;
            }
            else if (y == 0)
            {
                HighlightError(TxtY);
                errors += "• Y не может быть равно 0 (деление на ноль в логарифме)\n";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(TxtZ.Text))
            {
                HighlightError(TxtZ);
                errors += "• Поле Z не заполнено\n";
                valid = false;
            }
            else if (!double.TryParse(TxtZ.Text.Replace('.', ','), out z))
            {
                HighlightError(TxtZ);
                errors += "• Поле Z содержит некорректное значение (нужно число)\n";
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
            if (!ValidateInputs(out double x, out double y, out double z))
                return;

            try
            {
                double part1 = Math.Log(Math.Pow(y, -Math.Sqrt(Math.Abs(x))));
                double part2 = x - (y / 2.0);
                double part3 = Math.Pow(Math.Sin(Math.Atan(z)), 2);
                double a = part1 * part2 + part3;

                TxtResult.Text = Math.Round(a, 4).ToString("F4");
            }
            catch (Exception ex)
            {
                ErrorText.Text = "Ошибка вычисления: " + ex.Message;
                ErrorText.Visibility = Visibility.Visible;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtX.Clear(); TxtY.Clear(); TxtZ.Clear(); TxtResult.Clear();
            TxtX.BorderBrush = new SolidColorBrush(Color.FromRgb(0xA0, 0xB0, 0xC0));
            TxtY.BorderBrush = new SolidColorBrush(Color.FromRgb(0xA0, 0xB0, 0xC0));
            TxtZ.BorderBrush = new SolidColorBrush(Color.FromRgb(0xA0, 0xB0, 0xC0));
            TxtX.BorderThickness = new Thickness(1);
            TxtY.BorderThickness = new Thickness(1);
            TxtZ.BorderThickness = new Thickness(1);
            ErrorText.Visibility = Visibility.Collapsed;
        }
    }
}