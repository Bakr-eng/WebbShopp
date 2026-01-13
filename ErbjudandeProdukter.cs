using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;

namespace WebbShop2
{
    internal class ErbjudandeProdukter
    {
        public static void VisaErbjudandeProdukter()
        {
            using (var db = new MyDbContext())
            {
                var produkter = db.Produkter
                    .Where(p => p.Erbjudande == true)
                    .Include(ps => ps.ProduktStorlekar)
                    .ThenInclude(s => s.Storlek)
                    .ToList()
                    .Where(ps => ps.ProduktStorlekar.Any(ps => ps.EnheterIlager > 0))
                    .ToList();

                int index = 1;
                foreach( var p in produkter)
                {
                    if (p.KategoriId == 1)
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
                        Console.WriteLine($"{p.Namn}");
                        Console.WriteLine($"{p.Pris}kr");
                        Console.WriteLine();
                        index++;
                    }
                    else if (p.KategoriId == 2)
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
                        Console.WriteLine($"{p.Namn}");
                        Console.WriteLine($"{p.Pris}kr");
                        Console.WriteLine();
                        index++;
                    }
                    else if (p.KategoriId == 3)
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
                        Console.WriteLine($"Namn: {p.Namn}");
                        Console.WriteLine($"Pris: {p.Pris}kr");
                        Console.WriteLine();
                        index++;
                    }
                }
                
                
            }
        }
        public static void SeErbjudandeinfo()
        {
            using (var db = new MyDbContext())
            { 
            var produkter = db.Produkter
                   .Where(p => p.Erbjudande == true)
                   .Include(ps => ps.ProduktStorlekar)
                   .ThenInclude(s => s.Storlek)
                   .ToList()
                   .Where(ps => ps.ProduktStorlekar.Any(ps => ps.EnheterIlager > 0))
                   .ToList();
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= produkter.Count)
            {
                var valdProdukt = produkter[choice - 1];
                Console.Clear();
                Console.WriteLine("=== Produktinformation ===");
                Console.WriteLine($"Namn: {valdProdukt.Namn}");
                Console.WriteLine($"Pris: {valdProdukt.Pris}kr");
                Console.WriteLine($"Beskrivning: {valdProdukt.Beskrivning}");

                Console.WriteLine("\nStorlekar som finns:");
                foreach (var ps in valdProdukt.ProduktStorlekar.Where(p => p.EnheterIlager > 0))
                {
                    Console.WriteLine($"Storlek: {ps.Storlek.Namn}");
                }
                    Console.WriteLine("\nTryck Enter för att återgå...");
                }


            ShopLayout.BuyLayout();
            var Key = Console.ReadKey(true);
                switch (char.ToLower(Key.KeyChar))
                {
                    case '0':
                        Console.SetCursorPosition(50, 10);
                        Console.WriteLine("Välj storlekId: ");


                        int rad = 11;
                        var valdProdukt = produkter[choice - 1];
                        foreach (var ps in valdProdukt.ProduktStorlekar.Where(p => p.EnheterIlager > 0))
                        {
                            Console.SetCursorPosition(51, rad);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{ps.Storlek.Namn}");
                            Console.ResetColor();
                            rad++;
                        }

                        string storlek = Console.ReadLine().ToUpper();

                        var produkt = produkter[choice - 1];

                        var valdStorlek = produkt.ProduktStorlekar
                            .FirstOrDefault(ps => ps.Storlek.Namn == storlek && ps.EnheterIlager > 0);

                        if (valdStorlek == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Denna storlek finns inte längre i lager!");
                            Console.ResetColor();
                            break;
                        }

                        var varukorgItem = new Varukorg
                        {
                            ProduktId = produkt.Id,
                            StorlekId = valdStorlek.StorlekId,
                            Antal = 1
                        };
                        db.Varukorgar.Add(varukorgItem);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Produkten har lagts till i varukorgen!");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        Console.WriteLine("\nTryck Enter för att återgå...");
                        break;
                        
                }
            }
        }
    }
}
