using System;
using System.Windows;
using System.Windows.Controls;

namespace Практическая_работа_4_Новиков_Соболенко
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TxtX.Text, out double x) &&
                double.TryParse(TxtY.Text, out double y) &&
                double.TryParse(TxtZ.Text, out double z))
            {

                try
                {
                    double part1 = Math.Log(Math.Pow(y, -Math.Sqrt(Math.Abs(x))));
                    double part2 = x - (y / 2.0);
                    double part3 = Math.Pow(Math.Sin(Math.Atan(z)), 2);

                    double a = part1 * part2 + part3;

                    TxtResult.Text = Math.Round(a, 4).ToString(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка вычисления: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные числа.");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtX.Clear(); TxtY.Clear(); TxtZ.Clear(); TxtResult.Clear();
        }
    }
}