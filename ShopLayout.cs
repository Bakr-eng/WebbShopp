using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDemo;

namespace WebbShop2
{
    internal class ShopLayout
    {
        public static void DrawLayout()
        {
            List<string> sökning = new List<string> {"0 för att söka", "____________________" };
            var windowSearch = new Window("Sökning", 50, 0, sökning);
            windowSearch.Draw();

            List<string> AdminKnappt = new List<string> { "Tryck på X för att gå till Adminpanelen" };
            var windowAdmin = new Window("Admin", 76, 0, AdminKnappt);
            windowAdmin.Draw();

            List<string> erbjudandeA = new List<string> { "Fin tröja i ull", "pris: 199 kr ", "A för att lägga till varukorg", "" };
            var windowCartA = new Window("erbjudande 1", 1, 7, erbjudandeA);
            windowCartA.Draw();

            List<string> erbjudandeB = new List<string> { "Skjorta", "Snygg skjorta för alla tillfällen ", "Pris: 299 kr ", "B för att lägga till varukorg" };
            var windowCartB = new Window("erbjudande 2",35, 7, erbjudandeB);
            windowCartB.Draw();

            List<string> erbjudandeC = new List<string> { "Byxor", "Lagom långa byxor ", "Pris: 399 kr ", "C för att lägga till varukorg" };
            var windowCartC = new Window("erbjudande 3", 75, 7, erbjudandeC);
            windowCartC.Draw();

            List<string> Kategorier = new List<string> { "1. Tröjor", "2. Byxor", "3. Jackor" };
            var windowCategories = new Window("Kategorier", 1, 15, Kategorier);
            windowCategories.Draw();
        }
        public static void AdminLayout()
        {
            List<string> AdminOption = new List<string> { "1. Visa produkter", "2. Lägga till produkter", "3. Ta bort produkter", "4. Uppdatera produkter" };
            var windowAdminOption = new Window("Admin Panel",0,3, AdminOption);
            windowAdminOption.Draw();
        }
        public static void AdminUpdateLayout()
        {
            List<string> AdminUpdateOption = new List<string> {"1. Uppdatera Namn", "2. Uppdatera Pris" };
            var winedowAdminUpdateOption = new Window("Uppdatera Produkt", 0, 3, AdminUpdateOption);
            winedowAdminUpdateOption.Draw();

        }
    }
}
