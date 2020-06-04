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

            string text4 = "Velkommen til gæt tallet. \nFørst skal du skrive de 2 tal som du gerne vil finde et tal imellem. \nDerefter skal du skrive et tal, så vil jeg hjælpe dig med at fortælle dig \nom det tal som du leder efter er større eller mindre.";
            byte[] buffer4 = Encoding.UTF8.GetBytes(text4);
            stream.Write(buffer4, 0, buffer4.Length);

            int numberOfBytesRead1 = await stream.ReadAsync(buffer, 0, 255);
            string receivedMessage1 = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead1);

            int numberOfBytesRead2 = await stream.ReadAsync(buffer, 0, 255);
            string receivedMessage2 = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead2);

            Random random = new Random();
            int returnValue = random.Next(Convert.ToInt32(receivedMessage1), Convert.ToInt32(receivedMessage2));

            string text5 = "Der er nu blevet genereret et tal fra " + receivedMessage1 + " til og med " + receivedMessage2 + ". Held og lykke";
            byte[] buffer5 = Encoding.UTF8.GetBytes(text5);
            stream.Write(buffer5, 0, buffer5.Length);

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
                    string text = "Du gættede rigtig, tallet er: " + returnValue;
                    byte[] buffer1 = Encoding.UTF8.GetBytes(text);
                    stream.Write(buffer1, 0, buffer1.Length);
                    goto gohere;
                }
            }

        }
    }
}
