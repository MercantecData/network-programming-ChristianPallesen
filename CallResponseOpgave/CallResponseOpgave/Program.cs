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
            bool isActive = true;

            while (isActive) 
            {
            gohere:

                //Gets data from user
                Console.WriteLine("Skriv ip'en du vil connecte til: ");
                string ipFromUser = Console.ReadLine();
                Console.WriteLine("Skriv hvilken port du vil connecte til: ");
                int port = Convert.ToInt32(Console.ReadLine());

                //Declarations
                IPAddress ip = IPAddress.Parse(ipFromUser);
                IPEndPoint endPoint = new IPEndPoint(ip, port);
                TcpClient client = new TcpClient();

                //Connects to server
                NetworkStream stream = ConnectToServer(endPoint, client);
                gohere2:

                //Gets the message from user
                Console.WriteLine("Du er connected til server pc'en. Skriv din tekst: ");
                string text = Console.ReadLine();

                //Sends message to the server
                SendMessageToStream(text, stream);

                //Checks if the user wants to use the last endPoint or connect to a new one
                Console.WriteLine("Din besked er sendt! Hvis du vil sende en mere skriv LAST, ellers skrev NEW for at skrive til en ny person.");
                string inputFromUser = Console.ReadLine();
                if (inputFromUser == "LAST")
                {
                    goto gohere2;

                } else if (inputFromUser == "NEW")
                {
                    string text1 = "NEW";
                    NetworkStream stream1 = client.GetStream();
                    SendMessageToStream(text1, stream1);
                    goto gohere;
                }
                
            }

      
        }

        public static void SendMessageToStream(string text, NetworkStream stream)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);
        }

        public static NetworkStream ConnectToServer(IPEndPoint endPoint, TcpClient client)
        {
            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            return stream;
        }

    }
}
