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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");

                using (var db = new MyDbContext())
                {
                    var varukorgen = (
                        from v in db.Varukorgar
                        join p in db.Produkter on v.ProduktId equals p.Id
                        join s in db.Storlekar on v.StorlekId equals s.Id
                        join ps in db.ProduktStorlekar 
                        on new { v.ProduktId, v.StorlekId } 
                        equals new { ps.ProduktId, ps.StorlekId }
                        where v.KundId == KundSida.InloggadKundId
                        select new
                        {
                            v.ProduktId,
                            v.StorlekId,
                            v.KundId,
                            v.Antal,
                            p.Beskrivning,
                            p.Pris,
                            ps.EnheterIlager,
                            produktNamn = p.Namn,
                            storlekNamn = s.Namn,
                            totalPris = p.Pris * v.Antal,
                        }).ToList();
                    ShopLayout.BuyLayout();
                    ShopLayout.ShoppingCartLayout();
                    Console.SetCursorPosition(0, 0);
                    var totalVarukorgPris = varukorgen.Sum(s => s.totalPris);
                    int index = 1;
                    foreach (var v in varukorgen)
                    {
                        Console.WriteLine($"[{index}]--------------------------");
                        Console.WriteLine(v.produktNamn);
                        Console.WriteLine(v.Pris + "kr\n\n");
                       index++;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Totalt hela produkters pris: " + totalVarukorgPris);
                    Console.ResetColor();

                    if (!varukorgen.Any())
                    {
                        Console.SetCursorPosition(40, 10);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Din varukorg är tom.");
                        Console.ResetColor();
                        Console.ReadKey();
                        return;
                    }

                    


                    var key = Console.ReadKey(true);
                    switch (char.ToLower(key.KeyChar))
                    {
                        case '1':
                            Console.Write("\nVilken produkt ska du ändra antalet? ");
                            string input = Console.ReadLine();

                            if (int.TryParse(input, out int result) && result > 0 && result <= varukorgen.Count)
                            {
                                var valdProdukt = varukorgen[result - 1];
                                Console.WriteLine(valdProdukt.EnheterIlager + " som finns i lager.");
                                Console.Write("välj antal: ");
                                int antalProdukt = int.Parse(Console.ReadLine());

                                if (antalProdukt < 1 || antalProdukt > valdProdukt.EnheterIlager)
                                {
                                    Console.WriteLine("ogiltigt antal.");
                                    Console.ReadKey();
                                    break;

                                }


                                var varukorgAntal = db.Varukorgar
                                                    .FirstOrDefault(
                                                    v => v.KundId == valdProdukt.KundId &&
                                                    v.ProduktId == valdProdukt.ProduktId &&
                                                    v.StorlekId == valdProdukt.StorlekId);
                                if (varukorgAntal != null)
                                {
                                    varukorgAntal.Antal = antalProdukt;
                                    db.SaveChanges();

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Antalet har ändrat!");
                                    Console.ResetColor();
                                }



                            }
                            break;
                        case '3':
                            Console.WriteLine("Välj en produkt för mer information (skriv numret): ");
                            string nummer = Console.ReadLine();
                            if (int.TryParse(nummer, out int choice) && choice > 0 && choice <= varukorgen.Count)
                            {
                                var valdProdukt = varukorgen[choice -1];
                                Console.Clear();
                                Console.WriteLine("=== Produktinformation ===");
                                Console.WriteLine($"Namn: {valdProdukt.produktNamn}");
                                Console.WriteLine($"Pris: {valdProdukt.Pris} kr");
                                Console.WriteLine($"Beskrivning: {valdProdukt.Beskrivning}");
                                Console.WriteLine($"Antal: {valdProdukt.Antal}");
                                Console.WriteLine($"Storleken: {valdProdukt.storlekNamn}");
                            }
                            break;




                        case 'q': return;
                           
                    }
                    Console.ReadLine();

                }
            }
        }
        
        public static void ÄndraAntal()
        {
            //using (var db = new MyDbContext())
            //{
            //    Console.Write("Vilken produkt ska du ändra antalet? ");
            //    string input = Console.ReadLine();

            //    if (int.TryParse(input, out int result) && result > 0 && result <= varukorges.Count)
            //    {
            //        var valdProdukt = varukorgen[result - 1];


            //    }
            //}
        }
    }

    //public class VarukorgItem
    //{
    //    public int ProduktId { get; set; }
    //    public string ProduktNamn { get; set; }
    //    public string StorlekNamn { get; set; }
    //    public int Antal { get; set; }
    //    public decimal Pris { get; set; }
    //    public decimal TotalPris { get; set; }
    //}
}
