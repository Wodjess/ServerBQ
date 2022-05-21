using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
namespace betterquest
{
    public partial class Games : Page
    {
        public string[][] GamesArray;
        public Games()
        {
            InitializeComponent();
            GamesLoading();
        }
        void DownloadEvent(Object sender, EventArgs e)
        {
            DebugInformation.Text = "";
            string tag = ((sender as Button).Tag).ToString();
            string name = GamesArray[int.Parse(tag)][0].ToString().Substring(1);
            try
            {
                Download(Fixed.FixFirstLetter(GamesArray[int.Parse(tag)][3]), PreLoad.FileSavePath, name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry something went wrong " + ex.ToString());
                MessageBox.Show(GamesArray[int.Parse(tag)][3]);
            }
        }
        public void Download(string url, string save_path, string name)
        {
            string fl = @"\";
            WebClient wc = new WebClient();
            double i = 0;
            wc.DownloadProgressChanged += (s, e) =>
            {
                i += 15.8;
                if (i >= 1000)
                {
                    DownloadingTextBlock.Text = "Downloading: " + Math.Round((i / 1000), 2) + " mb";
                }
                else
                {
                    DownloadingTextBlock.Text = "Downloading: " + Math.Round(i, 0) + " kb";
                }
            };
            wc.DownloadFileCompleted += (s, e) =>
            {
                DebugInformation.Text = "Your file has been downloaded";
                DownloadingTextBlock.Text = "";
            };
            wc.DownloadFileAsync(new Uri(url), save_path + fl + name);

        }
        public void GamesLoading()
        {
            GamesArray = MainWindow.GamesList;
            for (int i = 0; i < GamesArray.Length; i++)
            {
                Grid GameGrid = new Grid();
                GameGrid.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                GameGrid.Height = 100;
                GameGrid.Children.Add(
                    new TextBlock
                    {
                        Text = GamesArray[i][0],
                        Margin = new System.Windows.Thickness(157, 8, 3, 80),
                        FontFamily = new FontFamily("Arial Black")
                    }
                    );
                GameGrid.Children.Add(
                    new TextBlock
                    {
                        Text = GamesArray[i][1],
                        TextWrapping = System.Windows.TextWrapping.Wrap,
                        Margin = new System.Windows.Thickness(157, 22, 24, 38),
                        FontFamily = new FontFamily("Bahnschrift SemiBold Condensed"),
                        FontSize = 18
                    }
                    );
                Button button = new Button();
                button.Tag = i.ToString();
                string Test = Convert.ToString(GamesArray[i][0]).Substring(1);
                button.Name = "btn";
                button.Content = "Download";
                if (GamesArray[i][3] != null)
                    button.Background = (Brush)(new BrushConverter().ConvertFrom("#181735"));
                else
                    button.Background = (Brush)(new BrushConverter().ConvertFrom("#808080"));

                button.Foreground = Brushes.White;
                button.Margin = new System.Windows.Thickness(157, 65, 345, 10);
                button.Click += DownloadEvent;
                button.FontFamily = new FontFamily("Arial Black");
                GameGrid.Children.Add(button);

                //картинка с вапросикам)
                Image GameImage0 = new Image();
                GameImage0.Margin = new System.Windows.Thickness(10, 10, 436, 10);
                BlurEffect BlurEffect = new BlurEffect();
                BlurEffect.Radius = 5;
                GameImage0.Effect = BlurEffect;
                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.DecodePixelWidth = 200;
                GameImage0.Source = myBitmapImage;
                if (GamesArray[i][3] == null)
                {
                    myBitmapImage.UriSource = new Uri("../NotEnoghtImage.png", UriKind.Relative);

                }
                else
                {
                    try
                    {
                        myBitmapImage.UriSource = new Uri(GamesArray[i][2], UriKind.Absolute);
                        BlurEffect.Radius = 0;
                    }
                    catch
                    {
                        myBitmapImage.UriSource = new Uri("../NotEnoghtImage.png", UriKind.Relative);
                    }
                }
                myBitmapImage.EndInit();
                GameGrid.Children.Add(GameImage0);
                //картинка с вапросикам)
                GameListScroller.Children.Add(GameGrid);
            }
        }
        void CheakInformationAboutAPP(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Information());
        }
        void AdminDownloadServer(object sender, RoutedEventArgs e)
        {
            try
            {
                Download("https://data.terabox.com/file/0a588726f1addee73dda5e4892fc03fd?bkt=en-07c9b0a504a3706073dea77c779b0b2fed9ba8d7623e0f64f1c9d0d0a970a6e14709b6831642a29d&fid=4398460564292-250528-375107579380841&time=1644577986&sign=FDTAXUGERLQlBHSKfW-DCb740ccc5511e5e8fedcff06b081203-ofiKJrZXAN9Fgv7vQoSnpLwVyxA%3D&to=140&size=30375529&sta_dx=30375529&sta_cs=0&sta_ft=zip&sta_ct=0&sta_mt=0&fm2=MH%2Cdefault_region%2CAnywhere%2C%2C%2Cany&region=default_region&ctime=1644577758&mtime=1644577781&vuk=4398460564292&iv=0&htype=&randtype=&newver=1&newfm=1&secfm=1&flow_ver=3&pkey=en-7337d5356891fbd4abbdda20243af86ebe1b30d0ec9056c5edcb2b9d63c4426b243b914d180e55c5&sl=78577753&expires=1644606786&rt=pr&r=814411158&vbdid=-&fin=%D0%9F%D1%80%D0%B8%D0%BB%D0%BE%D0%B6%D0%B5%D0%BD%D0%B8%D0%B5.zip&fn=%D0%9F%D1%80%D0%B8%D0%BB%D0%BE%D0%B6%D0%B5%D0%BD%D0%B8%D0%B5.zip&rtype=1&dp-logid=9039928241963685331&dp-callid=0.1&hps=1&tsl=6000&csl=6000&fsl=-1&csign=A4fwvcIXYeuAyYKdCG13wr76Vvw%3D&so=0&ut=6&uter=4&serv=0&uc=1968544821&ti=e6e2f9d25109af0e302d0a7a8de10d18e6c4cb212da0ded2&adg=&reqlabel=250528_f_84b0d03d1ab90190cfac3f6bdead8298_-1_bca3c2772ef9b393b7a690ed9c24a0f9&ccn=RU&by=themis&Expires=1644606786&Signature=2lIAr-ROHP3HUUdEUBEubbIes3h16UQ0t93hFgTA5hCOyZgbjWwhNRaQ15ZqYb1ctHYLqCrmbQR2pb0R2NZwP37qAKXjyHYYdntQ0y2b-lpmcg6HKQIlWOeI2EKpvh7zhTh-GUW-bnQ3GP8iWuu907PG6ufKy3pYO~W6EuyvaY3ijS2UvouovdKGr5Lys71gwKtX2T1l48JUcj~t2p5k6veN7W0UZilm3do4NRGXLIuQJDFJ9ZlTH0qyR5hdKgU~2iPvXTmwrI8pe8M5EiMdRHMODrfpq8eR-vSVBjS6TPCep67-MGIk1CWsQJ3u2ZzT9JOr-M3yxdU7pL02H~CkNg__&Key-Pair-Id=K2UF6UUQLSZ84", PreLoad.FileSavePath, "ServerCreate.zip");
                Process.Start("https://disk.yandex.ee/d/W2leCGblmnub6w");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка внутри вашего пк " + ex);
            }
        }
        void AdministrationModeSave(object sender, RoutedEventArgs e)
        {
            try
            {
                Download("https://data.terabox.com/file/0d33a811f85e7eb8ec34be9c2a5276e3?bkt=en-713b21d6dbc3139820d0edd0145525b619d904fb0bf39ebc5cbba6ca65dd1a67fe4389a157cc3d33d133c008c79fdcf04add0c3018086b2133cbcfcbd1dd2bc4&fid=4398460564292-250528-167158977927678&time=1644577832&sign=FDTAXUGERLQlBHSKfW-DCb740ccc5511e5e8fedcff06b081203-L7cJkAXb0Nd%2F6jLIifQSAgbum30%3D&to=140&size=136665&sta_dx=136665&sta_cs=0&sta_ft=exe&sta_ct=0&sta_mt=0&fm2=MH%2Cdefault_region%2CAnywhere%2C%2C%2Cany&region=default_region&ctime=1644577751&mtime=1644577781&vuk=4398460564292&iv=0&htype=&randtype=&newver=1&newfm=1&secfm=1&flow_ver=3&pkey=en-d53934e49a9d664ab3ce4601d80017ecf484e7511a930bf7bbd58436e13a30404264e132e2eb4f5c1e2908d564426b7d5c9f4082dd95ce36305a5e1275657320&sl=78577753&expires=1644606632&rt=pr&r=434917063&vbdid=-&fin=BetterQuest+Server.exe&fn=BetterQuest+Server.exe&rtype=1&dp-logid=9039886707133907459&dp-callid=0.1&hps=1&tsl=6000&csl=6000&fsl=-1&csign=A4fwvcIXYeuAyYKdCG13wr76Vvw%3D&so=0&ut=6&uter=4&serv=0&uc=1968544821&ti=e6e2f9d25109af0e7f004ecc3e9623f06dce948cec9189be&adg=&reqlabel=250528_f_84b0d03d1ab90190cfac3f6bdead8298_-1_bca3c2772ef9b393b7a690ed9c24a0f9&ccn=RU&by=themis&Expires=1644606632&Signature=RNt3U3bVq-2khTBUu6iqyerDKmH5FLZit7brrXGEjT4w~BJ2yGzaXlv9TponmIYtGgl4D8v1Lhyh-nzZP60-4ACeE6CN6RT94~HXEr-T0a-PNCNBS1eCk2ihQcK2SFGvZfMbwrjipnKApnh9KAf4cFCfdMP1wjyNcaGoAdU2oEnSEOzmzY0-w2yYIvUuSViTP3uchgAJQNZ6Ny~WpnZf7krL3FBiBjMxqtBsLNt7rJwAJGnKutlcTgVOQQLCmaKZW8WMXtvUvyfFFcPxHMyQRIgtE2GHsOm1IheOUw0k~bWmx05xg7g~6Uh6sFyR6kz~bx5aYApZhK0Z7SI5uA9MQA__&Key-Pair-Id=K2UF6UUQLSZ84", PreLoad.FileSavePath, "BetterQuestForAdministration.exe");
                Process.Start("https://disk.yandex.ee/d/W2leCGblmnub6w");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка внутри вашего пк " + ex);
            }
        }
    }
}
