using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace CallResponseOpgave
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();

            int port = 13356;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            string text = "Dette er en test besked!";
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);
            client.Close();
        }
    }
}
