using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace GuessTheNumber2
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

            Console.WriteLine("Venter på klienter.");
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
            gohere:
            byte[] buffer = new byte[255];

            Random random = new Random();

            int returnValue = random.Next(0, 100);

            while (true)
            {

                Console.WriteLine("Nummeret er: " + returnValue);

                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 255);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                Console.WriteLine("\n" + receivedMessage);

                int StringToInt = Convert.ToInt32(receivedMessage);

                if (StringToInt < returnValue)
                {
                    Console.WriteLine("Du gættede forkert, tallet er højere end: " + StringToInt);
                    string text = "Du gættede forkert, tallet er højere end: " + StringToInt;
                    byte[] buffer1 = Encoding.UTF8.GetBytes(text);
                    stream.Write(buffer1, 0, buffer1.Length);
                } else if (StringToInt > returnValue)
                {
                    Console.WriteLine("Du gættede forkert, tallet er mindre end: " + StringToInt);
                    string text = "Du gættede forkert, tallet er mindre end: " + StringToInt;
                    byte[] buffer1 = Encoding.UTF8.GetBytes(text);
                    stream.Write(buffer1, 0, buffer1.Length);
                } else if (StringToInt == returnValue)
                {
                    Console.WriteLine("Du gættede rigtig, tallet er: " + returnValue);
                    string text = "Du gættede rigtig, tallet er: " + returnValue + ". Der er kommet et nyt nummer som du skal gætte.";
                    byte[] buffer1 = Encoding.UTF8.GetBytes(text);
                    stream.Write(buffer1, 0, buffer1.Length);
                    goto gohere;
                }
            }

        }
    }
}
