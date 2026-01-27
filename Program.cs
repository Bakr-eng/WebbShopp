using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebbShop2.Models;
using WindowDemo;

namespace WebbShop2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //using (var db = new MyDbContext())
            //{
            //    db.Database.ExecuteSqlRaw("select 1");
            //}
            while (true)
            {


                Console.Clear();
                Console.WriteLine("\x1b[3J");
               ShopLayout.WebbShopTitle();

                ShopLayout.LogInLayout();
                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    case 'x': await KundSida.Start(); break;
                    case 'y': Admin.Start(); break;
                }
                Console.ReadLine();
            }
        }
    }
}
