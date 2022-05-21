using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;
namespace betterquest
{
    class Client // не забудь про мультитрединг
    {
        const int port = 48657;
        public static void Download(ref string message, string address = "127.0.0.1")
        {
            TcpClient client = null;
            address = PreLoad.IPAdressOfServer;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                if (message != "")
                {
                    message = String.Format(message);
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    // отправка сообщения
                    stream.Write(data, 0, data.Length);

                    // получаем ответ
                    data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    message = builder.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
    class Fixed
    {
        public static string FixFirstLetter(string url)
        {
            try
            {
                return url.Substring(0);
            }
            catch
            {
                return "error";
            }

        }
    }
}
