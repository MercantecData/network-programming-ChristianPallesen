using System;
using System.Text;

namespace FørsteOpgave
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Dette burde være et spørgsmål!";

            byte[] encoder = Encoding.UTF8.GetBytes(text);

            foreach (byte b in encoder)
            {
                Console.WriteLine(b);
            }


            var str = Encoding.UTF8.GetString(encoder);

            Console.WriteLine(str);
        }
    }
}
