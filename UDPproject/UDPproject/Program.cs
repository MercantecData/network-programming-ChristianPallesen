using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDPproject
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hello world";
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            UdpClient client = new UdpClient();

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

            client.Send(bytes, bytes.Length, endpoint);

        }

    }
}
