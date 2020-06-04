using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDPproject2
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver();
            Console.ReadLine();
        }
        public static async void Receiver()
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            UdpClient client = new UdpClient(endpoint);

            UdpReceiveResult result = await client.ReceiveAsync();

            byte[] buffer = result.Buffer;

            string text = Encoding.UTF8.GetString(buffer);
            Console.WriteLine("Modtog " + text);
        }
    
    }
}
