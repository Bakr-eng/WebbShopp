using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Migrations;
using WebbShop2.Models;
using WindowDemo;


namespace WebbShop2
{
    internal class Show
    {
        public static void Display()
        {
           
        }

        public static void Tröjor()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();

                var tröjor = db.Produkter
                    .Where(p => p.KategoriId == 1)
                    .Include(p => p.ProduktStorlekar)
                         .ThenInclude(ps => ps.Storlek)
                    .ToList()
                    .Where(p =>p.ProduktStorlekar.Any(ps => ps.EnheterIlager > 0)) 
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

                    Console.WriteLine("\nStorlekar som finns:");

                    foreach (var ps in valdProdukt.ProduktStorlekar.Where(p => p.EnheterIlager > 0))
                    {
                        Console.WriteLine($"Storlek: {ps.Storlek.Namn}");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt val.");
                }

                ShopLayout.BuyLayout();
                
                var key = Console.ReadKey(true);
                switch (char.ToLower(key.KeyChar))
                {
                    case '0':
                        Console.SetCursorPosition(50, 10);
                        Console.WriteLine("Välj storlek: ");
                        Console.SetCursorPosition(50, 11);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("S  M  L  XL");
                        Console.ResetColor();
                        string storlek = Console.ReadLine().ToUpper();

                        var produkt = tröjor[choice - 1];

                        var valdStorlek = produkt.ProduktStorlekar
                            .FirstOrDefault(ps => ps.Storlek.Namn == storlek && ps.EnheterIlager > 0);

                        if (valdStorlek == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Denna storlek finns inte längre i lager:");
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
                        break;

                }

                Console.WriteLine("\nTryck Enter för att återgå...");
            }

        }
        public static void Byxor()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();

                var byxor = db.Produkter
                    .Where(p => p.KategoriId == 2)
                    .Include(p => p.ProduktStorlekar)
                         .ThenInclude(ps => ps.Storlek)
                    .ToList()
                    .Where(p => p.ProduktStorlekar.Any(ps => ps.EnheterIlager > 0))
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

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= byxor.Count)
                {
                    var valdProdukt = byxor[choice - 1];

                    Console.Clear();
                    Console.WriteLine("=== Produktinformation ===");
                    Console.WriteLine($"Namn: {valdProdukt.Namn}");
                    Console.WriteLine($"Pris: {valdProdukt.Pris} kr");
                    Console.WriteLine($"Beskrivning: {valdProdukt.Beskrivning}");

                    Console.WriteLine("\nStorlekar Som finns:");
                    foreach (var ps in valdProdukt.ProduktStorlekar.Where(p => p.EnheterIlager > 0))
                    {
                        Console.WriteLine($"Storlek: {ps.Storlek.Namn}");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt val.");
                }

                ShopLayout.BuyLayout();
                var key = Console.ReadKey(true);
                switch (char.ToLower(key.KeyChar))
                {
                    case '0':
                        Console.SetCursorPosition(50, 11);
                        Console.WriteLine("Välj storlekId: ");
                        Console.SetCursorPosition(50, 12);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("S  M  L  XL");
                        Console.ResetColor();

                        string storlek = Console.ReadLine().ToUpper();

                        var produkt = byxor[choice - 1];

                        var valdStorlek = produkt.ProduktStorlekar
                            .FirstOrDefault(ps => ps.Storlek.Namn == storlek && ps.EnheterIlager > 0);

                        if (valdStorlek == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Denna storlek finns inte längre i lager:");
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
                        Console.WriteLine("Produkten har lagts till i varukorgen! ");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        break;
                    
                }

                Console.WriteLine("\nTryck Enter för att återgå...");
            }
        }
        public static void Jackor()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                var jackor = db.Produkter
                    .Where(P => P.KategoriId == 3)
                    .Include(p => p.ProduktStorlekar)
                         .ThenInclude(ps => ps.Storlek)
                    .ToList()
                    .Where(p => p.ProduktStorlekar.Any(ps => ps.EnheterIlager > 0))
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

                    Console.WriteLine("\nStorlekar som finns:");
                    foreach (var ps in valdProdukt.ProduktStorlekar.Where(p => p.EnheterIlager > 0))
                    {
                        Console.WriteLine($"Storlek: {ps.Storlek.Namn}");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt val.");
                }

                ShopLayout.BuyLayout();
                var Key = Console.ReadKey(true);
                switch (char.ToLower(Key.KeyChar))
                {
                    case '0':
                        Console.SetCursorPosition(50, 11);
                        Console.WriteLine("Välj storlekId: ");
                        Console.SetCursorPosition(50, 12);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("S  M  L  XL");
                        Console.ResetColor();

                        string storlek = Console.ReadLine().ToUpper();

                        var produkt = jackor[choice -1];

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
                        break;
                }
                
                Console.WriteLine("\nTryck Enter för att återgå...");


            }

        }
        public static void Sökning()
        {
            Console.Clear();
            Console.WriteLine("Sök efter en produkt: ");
            string searchTerm = Console.ReadLine().ToLower();

            if (searchTerm == "tröja" || searchTerm == "tröjor")
            {
                Tröjor();
            }
            else if (searchTerm == "byxor")
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



