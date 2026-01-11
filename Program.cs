namespace WebbShop2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Show.Display();

            while (true)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Välkommen till den bästa WebbShoppen!");
                Console.ResetColor();

                ShopLayout.DrawLayout();
                ShopLayout.LogInLayout();

                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    case 'a':
                        Console.Clear();
                        Console.WriteLine("Du har köpt tröjan i ull. Tack för ditt köp!");
                        break;
                    case 'b':
                        Console.Clear();
                        Console.WriteLine("Du har köpt skjortan. Tack för ditt köp!");
                        break;
                    case 'c':
                        Console.Clear();
                        Console.WriteLine("Du har köpt byxorna. Tack för ditt köp!");
                        break;


                    case '0': Show.Sökning(); break;
                    case '1': Show.Tröjor(); break;
                    case '2': Show.Byxor(); break;
                    case '3': Show.Jackor(); break;

                    case 'x': KundSida.Start(); break;
                    case 'y': Admin.Start(); break;
                }
                Console.ReadLine();
            }
        }
    }
}
