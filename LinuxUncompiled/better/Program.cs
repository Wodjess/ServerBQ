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
        public static string password;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Write the access password");
                Console.ForegroundColor = ConsoleColor.Green;
                password = Console.ReadLine();
                if (password.Length >= 6)
                {

                        Console.WriteLine("Repeat your password");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (Console.ReadLine() == password)
                        {
                            break;
                        }
                        else
                        {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR");
                        }
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("YOUR PASSWORD MUST BE LONGER THAN 6 SYMBOLS");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.Title = "WARNING!! Please don't forget this password";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please don't forget this password");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Starting the server...");
            Thread.Sleep(1000);
            Thread thread = new Thread(ClientObject.lister);
            thread.Start();
            Console.Title = "BetterQuest Server by Tokareff";
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
                    Console.WriteLine("3 - Change password");
                    Console.WriteLine("4 - Clear all errors");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("5 - Exit");
                    Console.ForegroundColor = ConsoleColor.White;
                    if (Program.errors != "")
                    {
                        Console.WriteLine(Program.errors);
                    }
                    int thenumber = 1;
                    try
                    {
                       thenumber = int.Parse(Console.ReadLine());
                    }
                    catch{}
                    Console.ForegroundColor = ConsoleColor.White;
                    if (thenumber == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        try
                        {
                            int deli = int.Parse(Console.ReadLine());
                            Program.array = Aditional.del(Program.array, deli);
                        }
                        catch{
                            Console.WriteLine("ERROR OR NOT FOUND ");
                            Thread.Sleep(250);
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (thenumber == 3)
                    {
                        try
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please don't forget this password");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Write the access password");
                            string passwordTemp = Console.ReadLine();
                            if (passwordTemp.Length >= 6)
                            {
                                Program.password = passwordTemp;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("YOUR PASSWORD MUST BE LONGER THAN 6 SYMBOLS");
                                Thread.Sleep(1000);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        catch
                        {}
                    }
                    if (thenumber == 4)
                    {
                        Program.errors = "";
                    }
                    if (thenumber == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("All your data will be erased");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Are you sure?");
                        Console.WriteLine("Y - Yes  N - No");
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (Console.ReadLine() == "Y")
                        {
                            System.Environment.Exit(0);
                        }
                        Console.ForegroundColor = ConsoleColor.White;
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
                    if (message != "WhatArray" && message != "Connection")
                    {
                        if (message.Substring(0, Program.password.Length) != Program.password)
                        {
                            message = "No";
                        }
                        else
                        {
                            message = message.Remove(0, Program.password.Length);
                        }
                    }
                    }
                    catch{}
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
