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
            string AdminNamn = "Bakr Khalil";
            int password = 0000;
          
            while (true)
            {
                
                Console.Clear();
                Console.WriteLine("Ange ditt namn: ");
                string inputNamn = Console.ReadLine();
                Console.WriteLine("Ange ditt lösenord: ");
                int inputPassword = int.Parse(Console.ReadLine());

                if (inputNamn == AdminNamn && inputPassword == password)

                {
                    Console.Clear();
                    Console.WriteLine($"Hej {AdminNamn}!");
                    Console.WriteLine($"Välkommen till Adminpanelen");
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
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Fel namn eller lösenord, försök igen."); Console.ResetColor();
                    Thread.Sleep(3000);
                }
            }
        }
        public static void VisaProdukter()
        {
            string Trim(string text, int max)
            {
                if (text.Length <= max)
                {
                    return text;
                }
                else
                {
                    return text.Substring(0, max - 3) + "...";
                }
            }

            using (var db = new MyDbContext())
            {
                Console.Clear();
                var produkter = db.Produkter
                    .Include(p => p.Storlekar)
                    .Include(p => p.Leverantor)
                    .ToList();

                Console.WriteLine("------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"| {"ID",-5} | {"Namn",-20} | {"Pris",-10} | {"KategoriId",-3}| {"Storlekar",-8} | {"EnheterILager",-10}| {"Leverantör", -15} |");
                Console.WriteLine("------------------------------------------------------------------------------------------------------");

                foreach (var produkt in produkter)
                {
                    string namn = Trim(produkt.Namn, 20);
                    string storlekarText = string.Join(", ", produkt.Storlekar.Select(s => s.Namn)); 
                    string leverantorNamn = string.Join(", ", produkt.Leverantor != null ? produkt.Leverantor.Namn : "Ingen leverantör");

                    Console.WriteLine($"| {produkt.Id,-5} | {namn,-20} | {produkt.Pris,-10} | {produkt.KategoriId,-9} | " +
                        $"{storlekarText,-10}| {produkt.EnheterILager,-12} | {leverantorNamn, -15} |");
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------");
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
                Console.Write("Ange beskrivning: ");
                string beskrivning = Console.ReadLine();
                Console.Write("Ange antal enheter i lager: ");
                int enheterILager = int.Parse(Console.ReadLine());


                Console.Write("Ange storlekId: ");
                int storlekId = int.Parse(Console.ReadLine());

                Console.WriteLine("Ange leverantör: ");
                Console.WriteLine(
                    "1. Adidas\n" +
                    "2. Nike\n" +
                    "3. Zara\n" +
                    "4. NordicWear AB\n" +
                    "5. ScandiJeans"
                    );
                int leverantorId = int.Parse(Console.ReadLine());




                var storlek = db.Storlekar.FirstOrDefault(s => s.Id == storlekId);
                var nyProdukt = new Produkt
                {
                    Namn = namn,
                    Pris = pris,
                    KategoriId = kategoriId,
                    Beskrivning = beskrivning,
                    EnheterILager = enheterILager,
                    LeverantorId = leverantorId
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
                case '3': AdminUpdate.Infotext(); break;
                case '4': AdminUpdate.Leverantör(); break;
            }
        }

       
    }
}
