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
        public static void Show(List<Varukorg> kundVarukorgen, MyDbContext db)
        {

            Console.Clear();
            Console.WriteLine("\x1b[3J");

            List<string> textRad = new List<string>();
            int radNummer = 1;
            var totalPris = kundVarukorgen.Sum(s => s.Produkt.Pris * s.Antal);
            foreach (var rad in kundVarukorgen)
            {
                textRad.Add(rad.Produkt.Namn + "  "+ rad.Produkt.Pris + "kr");
                textRad.Add( "Antal: " + rad.Antal + "");
                textRad.Add("");
                textRad.Add("");
            }
           textRad.Add("---------------------------------");
            textRad.Add($"Totalt pris: {totalPris:0.00}");

            var windowShoppingCart = new Window ("", 0,0, textRad);
            windowShoppingCart.Draw();



            Console.WriteLine("Tryck på 'Enter' för att fortsätta till betalning!");
            Console.ReadKey();

            HämtaBetalningsInfo(kundVarukorgen, db);
        }
        public static void HämtaBetalningsInfo(List<Varukorg> kundVarukorgen, MyDbContext Db)
        {

            Console.Clear();
            Console.Write("Mejladress: ");
            string email = Console.ReadLine();
            Console.WriteLine("-----------------------------------");
            Console.Write("Telefonnummer: ");

            string telefon = Console.ReadLine();
            Console.WriteLine();
            Console.Clear();

            Console.WriteLine();
            Console.Write("Gatuadress: ");
            string gatuAdress = Console.ReadLine();
            Console.WriteLine("-----------------------------------");

            Console.Write("Stad: ");
            string stad = Console.ReadLine();
            Console.WriteLine("-----------------------------------");

            Console.Write("Postnummer: ");
            int postnummer = int.Parse(Console.ReadLine());
            Console.WriteLine("-----------------------------------");
            int fraktPris = 0;
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Välj fraktalternativ:");
                Console.WriteLine("-----------------------\n");
                Console.WriteLine("1. Standardfrakt (49 kr) – Leverans 2–5 dagar");
                Console.WriteLine("2. Expressfrakt (99 kr) – Leverans 1–2 dagar");
                Console.WriteLine("-----------------------\n");
                Console.Write("Välj (1 eller 2): ");
                 fraktPris = int.Parse(Console.ReadLine());
                if (fraktPris == 1 )
                {
                    fraktPris = 49;
                    break;
                }
                else if (fraktPris == 2)
                {
                    fraktPris = 99;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltigt val, försök igen.");
                }

            }
            Console.Clear();

            List<string> textRader = new List<string>();
            int radNummer = 1;
            var totalPris = kundVarukorgen.Sum(s => s.Produkt.Pris * s.Antal);
            foreach (var rad in kundVarukorgen)
            {
                textRader.Add(rad.Produkt.Namn + "  " + rad.Produkt.Pris + "kr");
                textRader.Add("Antal: " + rad.Antal + "");
                textRader.Add("");
                textRader.Add("");
            }

            var windowShoppingCart = new Window("", 52, 0, textRader);
            windowShoppingCart.Draw();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine();
            Console.WriteLine($"email: {email}\ntelefon: {telefon}");
            Console.WriteLine($"gatuAdress: {gatuAdress}\npostnummer: { postnummer}\nstad: {stad}");
            Console.WriteLine();
            Console.WriteLine($"totalPris: {totalPris}");
            Console.WriteLine($"fraktkostnad: {fraktPris}");
            Console.WriteLine("---------------------");
            Console.WriteLine($"summa att betala: {totalPris + fraktPris}");
            Console.WriteLine("---------------------");
            Console.WriteLine("Till beetalning tryck på 'Enter'...");
            Console.ReadKey();





        }
    }
}
