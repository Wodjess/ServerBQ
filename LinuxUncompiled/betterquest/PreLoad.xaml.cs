using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Windows.Controls;
namespace betterquest
{
    public partial class PreLoad : Page
    {
        public static bool IfDone = false;
        public static string FileSavePath;
        public static string IPAdressOfServer;
        public PreLoad()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileSavePath = "Error";
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                FileSavePath = dialog.FileName;

            }
            PathButton1.Content = FileSavePath;
            if (FileSavePath != "Error")
            {
                TextNenych.Visibility = Visibility.Hidden;
                ReadyToGo.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                IfDone = true;
                ProgBar.Visibility = Visibility.Visible;
                ProgBar.Text = "Проверка доступности...";
                IPAdressOfServer = IPAdressOfServer1.Text;
                if (PreLoad.IfDone == true) // распаралелитьиьть
                {
                    MainWindow.DownloadGamesList();
                }
                if (MainWindow.avalible)
                {
                    ProgBar.Text = "Сервер доступен";
                }
                if (MainWindow.download10 >= 4)
                {
                    ProgBar.Text = "Скачанно " + MainWindow.download10.ToString();
                }
                if (MainWindow.temp != 0)
                {
                    NavigationService.Navigate(new Games());
                }
            }
            catch
            {
                MessageBox.Show("Попробуйте указать другой IP или место расположения файлов");
            }
        }
    }
}
