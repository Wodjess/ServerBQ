using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace better
{
    class Program
    {
        public static string errors = "";
        const int port = 48657;
        public static string[] array = new string[2];
        static TcpListener listener;
        static void Main(string[] args)
        {
            Thread thread = new Thread(ClientObject.lister);
            thread.Start();
                try
                {
                    listener = new TcpListener(IPAddress.Any, port);
                    listener.Start();
                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        ClientObject clientObject = new ClientObject(client);
                        Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                        clientThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    errors = ex.ToString();
                }
                finally
                {
                    if (listener != null)
                        listener.Stop();
                }
           /* else
            {
                Program2.Main1("");
            }*/
        }
    }
    public class ClientObject
    {
        public static void lister()
        {
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < Program.array.Length; i++)
                {
                    Console.WriteLine("["+i+"] "+Program.array[i]);
                }
                try
                {
                    Console.WriteLine("Options");
                    Console.WriteLine("1 - Update game list");
                    Console.WriteLine("2 - Delete an array element");
                    if (Program.errors != "")
                    {
                        Console.WriteLine(Program.errors);
                    }
                    if (int.Parse(Console.ReadLine()) == 2)
                    {
                        int deli = int.Parse(Console.ReadLine());
                        Program.array = Aditional.del(Program.array, deli);
                    }
                }
                catch (Exception ex)
                {
                   Program.errors = ex.ToString();
                }
            }
        }
        public TcpClient client;
        public ClientObject(TcpClient tcpClient)
        {
            client = tcpClient;
        }
        string[] morearray(string[] array)
        {
            string[] arraynew = new string[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                arraynew[i] = array[i];
            }
            return arraynew;
        }
        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    try
                    {
                        int number = int.Parse(message);
                        if (number * 4 > Program.array.Length)
                        {
                            message = "null";
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                message = message + Program.array[i + (number * 4)];
                            }
                        }
                    }
                    catch
                    {
                        if (message == "WhatArray")
                        {
                            message = Program.array.Length.ToString();
                        }
                        else
                        {
                            if (message != "Connection" && message.Length >= 3)
                            {
                                for (int i = 0; i < Program.array.Length; i++)
                                {
                                    if (i + 1 == Program.array.Length)
                                    {
                                        Program.array = morearray(Program.array);
                                    }
                                    if (Program.array[i] == null)
                                    {
                                        Program.array[i] = message + "➽";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                        data = Encoding.Unicode.GetBytes(message);
                        stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
            stream.Close();
            Program.errors = ex.ToString();
            return;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
            return;
        }
    }

    class Program2
    {
        const int port = 48657;   
        public static void Main1(string message, string address = "51.124.219.208")
        {
            string[] array;
            Console.Write("Введите свое имя:");
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    // ввод сообщения
                    message = Console.ReadLine();
                    if (message != "")
                    {
                        Console.Clear();
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
                        array = message.Split('➽');
                        for (int i = 0; i < array.Length; i++)
                        {
                            Console.WriteLine(array[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
    class Aditional
    {
        public static string[] del(string[] array, int num)
        {
            string[] arr = new string[array.Length - 1];
            for (int i = 0; i < num; i++)
            {
                arr[i] = array[i];
            }
            for (int i = num + 1; i < array.Length; i++)
            {
                arr[i - 1] = array[i];
            }
            return arr;
        }
    }
}
