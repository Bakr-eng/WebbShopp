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

                //ShopLayout.DrawLayout();
                ShopLayout.LogInLayout();


                //ErbjudandeProdukter.VisaErbjudandeProdukter();
                //string input = Console.ReadLine().ToLower();
                //ErbjudandeProdukter.SeErbjudandeinfo(input);


                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    //case '0': ProduktVisning.Sökning(); break;
                    //case '1': ProduktVisning.VisaKategoriProdukter(1); break;
                    //case '2': ProduktVisning.VisaKategoriProdukter(2); break;
                    //case '3': ProduktVisning.VisaKategoriProdukter(3); break;

                    case 'x': KundSida.Start(); break;
                    case 'y': Admin.Start(); break;
                    //case 'v': VarukorgSidan.Show(); break;
                    

                }
                Console.ReadLine();
            }
        }
    }
}
