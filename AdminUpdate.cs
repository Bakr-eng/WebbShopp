using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2
{
    internal class AdminUpdate
    {
        public static void Pris()
        {
            
            using (var db = new Models.MyDbContext())
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
            using (var db = new Models.MyDbContext())
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
    }
}
