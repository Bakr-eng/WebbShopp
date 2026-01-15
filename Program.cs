using WindowDemo;

namespace WebbShop2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                //List<string> bild = new List<string>
                //{
                //    "          _________          ",
                //     "         /         \\         ",
                //     "    ____/   T‑SHIRT   \\____   " ,
                //     "   /    \\           /    \\   " ,
                //     "  /      \\         /      \\  " ,
                //     " |   __   \\_______/   __   | " ,
                //     " |  |  |             |  |  | " ,
                //     " |  |  |             |  |  | " ,
                //     " |  |__|             |__|  | " ,
                //     "  \\                        / " ,
                //     "   \\______________________/  "
                //};
                //Window bl = new Window("", 2, 2, bild);
                //bl.Draw();
                //Console.ReadKey();


                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Välkommen till den bästa WebbShoppen!");
                Console.ResetColor();

                ShopLayout.DrawLayout();
                ShopLayout.LogInLayout();
               // ErbjudandeProdukter.VisaErbjudandeProdukter();
                


                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    
                  //  case 't': Show.Tröjor(); break;
                  ////  case 'b': Show.Byxor(); break;
                  ////  case 'j': Show.Jackor(); break;
                  //  case 'x': KundSida.Start(); break;
                  //  case 'y': Admin.Start(); break;
                  //  case '0': Show.Sökning(); break;

                    case '1': ProduktVisning.VisaKategoriProdukter(1); break;
                    case '2': ProduktVisning.VisaKategoriProdukter(2); break;
                    case '3': ProduktVisning.VisaKategoriProdukter(3); break;

                    case 'x': KundSida.Start(); break;
                    case 'y': Admin.Start(); break;
                    case '0': ProduktVisning.Sökning(); break;

                        // default: ErbjudandeProdukter.SeErbjudandeinfo(); break;

                }
                Console.ReadLine();
            }
        }
    }
}
