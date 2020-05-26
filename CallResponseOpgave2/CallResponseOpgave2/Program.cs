using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace CallResponseOpgave2
{
    class Program
    {
        static void Main(string[] args)
        {
            gohere:
            int port = 480;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);
            TcpListener listener = new TcpListener(localEndpoint);
            listener.Start();
            Console.WriteLine("Venter på forbindelser");
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[255];

            while (true)
            {
                int numberOfBytesRead = stream.Read(buffer, 0, 255);
                string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
                Console.WriteLine(message);

                if (message == "NEW")
                {
                    client.Close();
                    listener.Stop();
                    goto gohere;
                }

            }

        }
    }
}
