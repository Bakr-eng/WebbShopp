using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;

namespace WebbShop2
{
    internal class CustomerUpdate
    {
        public static void VisaKundInfo()
        {
            using (var db = new MyDbContext())
            {
                var kunder = db.Kunder.ToList();
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine($"| {"ID",-5} | {"Användarnamn",-25} | {"Lösenord",-25} |");
                Console.WriteLine("-----------------------------------------------------------------");

                foreach (var kund in kunder)
                {
                    Console.WriteLine($"| {kund.Id, -5} | {kund.Anvandarnamn, -25} | {kund.Losenord, -25} |");
                }
                Console.WriteLine("-----------------------------------------------------------------");
            }
        }
        public static void AnvändareNamn()
        {

            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                VisaKundInfo();

                Console.WriteLine("Ange ID för den kund du vill ändra användarnamn för:");
                if (int.TryParse(Console.ReadLine(), out int kundId))
                {
                    var kund = db.Kunder.Find(kundId);
                    if (kund != null)
                    {
                        Console.WriteLine("Ange nytt användarnamn:");
                        string nyttAnvandarnamn = Console.ReadLine();
                        kund.Anvandarnamn = nyttAnvandarnamn;
                        db.SaveChanges();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Användarnamn uppdaterat.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Kund med angivet ID hittades inte.");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt ID format.");
                }

                db.SaveChanges();
            }
        }
        public static void Lösenord()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                VisaKundInfo();
                Console.WriteLine("Ange ID för den kund du vill ändra lösenordet för:");
                if (int.TryParse(Console.ReadLine(), out int kundId))
                {
                    var kund = db.Kunder.Find(kundId); 
                    if (kund != null)
                    {
                        Console.WriteLine("Ange nytt lösenord:");
                        string nyttLosenord = Console.ReadLine();
                        kund.Losenord = nyttLosenord;
                        db.SaveChanges();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Lösenord uppdaterat.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Kund med angivet ID hittades inte.");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt ID format.");
                }
                db.SaveChanges();
            }

        }
    }
}
