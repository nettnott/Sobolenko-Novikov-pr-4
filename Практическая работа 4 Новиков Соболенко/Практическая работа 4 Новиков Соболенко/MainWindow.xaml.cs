using System.Windows;

namespace Практическая_работа_4_Новиков_Соболенко
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Page1()); 
        }

        private void BtnPage1_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Page1());
        private void BtnPage2_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Page2());
        private void BtnPage3_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Page3());
    }
}