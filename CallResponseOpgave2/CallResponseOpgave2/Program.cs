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
                if (message != "NEW")
                {
                    Console.WriteLine(message);
                } else
                {
                    Console.WriteLine("Din tilknyttede client er disconnected.");
                    DisconnectFromClient(client, listener);
                    goto gohere;
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
