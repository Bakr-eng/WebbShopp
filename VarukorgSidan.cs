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

                    var varukorgen = db.Varukorgar
                        .Include(p => p.Produkt)
                        .ThenInclude(ps => ps.ProduktStorlekar)
                        .ThenInclude(s => s.Storlek)
                        .Include(k => k.Produkt.Kategori)
                        .Include(k => k.Kund)
                        .ToList();




                    ShopLayout.BuyLayout();
                    ShopLayout.ShoppingCartLayout();
                    Console.SetCursorPosition(0, 0);
                    var totalVarukorgPris = varukorgen.Sum(s => s.Produkt.Pris * s.Antal);
                    int index = 1;
                    foreach (var v in varukorgen)
                    {
                        Console.WriteLine($"[{index}]--------------------------");
                        Console.WriteLine(v.Produkt.Namn);
                        Console.WriteLine(v.Produkt.Pris + "kr\t  antal: " + v.Antal + "\n\n");
                        index++;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"\n\n\tTotalt pris: {totalVarukorgPris:0.00}");
                    Console.ResetColor();

                    if (!varukorgen.Any())
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
                        case '1': ÄndraAntal(varukorgen, db); break;
                        case '2': TaBortProdukter(varukorgen, db); break;
                        case '3':  VisaProduktInfo(varukorgen); break;
                        case 'q': return;
                    }
                    Console.ReadLine();
                }
            }
        }
        public static void ÄndraAntal(List<Varukorg> varukorgen, MyDbContext db)
        {
            Console.Write("\nVilken produkt ska du ändra antalet? ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int result) && result > 0 && result <= varukorgen.Count)
            {
                var valdProdukt = varukorgen[result - 1];
                int produktId = valdProdukt.ProduktId;
                int storlekId = valdProdukt.StorlekId;

                int enheterILager = valdProdukt.Produkt.ProduktStorlekar
                    .Where(p => p.ProduktId == produktId && p.StorlekId == storlekId)
                    .Select(p => p.EnheterIlager)
                    .SingleOrDefault();

                Console.WriteLine(enheterILager + " som finns i lager.");
                Console.Write("välj antal: ");
                int antalProdukt = int.Parse(Console.ReadLine());

                if (antalProdukt < 1 || antalProdukt > enheterILager)
                {
                    Console.WriteLine("ogiltigt antal.");
                    Console.ReadKey();


                }

                valdProdukt.Antal = antalProdukt;
                db.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Antalet har ändrat!");
                Console.ResetColor();


            }
        }
        public static void TaBortProdukter(List<Varukorg> varukorgen, MyDbContext db)
        {
            Console.Write("Vilken produkt vill du ta borta? skriv Id: ");
            string tabortId = Console.ReadLine();
            if (int.TryParse(tabortId, out int choiceTaBort) && choiceTaBort > 0 && choiceTaBort <= varukorgen.Count)
            {
                var valdProdukt = varukorgen[choiceTaBort - 1];


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
        public static void VisaProduktInfo(List<Varukorg> varukorgen)
        {
            Console.WriteLine("Välj en produkt för mer information (skriv numret): ");
            string infoId = Console.ReadLine();
            if (int.TryParse(infoId, out int choice) && choice > 0 && choice <= varukorgen.Count)
            {
                var valdProdukt = varukorgen[choice - 1];
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

    



   

