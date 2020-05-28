using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace MultiClientServer2
{
    class Program
    {
        //List of all the connected clients
        public static List<TcpClient> clients = new List<TcpClient>();


        public static void Main(string[] args)
        {
            //Declarations
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 480;
            TcpListener listener = new TcpListener(ip, port);

            //Starts listening for clients and accepts them
            listener.Start();
            AcceptClients(listener);

            //Allows for the server to write to all connected clients
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Skriv en besked: ");
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);

                foreach (TcpClient client in clients)
                {
                    client.GetStream().Write(buffer, 0, buffer.Length);
                }
            }
        }
 
        //Function that accepts clients and reads their messages
        public static async void AcceptClients(TcpListener listener)
        {
            bool isRunning = true;
            while (isRunning)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                NetworkStream stream = client.GetStream();
                ReceiveMessage(stream);
            }
        }
        //Function to allow server to read clients message
        public static async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[255];
            bool isRunning = true;
            while (isRunning)
            {
                int read = await stream.ReadAsync(buffer, 0, buffer.Length);
                string text = Encoding.UTF8.GetString(buffer, 0, read);
                Console.WriteLine("Klienten skriver: " + text);
            }
        }
    }

}
