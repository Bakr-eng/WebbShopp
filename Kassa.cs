using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;

namespace WebbShop2
{
    internal class Kassa
    {
        public static void Show(List<Varukorg> kundVarukorgen, MyDbContext Db)
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            int radNummer = 1;
            var totalPris = kundVarukorgen.Sum(s => s.Produkt.Pris * s.Antal);
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
        }
    }
}
