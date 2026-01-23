using System.Security.Cryptography;
using WindowDemo;

namespace WebbShop2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {


                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Välkommen till den bästa WebbShoppen!");
                Console.ResetColor();
                ShopLayout.LogInLayout();
                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    case 'x': KundSida.Start(); break;
                    case 'y': Admin.Start(); break;
                }
                Console.ReadLine();
            }
        }
    }
}
