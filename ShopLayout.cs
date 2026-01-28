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
        public static void WebbShopTitle()
        {


            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            List<string> title = new List<string> { " ", " Välkommen till WebbShop   ", " " };
            var windowTitle = new Window("", 35, 0, title);
            windowTitle.Draw();




        }
        public static void WelcomeLayout()
        {
            List<string> Welcome = new List<string> { "🌸 🌼 🌻 🌺 🌹 🌷 🌸 🌼 🌻 🌺 🌹 🌷 🌸","", "             Välkommen", ""
                , "    Upptack stilen som passar dig    ","", "🌸 🌼 🌻 🌺 🌹 🌷 🌸 🌼 🌻 🌺 🌹 🌷 🌸" };
            var windowWelcome = new Window("", 57, 18, Welcome);
            windowWelcome.Draw();
        }
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
            List<string> AdminOption = new List<string> { "1. Visa produkter", "2. Lägga till produkter", "3. Ta bort produkter",
                "4. Uppdatera produkter", "5. Uppdatera kundInfo", "6. Bäst säljande produkter" };
            var windowAdminOption = new Window("Admin Panel",0,8, AdminOption);
            windowAdminOption.Draw();
        }
        public static void AdminUpdateLayout()
        {
            List<string> AdminUpdateOption = new List<string> {"1. Namn", "2. Pris", "3. Infotext", "4. Leverantör", "5. Erbjudande produkter" };
            var windowAdminUpdateOption = new Window("Uppdatera Produkt", 0, 3, AdminUpdateOption);
            windowAdminUpdateOption.Draw();

        }
        public static void CustomerUpdateLayout()
        {
            List<string> CustomerUpdateOption = new List<string> { "1. Användaren namn", "2. lösenord", "3. Vissa kunden information"};
            var windowCustomerOption = new Window("Uppdatera KundenInfo", 0 ,4, CustomerUpdateOption);
            windowCustomerOption.Draw();
        }
        public static void BuyLayout()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            List<string> BuyOption = new List<string> { "0.🛒 Köp nu" };
            var windowBuyOption = new Window("", 99, 0, BuyOption);
            windowBuyOption.Draw();
            Console.ResetColor();
        }
        public static void CustomersLayout()
        {
            List<string> CustomerOption = new List<string> { "1. Logga in", "2. Registrera" };
            var windowCustomerOption = new Window("",0, 3, CustomerOption);
            windowCustomerOption.Draw();

            List<string> Option = new List<string> { "Q. Tillbaka" };
            var windowQOption = new Window("", 100, 0, Option);
            windowQOption.Draw();
        }
        public static void ShoppingCartLayout()
        {
            List<string> Name = new List<string> {"     Varukorg       "};
            var windowName = new Window("", 45, 0, Name);
            windowName.Draw();


            List<string> UnitInStock = new List<string> { "1. Ändra Antal" };
            var windowUnitInStock = new Window("", 99, 3, UnitInStock);
            windowUnitInStock.Draw();


            List<string> DeleteOption = new List<string> { "2. Ta bort" };
            var windowDelete = new Window("", 99, 6, DeleteOption);
            windowDelete.Draw();



            List<string> ShowProducts = new List<string> { "3. mer Info" };
            var windowShow = new Window("", 99, 9, ShowProducts);
            windowShow.Draw();

          


        }


        
    }
}
