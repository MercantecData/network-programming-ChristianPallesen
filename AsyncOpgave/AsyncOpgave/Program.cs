﻿using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace AsyncOpgave
{
    class Program
    {
        static void Main(string[] args)
        {
            //SERVER
            int port = 480;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(localEndpoint);

            listener.Start();

            Console.WriteLine("Venter på klienter");
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);

            Console.WriteLine("Skriv din besked her: ");

            while (true)
            {
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                stream.Write(buffer, 0, buffer.Length);
                
            }
         Console.ReadKey();
            



        
        }

        public static async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[255];
            while (true)
            {
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 255);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                Console.WriteLine("\n" + receivedMessage);
            }

        }
    }
}
