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
    
    internal class ProduktVisning
    {
        public static void Sökning()
        {
            Console.Clear();
            Console.WriteLine("Sök efter en produkt: ");
            string searchTerm = Console.ReadLine().ToLower();

            if (searchTerm == "tröja" || searchTerm == "tröjor")
            {
                VisaKategoriProdukter(1);
            }
            else if (searchTerm == "byxor")
            {
                VisaKategoriProdukter(2);
            }
            else if (searchTerm == "jacka" || searchTerm == "jackor")
            {
                VisaKategoriProdukter(3);
            }
            else
            {
                Console.WriteLine("Inga träffar hittades.");
            }
        }
        public static void VisaKategoriProdukter(int kategoriId)
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J"); //Rensa hela konsolens buffer
                var tröjor = db.Produkter
                    .Where(p => p.KategoriId == kategoriId)
                    .Include(p => p.ProduktStorlekar)
                    .ThenInclude(ps => ps.Storlek)
                    .ToList()
                    .Where(p => p.ProduktStorlekar.Any(ps => ps.EnheterIlager > 0))
                    .ToList();

                

                int index = 1;
                foreach (var tröja in tröjor)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine($"[{index}]--------------------------");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.ResetColor();

                    Console.WriteLine($"{tröja.Namn}");
                    Console.WriteLine($"{tröja.Pris}kr");
                    Console.WriteLine("\n");

                    index++;
                }
                Console.WriteLine("Välj en produkt för mer information (skriv numret): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= tröjor.Count)
                {
                    var valdProdukt = tröjor[choice - 1];
                    Console.Clear();

                    Console.WriteLine(HämtaBild(kategoriId));


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
                    Console.Clear();
                    Console.WriteLine("Ogiltigt val.");
                }

                LäggTillVarukorgen(tröjor, choice);
            }

        }
        public static void LäggTillVarukorgen(List<Produkt> tröjor, int choice)
        {
            using (var db = new MyDbContext())
            {

                ShopLayout.BuyLayout();

                var key = Console.ReadKey(true);
                switch (char.ToLower(key.KeyChar))
                {
                    case '0':
                        // Console.SetCursorPosition(50, 21);
                        Console.WriteLine("Välj storlek: ");

                        int rad = 22;
                        var valdProdukt = tröjor[choice - 1];
                        foreach (var ps in valdProdukt.ProduktStorlekar.Where(p => p.EnheterIlager > 0))
                        {
                            // Console.SetCursorPosition(51, rad);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{ps.Storlek.Namn}");
                            Console.ResetColor();
                            rad++;
                        }

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
                        break;
                }
            }
            Console.WriteLine("\nTryck Enter för att återgå...");
            Console.ReadLine();
        }
        private static List<string> HämtaBild(int id)
        {
            List<string> tröja = new List<string>
                {
                    "          _________          ",
                    "         /         \\         ",
                    "    ____/   T‑SHIRT   \\____   " ,
                    "   /    \\           /    \\   " ,
                    "  /      \\         /      \\  " ,
                    " |   __   \\_______/   __   | " ,
                    " |  |  |             |  |  | " ,
                    " |  |  |             |  |  | " ,
                    " |  |__|             |__|  | " ,
                    "  \\                        / " ,
                    "   \\______________________/  "
                };


            List<string> byxor = new List<string>
            {
                "        ||||||||||||        ",
                "        ||        ||        ",
                "        ||        ||        ",
                "        ||        ||        ",
                "        ||        ||        ",
                "        ||        ||        ",
                "       / |        | \\       ",
                "      /  |        |  \\      ",
                "     /   |        |   \\     ",
                "    /    |        |    \\    ",
                "   /_____|        |_____\\   "
            };

            List<string> jacka = new List<string>

            {
                 "        ________________        ",
                    "       /                \\       ",
                    "   ___/      JACKA       \\___   ",
                    "  /   |                |    \\  ",
                    " /    |      ||||      |     \\ ",
                    "|     |      ||||      |      |",
                    "|     |      ||||      |      |",
                    "|     |      ||||      |      |",
                    " \\    |                |     / ",
                    "  \\___|________________|____/  "
            };


            switch (id)
            {
                case 1:
                    Window windowTröja = new Window("", 0, 12, tröja);
                    windowTröja.Draw();
                    Console.SetCursorPosition(0, 0);
                    return tröja;

                case 2:
                    Window windowByxor = new Window("", 0, 12, byxor);
                    windowByxor.Draw();
                    Console.SetCursorPosition(0, 0);
                    return byxor;

                case 3:
                    Window windowJacka = new Window("", 0, 12, jacka);
                    windowJacka.Draw();
                    Console.SetCursorPosition(0, 0);
                    return jacka;

                default: return new List<string>();
            }
        }


    }
}
