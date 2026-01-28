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
                textRad.Add("" + rad.Storlek.Namn);
                textRad.Add("");
            }
           textRad.Add("---------------------------------");
            textRad.Add($"Totalt pris: {totalPris:0.00}");

            var windowShoppingCart = new Window ("", 0,0, textRad);
            windowShoppingCart.Draw();



            Console.Write("Tryck på "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("'Enter'"); Console.ResetColor();
            Console.WriteLine(" för att fortsätta till betalnigen!");
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
            decimal frakt = 0m; // m är för decimal literal
            Console.Clear();


            while (true)
            {
                Console.WriteLine("Välj fraktalternativ:");
                Console.WriteLine("-----------------------\n");
                Console.WriteLine("1. Standardfrakt (49 kr) – Leverans 2–5 dagar");
                Console.WriteLine("2. Expressfrakt (99 kr) – Leverans 1–2 dagar");
                Console.WriteLine("-----------------------\n");
                Console.Write("Välj (1 eller 2): ");
                 frakt = int.Parse(Console.ReadLine());
                if (frakt == 1 )
                {
                    frakt = 49;
                    break;
                }
                else if (frakt == 2)
                {
                    frakt = 99;
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
            decimal totalPris = kundVarukorgen.Sum(s => s.Produkt.Pris * s.Antal);
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
            Console.WriteLine($"fraktkostnad: {frakt}");
            Console.WriteLine("---------------------");
            Console.Write($"summa att betala: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( totalPris + frakt);
            Console.ResetColor();
            Console.WriteLine("---------------------\n\n");


            BetalningsSätt();


            decimal totalAttBetala = totalPris + frakt;

            var nyOrder = new Order
            {
                KundId = KundSida.InloggadKundId,
                Datom = DateTime.Now,
                TotalPris = totalAttBetala,
                FraktId = frakt == 49 ? 1 : 2
            };

            Db.Ordrar.Add(nyOrder);
            Db.SaveChanges();


            var orderRader = new List<OrderRad>();
            foreach (var rad in kundVarukorgen)
            {
                var nyOrderRad = new OrderRad
                {
                    OrderId = nyOrder.Id,
                    ProduktId = rad.ProduktId,
                    StorlekId = rad.StorlekId,
                    Antal = rad.Antal,
                    PrisVidKop = rad.Produkt.Pris
                };
                orderRader.Add(nyOrderRad);
                Db.OrderRader.Add(nyOrderRad);
              
            }
            Db.SaveChanges();

            var varukorgRemove = Db .Varukorgar.Where(v => v.KundId == KundSida.InloggadKundId);

            Db.Varukorgar.RemoveRange(varukorgRemove);
            Db.SaveChanges();

            Console.Clear();
            Console.SetCursorPosition(30, 10);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tack för din beställning!");
            Console.ResetColor();
            //Console.ReadKey();
        }
        private static void BetalningsSätt()
        {
            int top = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(0, top);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Betalningssätt!");
                Console.ResetColor();
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. betala med kort");
                Console.WriteLine("2. betala med swish");
                Console.WriteLine("Välj (1 eller 2): ");
                Console.WriteLine("Eller 'q' för avbryt");

                var key = Console.ReadKey(true);
                switch (char.ToLower(key.KeyChar))
                {
                    case '1':
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("betala med kort");
                        Console.ResetColor();
                        Console.WriteLine("----------------------");
                        Console.Write("kortNummer: ");
                        string kortNummer = Console.ReadLine();
                        Console.Write("sista giltighetsdag: ");
                        string giltighetsdag = Console.ReadLine();
                        Console.Write("CVC: ");
                        string cvc = Console.ReadLine();
                        return;

                    case '2':
                        Console.WriteLine("----------------------");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("betala med swish");
                        Console.ResetColor();
                        Console.Write("Swish Nummer: ");
                        string swishNummer = Console.ReadLine();
                        return;

                    case 'q':
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("------------------------------------\n");
                        Console.WriteLine("Tryck på Enter för att återgå till start sida");
                        Console.WriteLine("------------------------------------ ");
                        Console.ResetColor();
                        Console.ReadKey();

                        KundSida.Inloggad();
                        break;

                    default:
                     //  Console.Clear();
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        Console.ReadKey(true);
                        Console.SetCursorPosition(0, Console.CursorTop -1); 
                        Console.WriteLine("                             "); 
                        break;
                }



            }
        }
    }
}
