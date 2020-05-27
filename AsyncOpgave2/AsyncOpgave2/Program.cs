using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace AsyncOpgave2
{
    class Program
    {
        static void Main(string[] args)
        {
            //CLIENT
            TcpClient client = new TcpClient();

            int port = 480;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);

            Console.WriteLine("Skriv din besked her: ");
            while (true)
            {
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                stream.Write(buffer, 0, buffer.Length);

            }
            client.Close();

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
