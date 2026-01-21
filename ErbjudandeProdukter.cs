using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;
using WindowDemo;

namespace WebbShop2
{
    internal class ErbjudandeProdukter
    {
        public static void VisaErbjudandeProdukter()
        {
            using (var db = new MyDbContext())
            {
                Console.SetCursorPosition(0, 0);
                var produkter = db.Produkter
                    .Where(p => p.Erbjudande == true)
                    .Include(ps => ps.ProduktStorlekar)
                    .ThenInclude(s => s.Storlek)
                    .ToList()
                    .Where(ps => ps.ProduktStorlekar.Any(ps => ps.EnheterIlager > 0))
                    .ToList();

                char index = 'a';
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
        public static void SeErbjudandeinfo(string input)
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


                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("fortsätt!");
                    return;

                }

                char choice = input[0];
                int produktIndex = choice - 'a';

                if (produktIndex >= 0 && produktIndex < produkter.Count)
                {
                    {
                        var valdProdukt = produkter[produktIndex];
                        Console.Clear();
                        Console.WriteLine("\x1b[3J");
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
                            var valdProdukt = produkter[produktIndex];
                            foreach (var ps in valdProdukt.ProduktStorlekar.Where(p => p.EnheterIlager > 0))
                            {
                                Console.SetCursorPosition(51, rad);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"{ps.Storlek.Namn}");
                                Console.ResetColor();
                                rad++;
                            }

                            string storlek = Console.ReadLine().ToUpper();

                            var produkt = produkter[produktIndex];

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
                                KundId = KundSida.InloggadKundId,
                                ProduktId = produkt.Id,
                                StorlekId = valdStorlek.StorlekId,
                                Antal = 1
                            };
                            db.Varukorgar.Add(varukorgItem);
                            db.SaveChanges();


                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Produkten har lagts till i varukorgen!");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            Console.WriteLine("\nTryck Enter för att återgå...");
                            break;
                    }
                } 
                else
                {
                    Console.WriteLine("Ogiltigt val! ");
                    
                }


            }
        }
    }
}
