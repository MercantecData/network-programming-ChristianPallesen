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

            Random random = new Random();

            int returnValue = random.Next(0, 100);

            //Declarations
            int port = 480;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);
            TcpListener listener = new TcpListener(localEndpoint);

            //Starts the listener and waits for a connection. If one is pending it gets accepted, using Tcp.
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            Console.WriteLine("Du er connected med en client, vent på modtagelse af besked.");

            byte[] buffer = new byte[255];

            while (true)
            {
                //Prints the message recieved from the Client after it has been encoded using UTF8
                int numberOfBytesRead = stream.Read(buffer, 0, 255);
                string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                int messageFromUser = Convert.ToInt32(message);
                if (messageFromUser < returnValue)
                {
                    Console.WriteLine("Du gættede forkert, tallet er højere end: " + messageFromUser);
                } else if (messageFromUser > returnValue)
                {
                    Console.WriteLine("Du gættede forkert, tallet er mindre end: " + messageFromUser);                    
                } else
                {
                    Console.WriteLine("Du gættede rigtig, tallet var: " + returnValue);
                }

            }

        }
        static void DisconnectFromClient(TcpClient client, TcpListener listener)
        {

            client.Close();
            listener.Stop();
        }
    }
}
