using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;
using WindowDemo;

namespace WebbShop2
{
    internal class Kassa
    {
        public static void Show(List<Varukorg> kundVarukorgen, MyDbContext Db)
        {

            Console.Clear();
            Console.WriteLine("\x1b[3J");

            List<string> textRad = new List<string>();
            int radNummer = 1;
            var totalPris = kundVarukorgen.Sum(s => s.Produkt.Pris * s.Antal);
            foreach (var rad in kundVarukorgen)
            {
                //Console.WriteLine($"[{radNummer}]--------------------------");
              //  Console.WriteLine(rad.Produkt.Namn);
              //  Console.WriteLine(rad.Produkt.Pris + "kr\t  antal: " + rad.Antal + "\n\n");
                // radNummer++;
                textRad.Add(rad.Produkt.Namn + "  "+ rad.Produkt.Pris + "kr");
                textRad.Add( "Antal: " + rad.Antal + "");
                textRad.Add("");
                textRad.Add("");
            }
           // Console.ForegroundColor = ConsoleColor.DarkGray;
           // Console.WriteLine($"\n\n\tTotalt pris: {totalPris:0.00}");
           textRad.Add("---------------------------------");
            textRad.Add($"Totalt pris: {totalPris:0.00}");
           // Console.ResetColor();

            var windowShoppingCart = new Window ("", 0,0, textRad);
            windowShoppingCart.Draw();



            Console.WriteLine("Tryck Valfri knappt för att fortsätta till betalning!");
            Console.ReadKey();
        }
    }
}
