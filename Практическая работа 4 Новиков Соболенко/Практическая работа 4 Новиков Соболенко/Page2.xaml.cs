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

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TxtX.Text, out double x) && int.TryParse(TxtI.Text, out int i))
            {

                double fx = 0;
                if (RbSh.IsChecked == true) fx = Math.Sinh(x);
                else if (RbSqr.IsChecked == true) fx = Math.Pow(x, 2);
                else if (RbExp.IsChecked == true) fx = Math.Exp(x);

                double result;
                bool isOdd = (i % 2 != 0); 

                if (isOdd && x > 0)
                {
                    result = i * Math.Sqrt(fx);
                }
                else if (!isOdd && x < 0)
                {
                    result = (i / 2.0) * Math.Sqrt(Math.Abs(fx));
                }
                else
                {
                    result = Math.Sqrt(Math.Abs(i * fx));
                }

                TxtResult.Text = Math.Round(result, 4).ToString();
            }
            else
            {
                MessageBox.Show("Введите корректные данные (X - число, i - целое число).");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtX.Clear(); TxtI.Clear(); TxtResult.Clear();
            RbSh.IsChecked = true;
        }
    }
}
