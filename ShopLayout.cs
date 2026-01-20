using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Models;
using WindowDemo;

namespace WebbShop2
{
    internal class ShopLayout
    {
        public static void DrawLayout()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;  // för att kunna använda emojis

            List<string> varugorg = new List<string> { "V. 🛒" };
            var windowShoppingCart = new Window("Varukorg", 93, 1, varugorg);
            windowShoppingCart.Draw();



            List<string> sökning = new List<string> {"0 söka", "_____________________________" };
            var windowSearch = new Window("Sökning", 40, 0, sökning);
            windowSearch.Draw();


            using (var db = new MyDbContext())
            {
                var kategorier = db.Kategorier
                    .Select(k => k.Id.ToString() + " " + k.KategoriNamn).ToList();
                //List<string> Kategorier = new List<string> { "T. Tröjor", "B. Byxor", "J. Jackor" };
                var windowCategories = new Window("Kategorier", 40, 5, kategorier);
                windowCategories.Draw();
            }
        }
        public static void LogInLayout()
        {
            List<string> AdminKnappt = new List<string> { "Y. Logga in som Admin!" };
            var windowAdmin = new Window("", 93, 3, AdminKnappt);
            windowAdmin.Draw();

            List<string> Kundknappt = new List<string> { "X. Logga in/Registrera som Kund!"};
            var windowKund = new Window("", 83, 0, Kundknappt);
            windowKund.Draw();

        }
        public static void AdminLayout()
        {
            List<string> AdminOption = new List<string> { "1. Visa produkter", "2. Lägga till produkter", "3. Ta bort produkter", "4. Uppdatera produkter" };
            var windowAdminOption = new Window("Admin Panel",0,8, AdminOption);
            windowAdminOption.Draw();
        }
        public static void AdminUpdateLayout()
        {
            List<string> AdminUpdateOption = new List<string> {"1. Namn", "2. Pris", "3. Infotext", "4. Leverantör", "5. Erbjudande produkter" };
            var winedowAdminUpdateOption = new Window("Uppdatera Produkt", 0, 3, AdminUpdateOption);
            winedowAdminUpdateOption.Draw();

        }
        public static void BuyLayout()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            List<string> BuyOption = new List<string> { "0.🛒 Köp nu" };
            var windowBuyOption = new Window("", 100, 0, BuyOption);
            windowBuyOption.Draw();
            Console.ResetColor();
        }
        public static void CustomersLayout()
        {
            List<string> CustomerOption = new List<string> { "1. Logga in", "2. Registrera" };
            var windowCustomerOption = new Window("",0, 3, CustomerOption);
            windowCustomerOption.Draw();

            List<string> QOption = new List<string> { "Q. Tillbaka" };
            var windowQOption = new Window("", 100, 0, QOption);
            windowQOption.Draw();
        }


        
    }
}
