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
                TcpClient client = new TcpClient();
                Console.WriteLine("Skriv ip'en du vil connecte til: ");
                string ipFromUser = Console.ReadLine();
                Console.WriteLine("Skriv hvilken port du vil connecte til: ");
                int port = Convert.ToInt32(Console.ReadLine());
                IPAddress ip = IPAddress.Parse(ipFromUser);
                IPEndPoint endPoint = new IPEndPoint(ip, port);

                client.Connect(endPoint);
                gohere2:
                Console.WriteLine("Du er connected til server pc'en. Skriv din tekst: ");
                string text = Console.ReadLine();
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                stream.Write(buffer, 0, buffer.Length);

                Console.WriteLine("Din besked er sendt, hvis du vil sende en mere skriv LAST, ellers skrev NEW for at skrive til en ny person.");
                string inputFromUser = Console.ReadLine();
                if (inputFromUser == "LAST")
                {
                    goto gohere2;

                } else if (inputFromUser == "NEW")
                {

                    string text1 = "NEW";
                    NetworkStream stream1 = client.GetStream();
                    byte[] buffer1 = Encoding.UTF8.GetBytes(text1);
                    stream.Write(buffer1, 0, buffer1.Length);

                    goto gohere;
                }
                //client.Close();
            }

      
        }
    }
}
