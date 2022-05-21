using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace betterquest
{
    public partial class Information : Page
    {
        public Information()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://disk.yandex.ee/d/W2leCGblmnub6w");
            }
            catch
            {
                MessageBox.Show("Произошла ошибка вашего браузера.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://vk.com/dotnet26");
            }
            catch
            {
                MessageBox.Show("Произошла ошибка вашего браузера.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
