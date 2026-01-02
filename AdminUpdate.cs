using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;

namespace WebbShop2
{
    internal class AdminUpdate
    {
        public static void Pris()
        {
            
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Admin.VisaProdukter();
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Ändra Pris"); Console.ResetColor();
                Console.WriteLine("--------------");
                Console.Write("Ange produktens Id som du vill uppdatera priset för: ");
                int produktId = int.Parse(Console.ReadLine());
                Console.Write("Ange det nya priset: ");
                decimal nyttPris = decimal.Parse(Console.ReadLine());

                var produkt = db.Produkter.Where(p => p.Id == produktId).FirstOrDefault();
                if (produkt != null)
                {
                    produkt.Pris = nyttPris;
                    try
                    {
                        db.SaveChanges();
                        Console.Write($"Priset för produkten '{produkt.Namn}' ");
                        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($" har uppdaterats till {nyttPris} kr."); Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Något gick fel: " + ex.InnerException);
                    }
                }
                else
                {
                    Console.WriteLine("Produkten med angivet Id hittades inte.");
                }
                
            }
        }
        public static void Namn()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Admin.VisaProdukter();
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Ändra Namn"); Console.ResetColor();
                Console.WriteLine("--------------");
                Console.Write("Ange produktens Id som du vill uppdatera namnet för: ");
                int produktId = int.Parse(Console.ReadLine());
                Console.Write("Ange det nya namnet: ");
                string nyttNamn = Console.ReadLine();

                var produkt = db.Produkter.Where(p => p.Id == produktId).FirstOrDefault();
                if (produkt != null)
                {
                    produkt.Namn = nyttNamn;
                    try
                    {
                        db.SaveChanges();
                        Console.Write($"Namnet för produkten med Id {produktId} ");
                        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($" har uppdaterats till '{nyttNamn}'."); Console.ResetColor();
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Något gick fel: " + ex.InnerException);
                    }
                }
                else
                {
                    Console.WriteLine("Produkten med angivet Id hittades inte.");
                }

            }

        }
        public static void Infotext()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Admin.VisaProdukter();
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Ändra Infotext"); Console.ResetColor();
                Console.WriteLine("--------------");
                Console.Write("Ange produktens Id som du vill uppdatera infotexten för: ");
                int produktId = int.Parse(Console.ReadLine());
                Console.Clear();
                var produkt = db.Produkter.Where(p => p.Id == produktId).ToList(); 
                foreach (var p in produkt)
                {
                    Console.WriteLine($"{p.Namn}");
                    Console.WriteLine("------------------");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;  Console.Write("Nuvarande infotext: "); Console.ResetColor();
                    Console.Write(p.Beskrivning);
                }
                Console.WriteLine();
                Console.Write("Ange den nya infotexten: ");
                string nyInfotext = Console.ReadLine();

                var produktUppdatera = db.Produkter.Where(p => p.Id == produktId).FirstOrDefault();

                if (produktUppdatera != null)
                {
                    produktUppdatera.Beskrivning = nyInfotext;
                    try
                    {
                        db.SaveChanges();
                        Console.Write($"Infotexten för produkten med Id {produktId} ");
                        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(" har uppdaterats."); Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Något gick fel: " + ex.InnerException);
                    }
                }
                else
                {
                    Console.WriteLine("Produkten med angivet Id hittades inte.");
                }
            }

        }
        public static void Leverantör()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Admin.VisaProdukter();
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Uppdatera Leverantör"); Console.ResetColor();
                Console.WriteLine("--------------");
                Console.Write("Ange produktens Id som du vill uppdatera leverantören för: ");
                int produktId = int.Parse(Console.ReadLine());
                Console.WriteLine("Ange den nya leverantörens Id: ");
                Console.WriteLine("----------------");
                Console.WriteLine(
                   "1. Adidas\n" +
                   "2. Nike\n" +
                   "3. Zara\n" +
                   "4. NordicWear AB\n" +
                   "5. ScandiJeans"
                   );
                int nyLeverantorId = int.Parse(Console.ReadLine());

                var produkt = db.Produkter.Where(p => p.Id == produktId).FirstOrDefault();

                if (produkt != null)
                {
                    produkt.LeverantorId = nyLeverantorId;
                    try
                    {
                        db.SaveChanges();
                        Console.Write($"Leverantören för produkten '{produkt.Namn}' ");
                        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(" har uppdaterats."); Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Något gick fel: " + ex.InnerException);
                    }
                }
                else
                {
                    Console.WriteLine("Produkten med angivet Id hittades inte.");
                }

            }
        }
    }
}
