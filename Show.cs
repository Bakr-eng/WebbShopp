using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;
using WindowDemo;


namespace WebbShop2
{
    internal class Show
    {
        public static void Display()
        {
            while (true)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Välkommen till den bästa WebbShoppen!"); Console.ResetColor();

                ShopLayout.DrawLayout();

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

                    case '0': Sökning(); break;
                    case '1': Tröjor(); break;
                    case '2': Byxor(); break;
                    case '3': Jackor(); break;

                    case 'x': Admin.Start(); break;
                }
                Console.ReadLine();
            }
        }

        private static void Tröjor()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();


                var tröjor = db.Produkter
                    .Where(p => p.KategoriId == 1 && p.EnheterILager > 0)
                    .Include(p => p.Storlekar)
                    .ToList();

                int index = 1;
                foreach (var tröja in tröjor)
                {
                    Console.WriteLine($"[{index}]--------------------------");
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(
                     "          _________          \n" +
                     "         /         \\         \n" +
                     "    ____/   T‑SHIRT   \\____   \n" +
                     "   /    \\           /    \\   \n" +
                     "  /      \\         /      \\  \n" +
                     " |   __   \\_______/   __   | \n" +
                     " |  |  |             |  |  | \n" +
                     " |  |  |             |  |  | \n" +
                     " |  |__|             |__|  | \n" +
                     "  \\                        / \n" +
                     "   \\______________________/  \n"
                    ); Console.ResetColor();
                    Console.WriteLine($"{tröja.Namn}");
                    Console.WriteLine($"{tröja.Pris}kr");
                    Console.WriteLine();
                    index++;
                }


                Console.WriteLine("Välj en produkt för mer information (skriv numret): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= tröjor.Count)
                {
                    var valdProdukt = tröjor[choice - 1];

                    Console.Clear();
                    Console.WriteLine("=== Produktinformation ===");
                    Console.WriteLine($"Namn: {valdProdukt.Namn}");
                    Console.WriteLine($"Pris: {valdProdukt.Pris} kr");
                    Console.WriteLine($"Beskrivning: {valdProdukt.Beskrivning}");

                    string storlek = valdProdukt.Storlekar.FirstOrDefault()?.Namn ?? "-";
                    Console.WriteLine("Storlek: " + storlek);




                }
                else
                {
                    Console.WriteLine("Ogiltigt val.");
                }

                Console.WriteLine("\nTryck Enter för att återgå...");
                Console.ReadLine();
            }

        }
        private static void Byxor()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();

                var byxor = db.Produkter
                    .Where(p => p.KategoriId == 2 && p.EnheterILager > 0)
                    .Include(p => p.Storlekar)
                    .ToList();

                int index = 1;
                foreach (var byxa in byxor)
                {
                    Console.WriteLine($"[{index}]--------------------------");
                    Console.ForegroundColor = ConsoleColor.Blue; Console.Write(
                    "        ||||||||||||        \n" +
                    "        ||        ||        \n" +
                    "        ||        ||        \n" +
                    "        ||        ||        \n" +
                    "        ||        ||        \n" +
                    "        ||        ||        \n" +
                    "       / |        | \\       \n" +
                    "      /  |        |  \\      \n" +
                    "     /   |        |   \\     \n" +
                    "    /    |        |    \\    \n" +
                    "   /_____|        |_____\\   \n"
                    ); Console.ResetColor();
                    Console.WriteLine($"{byxa.Namn}");
                    Console.WriteLine($"{byxa.Pris}kr");
                    Console.WriteLine();
                    index++;
                }



                Console.WriteLine("Välj en produkt för mer information (skriv numret): ");
                string input = Console.ReadLine();

                if(int.TryParse(input, out int choice) && choice > 0 && choice <= byxor.Count)
                {
                    var valdProdukt = byxor[choice -1];

                    Console.Clear();
                    Console.WriteLine("=== Produktinformation ===");
                    Console.WriteLine($"Namn: {valdProdukt.Namn}");
                    Console.WriteLine($"Pris: {valdProdukt.Pris} kr");
                    Console.WriteLine($"Beskrivning: {valdProdukt.Beskrivning}");

                    string storlek = valdProdukt.Storlekar.FirstOrDefault()?.Namn ?? "-";
                    Console.WriteLine("Storlek: " + storlek);



                }
                else
                {
                    Console.WriteLine("Ogiltigt val.");
                }

                Console.WriteLine("\nTryck Enter för att återgå...");
                Console.ReadLine();
            }
        }
        private static void Jackor()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                var jackor = db.Produkter
                    .Where(P => P.KategoriId == 3 && P.EnheterILager > 0)
                    .Include(p => p.Storlekar)
                    .ToList();
                int index = 1;

                foreach (var jacka in jackor)
                {
                    Console.WriteLine($"[{index}]--------------------------");
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(
                    "        ________________        \n" +
                    "       /                \\       \n" +
                    "   ___/      JACKA       \\___   \n" +
                    "  /   |                |    \\  \n" +
                    " /    |      ||||      |     \\ \n" +
                    "|     |      ||||      |      |\n" +
                    "|     |      ||||      |      |\n" +
                    "|     |      ||||      |      |\n" +
                    " \\    |                |     / \n" +
                    "  \\___|________________|____/  \n"
                    ); Console.ResetColor();
                    Console.WriteLine($"Namn: {jacka.Namn}");
                    Console.WriteLine($"Pris: {jacka.Pris}kr");
                    Console.WriteLine();
                    index++;
                }

                Console.WriteLine("Välj en produkt för mer information (skriv numret): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= jackor.Count)
                {
                    var valdProdukt = jackor[choice - 1];
                    Console.Clear();

                    Console.WriteLine("=== Produktinformation ===");
                    Console.WriteLine($"Namn: {valdProdukt.Namn}");
                    Console.WriteLine($"Pris: {valdProdukt.Pris}kr");
                    Console.WriteLine($"Beskrivning: {valdProdukt.Beskrivning}");

                    string storlek = valdProdukt.Storlekar.FirstOrDefault()?.Namn ?? "-";
                    Console.WriteLine("Storlek: " + storlek);



                }
                else
                {
                    Console.WriteLine("Ogiltigt val.");
                }

                Console.WriteLine("\nTryck Enter för att återgå...");
                Console.ReadLine();


            }

        }
        private static void Sökning()
        {
            Console.Clear();
            Console.WriteLine("Sök efter en produkt: ");
            string searchTerm = Console.ReadLine().ToLower();

            if (searchTerm == "tröja" || searchTerm == "tröjor")
            {
                Tröjor();
            }
            else if ( searchTerm == "byxor" )
            {
                Byxor();
            }
            else if (searchTerm == "jacka" || searchTerm == "jackor")
            {
                Jackor();
            }
            else
            {
                Console.WriteLine("Inga träffar hittades.");
            }
        }

       

    }
}



