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
                            totalPris = p.Pris * v.Antal,
                            produktNamn = p.Namn,
                            storlekNamn = s.Namn,
                            v.KundId,
                            p.Pris,
                            v.Antal,
                            ps.EnheterIlager,
                        }).ToList();


                    var totalVarukorgPris = varukorgen.Sum(s => s.totalPris);
                    int index = 1;
                    foreach (var v in varukorgen)
                    {
                        Console.WriteLine($"{index}| produkten: {v.produktNamn}| Antal: {v.Antal} | storleken är:{v.storlekNamn}  |\t pris:  {v.Pris}\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\ntotalt pris:  " + v.totalPris);
                        Console.ResetColor();
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        index++;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Totalt pris: " + totalVarukorgPris);
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




                    var key = Console.ReadKey();
                    switch (char.ToLower(key.KeyChar))
                    {
                        case '1':
                            Console.Write("\nVilken produkt ska du ändra antalet? ");
                            string input = Console.ReadLine();

                            if (int.TryParse(input, out int result) && result > 0 && result <= varukorgen.Count)
                            {
                                var valdProdukt = varukorgen[result - 1];
                                Console.WriteLine("välj antal: ");
                                Console.WriteLine(valdProdukt.EnheterIlager + " som finns i lager.");
                                int antalProdukt = int.Parse(Console.ReadLine());
                                
                            }
                            break;
                           
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
