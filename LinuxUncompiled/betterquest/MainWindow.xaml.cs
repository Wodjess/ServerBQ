using System.Threading;
using System.Windows;
using System.Windows.Input;
namespace betterquest
{
    public partial class MainWindow : Window
    {
        public static Thread DownloadThread = new Thread(DownloadGamesList);
        public static sbyte temp;
        public static string[][] GamesList = new string[1000][];
        public static bool avalible = false;
        public static int download10 = 0;
        public MainWindow()
        {
            InitializeComponent();
            if (PreLoad.IfDone != true)
            {
                MainFrame.Content = new PreLoad();
            }
        }
        public static void DownloadGamesList() //не мультипоток не забудь исправить
        {
            for (int i = 0; i < GamesList.Length; i++)
            {
                GamesList[i] = new string[4];
            }
            string message = "WhatArray";
            //скачивание массива данных
            Client.Download(ref message);
            avalible = true;
            int numberofArray = int.Parse(message);
            string final = "";
            for (int i = 0; i < numberofArray / 4; i++)
            {
                message = i.ToString();
                Client.Download(ref message);
                if (message == "null")
                {
                    break;
                }
                final = final + message;
                download10 = i * 4;
            }
            message = final;
            string[] testarray = message.Split('➽');
            try
            {
                testarray = connectonminus(testarray);
            }
            catch
            {
            }
            GamesList = beatyinfo(GamesList, testarray);
            GamesList = optimization(GamesList);
            temp = 1;
            
        }
        private void Button_Exit_Event(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void event1(object sender, MouseEventArgs e)
        {
            FirstGridAvalible.Opacity = 0.95;
            base.DragMove();
            FirstGridAvalible.Opacity = 1;
        }
        public static string[][] beatyinfo(string[][] array, string[] arraywithinformation)
        {
            int temp = 0;
            string[][] normalarray = new string[array.Length][];
            try
            {
                for (int i = 0; i < normalarray.Length; i++)
                {
                    normalarray[i] = new string[4];
                }
                for (int i = 0; i < normalarray.Length; i++)
                {
                    for (int i1 = 0; i1 < normalarray[i].Length; i1++)
                    {
                        normalarray[i][i1] = arraywithinformation[temp];
                        temp++;
                    }
                }
            }
            catch
            {
                array = normalarray;
            }
            return array;
        }
        public static string[][] optimization(string[][] array)
        {
            int NormalKolvo = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int i1 = 0; i1 < 2; i1++)
                {
                    if (array[i][i1] == null)
                    {
                        NormalKolvo = i;
                        break;
                    }
                }
                if (NormalKolvo != 0)
                {
                    break;
                }
            }
            string[][] NormalArry = new string[NormalKolvo][];
            for (int i = 0; i < NormalArry.Length; i++)
            {
                NormalArry[i] = new string[4];
            }
            for (int i = 0; i < NormalArry.Length; i++)
            {
                for (int i1 = 0; i1 < NormalArry[i].Length; i1++)
                {
                    NormalArry[i][i1] = "0";
                }
            }
            for (int i = 0; i < NormalArry.Length; i++)
            {
                for (int i1 = 0; i1 < NormalArry[i].Length; i1++)
                {
                    NormalArry[i][i1] = array[i][i1];
                }
            }
            return NormalArry;
        }
        public static string[] connectonminus(string[] array)
        {
            string[] arr = new string[array.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = array[i];
            }
            string[] arr1 = new string[arr.Length - 1];
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = arr[i];
            }
            return arr1;
        }
    }
}
