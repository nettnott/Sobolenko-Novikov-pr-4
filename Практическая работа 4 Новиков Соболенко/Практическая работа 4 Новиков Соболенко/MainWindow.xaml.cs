using System.Windows;
using System.Windows.Controls;

namespace Практическая_работа_4_Новиков_Соболенко
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateTo(new Page1(), BtnNav1);
        }

        private void NavigateTo(System.Windows.Controls.Page page, Button activeBtn)
        {
            MainFrame.Navigate(page);
            BtnNav1.Style = (Style)Resources["NavButtonStyle"];
            BtnNav2.Style = (Style)Resources["NavButtonStyle"];
            BtnNav3.Style = (Style)Resources["NavButtonStyle"];
            activeBtn.Style = (Style)Resources["NavButtonActiveStyle"];
        }

        private void BtnPage1_Click(object sender, RoutedEventArgs e)
            => NavigateTo(new Page1(), BtnNav1);

        private void BtnPage2_Click(object sender, RoutedEventArgs e)
            => NavigateTo(new Page2(), BtnNav2);

        private void BtnPage3_Click(object sender, RoutedEventArgs e)
            => NavigateTo(new Page3(), BtnNav3);

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            ConfirmAndExit();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show(
                "Вы действительно хотите выйти из приложения?",
                "Подтверждение выхода",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                e.Cancel = true;
        }

        private void ConfirmAndExit()
        {
            var result = MessageBox.Show(
                "Вы действительно хотите выйти из приложения?",
                "Подтверждение выхода",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Closing -= Window_Closing;
                Application.Current.Shutdown();
            }
        }
    }
}