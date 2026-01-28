using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;
using Dapper;
using Microsoft.Data.SqlClient;


namespace WebbShop2
{
    internal class Admin
    {
        public static void Start()
        {
            string AdminNamn = "Bakr";
            var password = "Bakr";
            while (true)
            {
                Console.Clear();
                Console.Write("Ange ditt namn: ");
                string inputNamn = Console.ReadLine();

                Console.Write("Ange ditt lösenord: ");
                string inputPassword = LäsDoltLösenord();

                if (inputNamn == AdminNamn && inputPassword == password)

                {
                   AdminPanalen(AdminNamn);
                   return;
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Fel namn eller lösenord, försök igen.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }
        public static string LäsDoltLösenord()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1); // Ta bort sista tecknet
                    Console.Write("\b \b"); // Ta bort asterisken från konsolen
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;


        }
        public static void AdminPanalen(string AdminNamn)
        {
            while (true)
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
                    case '4': UppdateraProdukter(); break;
                    case '5': UppdateraKundInfo(); break;
                    case '6': BästSäljandeProdukter(); break;

                    case 'q': return;
                }
                Console.WriteLine("Tryck på Enter");
                Console.ReadLine();
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
                Console.WriteLine("\x1b[3J");

                var produkter = db.Produkter
                    .Include(p => p.ProduktStorlekar)
                        .ThenInclude(ps => ps.Storlek)
                    .Include(p => p.Leverantor)
                    .ToList();

                Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"| {"ID",-5} | {"Namn",-16} | {"Pris",-9} | {"KategoriId",-3}| {"Storlekar",-8} | {"Lager",-13}| {"Leverantör",-13} | {"Erbjudande", -10} |");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------");

                foreach (var produkt in produkter)
                {
                    string namn = Trim(produkt.Namn, 16);
                    string storlekarText = string.Join(", ", produkt.ProduktStorlekar.Select(ps => ps.Storlek.Namn));
                    string storlek = Trim(storlekarText, 10);
                    string lagerText = string.Join(", ", produkt.ProduktStorlekar.Select(ps => $"{ps.Storlek.Namn}:{ps.EnheterIlager}"));
                    string lager = Trim(lagerText, 12);
                    string leverantorNamn = string.Join(", ", produkt.Leverantor != null ? produkt.Leverantor.Namn : "Ingen leverantör");

                
                    Console.Write($"| {produkt.Id,-5} | {namn,-16} | {produkt.Pris,-9} | {produkt.KategoriId,-9} | " +
                        $"{storlek,-10}| {lager,-12} | {leverantorNamn,-13} |");

                    if (produkt.Erbjudande == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" {produkt.Erbjudande,-10}");
                        Console.ResetColor();
                    }
                    else if (produkt.Erbjudande == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($" {produkt.Erbjudande,-10}");
                        Console.ResetColor();
                    }
                    Console.WriteLine(" |");



                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            }
        }
        private static void LäggTillProdukt()
        {
            using (var db = new MyDbContext())
            {
                try
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

                    Console.WriteLine("Ange kategoriId: ");
                    Console.WriteLine(
                        "1. Tröjor\n" +
                        "2. Byxor\n" +
                        "3. Jackor"
                        );
                    int kategoriId = int.Parse(Console.ReadLine());

                    Console.Write("Ange beskrivning: ");
                    string beskrivning = Console.ReadLine();


                    Console.WriteLine("Ange leverantörId: ");
                    Console.WriteLine(
                        "1. Adidas\n" +
                        "2. Nike\n" +
                        "3. Zara\n" +
                        "4. NordicWear AB\n" +
                        "5. ScandiJeans"
                        );
                    int leverantorId = int.Parse(Console.ReadLine());

                    var nyProdukt = new Produkt
                    {
                        Namn = namn,
                        Pris = pris,
                        KategoriId = kategoriId,
                        Beskrivning = beskrivning,
                        LeverantorId = leverantorId
                    };
                    db.Produkter.Add(nyProdukt);
                    db.SaveChanges();

                    Console.Clear();
                    Console.WriteLine($"Produkten '{namn}' skapad!");
                    Console.WriteLine("Nu lägger vi till storlekar.");
                    Console.WriteLine("-----------------------");

                    while (true)
                    {
                        Console.WriteLine("\nTillgängliga storlekar:");
                        foreach (var s in db.Storlekar)
                        {
                            Console.WriteLine($"{s.Id}. {s.Namn}");
                        }
                        Console.Write("Ange storlek-ID (eller 0 för att avsluta): ");
                        int storlekId = int.Parse(Console.ReadLine());
                        if (storlekId == 0)
                        {
                            break;
                        }

                        Console.Write("Ange antal i lager för denna storlek: ");
                        int antal = int.Parse(Console.ReadLine());

                        var ps = new ProduktStorlek
                        {
                            ProduktId = nyProdukt.Id,
                            StorlekId = storlekId,
                            EnheterIlager = antal
                        };
                        db.ProduktStorlekar.Add(ps);
                        db.SaveChanges();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Storlek tillagd!");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nProdukten '{namn}'är nu helt klar med storlekar!");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Något gick fel: " + ex.InnerException);
                    Console.ResetColor();
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
        private static void UppdateraProdukter()
        {
            
            Console.Clear();
            Console.WriteLine("Adminpanalen - Uppdatera produkt");
            Console.WriteLine("-------------------------------");
            ShopLayout.AdminUpdateLayout();

            var key = Console.ReadKey();
            switch (char.ToLower(key.KeyChar))
            {
                case '1': ProductsUpdate.Namn(); break;
                case '2': ProductsUpdate.Pris(); break;
                case '3': ProductsUpdate.Infotext(); break;
                case '4': ProductsUpdate.Leverantör(); break;
                case '5': ProductsUpdate.HanteraErbjudande(); break;

            }
        }
        private static void UppdateraKundInfo()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Console.WriteLine("Adminpanalen - Uppdatera kundInfo");
                Console.WriteLine("-------------------------------");
                ShopLayout.CustomerUpdateLayout();

                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    case '1': CustomerUpdate.AnvändareNamn(); break;
                    case '2': CustomerUpdate.Lösenord(); break;
                    case '3': CustomerUpdate.VisaKundInfo(); break;
                }
            }
        }
        private static void BästSäljandeProdukter()
        {
            Console.Clear();
            using (var db = new MyDbContext())
            using (var conn = new SqlConnection(db.GetConnectionString()))
            {
                string sql = @"
        SELECT 
            p.Id,
            LEFT(p.Namn, 10) AS Namn,
            p.Pris,
            SUM(ord.Antal) AS TotaltSold
        FROM 
            OrderRader ord
        JOIN 
            Produkter p ON ord.ProduktId = p.Id
        GROUP BY 
            p.Id, p.Namn, p.Pris
        ORDER BY 
            TotaltSold DESC;
    ";

                var resultat = conn.Query(sql).ToList();

                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"| {"id",-5} | {"Namn",-15} | {"Sålda",-10} | ");
                Console.WriteLine("----------------------------------------");

                foreach (var r in resultat)
                {
                    Console.WriteLine($"| {r.Id, -5} | {r.Namn,-15} | {r.TotaltSold, -10} |");
                }
                Console.WriteLine("----------------------------------------");
            }

        }


    }
}
