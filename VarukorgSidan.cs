using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebbShop2.Models;

namespace WebbShop2
{
    internal class VarukorgSidan
    {
        public static void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");

                using (var db = new MyDbContext())
                {

                    var kundVarukorgen = db.Varukorgar
                        .Include(p => p.Produkt)
                        .ThenInclude(ps => ps.ProduktStorlekar)
                        .ThenInclude(s => s.Storlek)
                        .Include(k => k.Produkt.Kategori)
                        .Include(k => k.Kund)
                        .ToList();




                    ShopLayout.BuyLayout();
                    ShopLayout.ShoppingCartLayout();
                    Console.SetCursorPosition(0, 0);
                    var totalPris = kundVarukorgen.Sum(s => s.Produkt.Pris * s.Antal);
                    int radNummer = 1;
                    foreach (var rad in kundVarukorgen)
                    {
                        Console.WriteLine($"[{radNummer}]--------------------------");
                        Console.WriteLine(rad.Produkt.Namn);
                        Console.WriteLine(rad.Produkt.Pris + "kr\t  antal: " + rad.Antal + "\n\n");
                        radNummer++;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"\n\n\tTotalt pris: {totalPris:0.00}");
                    Console.ResetColor();

                    if (!kundVarukorgen.Any())
                    {
                        Console.SetCursorPosition(40, 10);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Din varukorg är tom.");
                        Console.ResetColor();
                        Console.ReadKey();
                        return;
                    }

                    var key = Console.ReadKey(true);
                    switch (char.ToLower(key.KeyChar))
                    {
                        case '1': ÄndraProduktAntal(kundVarukorgen, db); break;
                        case '2': TaBortProdukt(kundVarukorgen, db); break;
                        case '3':  VisaProduktInformation(kundVarukorgen); break;
                        case 'q': return;
                    }
                    Console.ReadLine();
                }
            }
        }
        public static void ÄndraProduktAntal(List<Varukorg> kundVarukorgen, MyDbContext db)
        {
            Console.Write("\nVilken produkt ska du ändra antalet? ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int valIndex) && valIndex > 0 && valIndex <= kundVarukorgen.Count)
            {
                var valdProdukt = kundVarukorgen[valIndex - 1];
                int produktId = valdProdukt.ProduktId;
                int storlekId = valdProdukt.StorlekId;

                int enheterILager = valdProdukt.Produkt.ProduktStorlekar
                    .Where(p => p.ProduktId == produktId && p.StorlekId == storlekId)
                    .Select(p => p.EnheterIlager)
                    .SingleOrDefault();

                Console.WriteLine(enheterILager + " som finns i lager.");
                Console.Write("välj antal: ");
                int nyttAntal = int.Parse(Console.ReadLine());

                if (nyttAntal < 1 || nyttAntal > enheterILager)
                {
                    Console.WriteLine("ogiltigt antal.");
                    Console.ReadKey();


                }

                valdProdukt.Antal = nyttAntal;
                db.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Antalet har ändrat!");
                Console.ResetColor();


            }
        }
        public static void TaBortProdukt(List<Varukorg> kundVarukorgen, MyDbContext db)
        {
            Console.Write("Vilken produkt vill du ta borta? skriv Id: ");
            string tabortId = Console.ReadLine();
            if (int.TryParse(tabortId, out int valIndex) && valIndex > 0 && valIndex <= kundVarukorgen.Count)
            {
                var valdProdukt = kundVarukorgen[valIndex - 1];


                if (valdProdukt != null)
                {
                    db.Varukorgar.Remove(valdProdukt);
                    db.SaveChanges();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" produkten: " + valdProdukt.Produkt.Namn + " har tagits bort!");
                    Console.ResetColor();
                }
            }
        }
        public static void VisaProduktInformation(List<Varukorg> kundVarukorgen)
        {
            Console.WriteLine("Välj en produkt för mer information (skriv numret): ");
            string infoId = Console.ReadLine();
            if (int.TryParse(infoId, out int valIndex) && valIndex > 0 && valIndex <= kundVarukorgen.Count)
            {
                var valdProdukt = kundVarukorgen[valIndex - 1];
                Console.Clear();
                Console.WriteLine("=== Produktinformation ===");
                Console.WriteLine($"Namn: {valdProdukt.Produkt.Namn}");
                Console.WriteLine($"Pris: {valdProdukt.Produkt.Pris} kr");
                Console.WriteLine($"Beskrivning: {valdProdukt.Produkt.Beskrivning}");
                Console.WriteLine($"Antal: {valdProdukt.Antal}");
                Console.WriteLine($"Storleken: {valdProdukt.Storlek.Namn}");
            }
        }
    }
}

    



   

