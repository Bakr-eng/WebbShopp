namespace WebbShop2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Välkommen till den bästa WebbShoppen!");
                Console.ResetColor();

                ShopLayout.DrawLayout();
                ShopLayout.LogInLayout();
                ErbjudandeProdukter.VisaErbjudandeProdukter();
                


                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    
                    case 't': Show.Tröjor(); break;
                    case 'b': Show.Byxor(); break;
                    case 'j': Show.Jackor(); break;
                    case 'x': KundSida.Start(); break;
                    case 'y': Admin.Start(); break;
                    case '0': Show.Sökning(); break;


                    default: ErbjudandeProdukter.SeErbjudandeinfo(); break;

                }
                Console.ReadLine();
            }
        }
    }
}
