using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;

namespace WebbShop2
{
    internal class Admin
    {
        public static void Start()
        {
            Console.Clear();
            while (true)
            {

                Console.Clear();
                Console.WriteLine("Välkommen till Adminpanelen");
                Console.WriteLine("---------------------------");
                ShopLayout.AdminLayout();

                var key = Console.ReadKey();

                switch (char.ToLower(key.KeyChar))
                {
                    case '1': VisaProdukter(); break;
                    case '2': LäggTillProdukt(); break;
                    case '3': TaBortProdukt(); break;
                    case '4': Uppdatering(); break;
                    case 'q': return;
                }
                Console.WriteLine("Tryck på Enter");
                Console.ReadLine();
            }
        }
        public static void VisaProdukter()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                var produkter = db.Produkter
                    .Include(p => p.Storlekar)
                    .ToList();

                Console.WriteLine("------------------------------------------------------------------------------------");
                Console.WriteLine($"| {"ID",-5} | {"Namn",-20} | {"Pris",-10} | {"KategoriId",-3}| {"Storlekar",-8} | {"EnheterILager",-10}| ");
                Console.WriteLine("------------------------------------------------------------------------------------");

                foreach (var produkt in produkter)
                {
                    string storlekarText = string.Join(", ", produkt.Storlekar.Select(s => s.Namn));
                    Console.WriteLine($"| {produkt.Id,-5} | {produkt.Namn,-20} | {produkt.Pris,-10} | {produkt.KategoriId,-9} | " +
                        $"{storlekarText,-10}| {produkt.EnheterILager,-12} |");
                }
                Console.WriteLine("------------------------------------------------------------------------------------");
            }
        }
        private static void LäggTillProdukt()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("Adminpanalen - Lägg till produkt");
                Console.WriteLine("-------------------------------");
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Lägg till produkt"); Console.ResetColor();
                Console.WriteLine("----------------");
                Console.Write("Ange produktnamn: ");
                string namn = Console.ReadLine();
                Console.Write("Ange pris: ");
                decimal pris = decimal.Parse(Console.ReadLine());
                Console.Write("Ange kategoriId: ");
                int kategoriId = int.Parse(Console.ReadLine());


                Console.Write("Ange storlekId: ");
                int storlekId = int.Parse(Console.ReadLine());


                Console.Write("Ange beskrivning: ");
                string beskrivning = Console.ReadLine();
                Console.Write("Ange antal enheter i lager: ");
                int enheterILager = int.Parse(Console.ReadLine());


                var storlek = db.Storlekar.FirstOrDefault(s => s.Id == storlekId);
                var nyProdukt = new Produkt
                {
                    Namn = namn,
                    Pris = pris,
                    KategoriId = kategoriId,
                    Beskrivning = beskrivning,
                    EnheterILager = enheterILager
                };
                if (storlek != null)
                {
                    nyProdukt.Storlekar.Add(storlek);

                }
                db.Produkter.Add(nyProdukt);

                try
                {
                    db.SaveChanges();
                    Console.Write($"Produkten '{namn}' med pris {pris} kr, kategori '{kategoriId}' storlek '{storlekId}' och beskrivning '{beskrivning}'");
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(" har lagts till i databasen."); Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("nogot gich fel: " + ex.InnerException);

                }
            }
        }
        private static void TaBortProdukt()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("Adminpanalen - Ta bort produkt");
                Console.WriteLine("-------------------------------");
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Ta bort produkt"); Console.ResetColor();
                Console.WriteLine("----------------");
                Console.Write("Ange produktens ID som du vill ta bort: ");
                int produktId = int.Parse(Console.ReadLine());
                var produkt = db.Produkter.Where(p => p.Id == produktId).FirstOrDefault();
                if (produkt != null)
                {
                    db.Produkter.Remove(produkt);
                    db.SaveChanges();
                    Console.WriteLine($"Produkten med ID {produktId} har tagits bort.");
                }
                else
                {
                    Console.WriteLine($"Ingen produkt hittades med ID {produktId}.");
                }
            }
        }
        private static void Uppdatering()
        {
            
            Console.Clear();
            Console.WriteLine("Adminpanalen - Uppdatera produkt");
            Console.WriteLine("-------------------------------");
            ShopLayout.AdminUpdateLayout();

            var key = Console.ReadKey();
            switch (char.ToLower(key.KeyChar))
            {
                case '1': AdminUpdate.Namn(); break;
                case '2': AdminUpdate.Pris(); break;
            }
        }

       
    }
}
