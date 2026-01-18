using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;

namespace WebbShop2
{
    internal class VarukorgSidan
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");

            using (var db = new MyDbContext())
            {
                var varukorgen = from v in db.Varukorgar
                                 join p in db.Produkter on v.ProduktId equals p.Id
                                 where v.KundId == KundSida.InloggadKundId 
                                 select new
                                 {
                                     v.KundId,
                                     p.Namn,
                                     p.Pris,
                                     v.Antal
                                 };

                foreach (var v in varukorgen)
                {
                    Console.WriteLine("kundenId: " + v.KundId + " \tNamn: " + v.Namn + "\t pris: " + v.Pris + "\tAntal: " + v.Antal);
                }

                if (!varukorgen.Any())
                {
                    Console.SetCursorPosition(40, 10);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Din varukorg är tom.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return; 
                }

                Console.ReadLine();
            }
        }
    }
}
