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

namespace Практическая_работа_4_Новиков_Соболенко
{
    public partial class Page2 : Page
    {
        public Page2()
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

        private bool ValidateInputs(out double x, out int i)
        {
            x = 0; i = 0;
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
                errors += "• Поле X содержит некорректное значение (нужно вещественное число)\n";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(TxtI.Text))
            {
                HighlightError(TxtI);
                errors += "• Поле i не заполнено\n";
                valid = false;
            }
            else if (!int.TryParse(TxtI.Text, out i))
            {
                HighlightError(TxtI);
                errors += "• Поле i должно содержать целое число (без дробной части)\n";
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
            if (!ValidateInputs(out double x, out int i))
                return;

            double fx = 0;
            if (RbSh.IsChecked == true) fx = Math.Sinh(x);
            else if (RbSqr.IsChecked == true) fx = Math.Pow(x, 2);
            else if (RbExp.IsChecked == true) fx = Math.Exp(x);

            double result;
            bool isOdd = (i % 2 != 0);

            if (isOdd && x > 0)
                result = i * Math.Sqrt(fx);
            else if (!isOdd && x < 0)
                result = (i / 2.0) * Math.Sqrt(Math.Abs(fx));
            else
                result = Math.Sqrt(Math.Abs(i * fx));

            TxtResult.Text = Math.Round(result, 4).ToString("F4");
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtX.Clear(); TxtI.Clear(); TxtResult.Clear();
            RbSh.IsChecked = true;
            TxtX.BorderBrush = new SolidColorBrush(Color.FromRgb(0xA0, 0xB0, 0xC0));
            TxtI.BorderBrush = new SolidColorBrush(Color.FromRgb(0xA0, 0xB0, 0xC0));
            TxtX.BorderThickness = new Thickness(1);
            TxtI.BorderThickness = new Thickness(1);
            ErrorText.Visibility = Visibility.Collapsed;
        }
    }
}
