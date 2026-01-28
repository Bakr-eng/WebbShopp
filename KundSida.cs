using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Migrations;
using WebbShop2.Models;
using WindowDemo;

namespace WebbShop2
{
    internal class KundSida
    {
        public static int InloggadKundId { get; private set; } // Sparar kunden Id 

        public static async Task Start()
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("Välj ett alternativ:");
                Console.WriteLine("--------------------");
                ShopLayout.CustomersLayout();

                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    case '1': await LoggaIn(); break;
                    case '2': Registrera(); break;
                    case 'q': return;
                }
            }
        }
        private static void Registrera()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("Registrera ny kund");
                Console.WriteLine("------------------");

                Console.Write("Förnamn: ");
                string förnamn = Console.ReadLine();
                Console.Write("Efternamn: ");
                string efternamn = Console.ReadLine();

                Console.WriteLine("Ange ditt lösenord: ");
                string losenord = Console.ReadLine();

                Console.Write("Ange din email: ");
                string email = Console.ReadLine();

                Console.WriteLine("Ange ditt telefonnummer: ");
                string phone = Console.ReadLine();



                Console.Clear();
                Console.WriteLine("Adress");
                Console.WriteLine("-------");

                Console.Write("Gatuadress: ");
                string gatuAdress = Console.ReadLine().ToLower(); ;

                Console.Write("Stad: ");
                string stad = Console.ReadLine().ToLower();

                Console.Write("Postnummer: ");
                int postnummer = int.Parse(Console.ReadLine());

                Console.Write("Land: ");
                string land = Console.ReadLine().ToUpper();


                var befintligAdress = db.Adresser.FirstOrDefault(a =>
                    a.GatuAdress == gatuAdress &&
                    a.Stad == stad &&
                    a.Postnummer == postnummer &&
                    a.Land == land
                );

                Adress adressAttAnvända;
                if ( befintligAdress != null)
                {
                    adressAttAnvända = befintligAdress;
                }
                else
                {
                    adressAttAnvända = new Adress
                    {
                        GatuAdress = gatuAdress,
                        Stad = stad,
                        Postnummer = postnummer,
                        Land = land
                    };
                    db.Adresser.Add(adressAttAnvända);
                    
                }

                var nyKund = new Kund
                {
                    //Anvandarnamn = förnamn + efternamn,
                    Losenord = losenord,
                    Email = email,
                    Phone = phone,
                    Adress = adressAttAnvända
                };

                try
                {
                    db.Kunder.Add(nyKund);
                    db.SaveChanges();


                    nyKund.Anvandarnamn = förnamn + efternamn + nyKund.Id;

                    db.SaveChanges();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Registreringen lyckades!"); 
                    Console.ResetColor();
                    Console.WriteLine("Ditt användarnamn är: " + förnamn + efternamn + nyKund.Id);
                    Console.WriteLine("Ditt lösenord är: " + nyKund.Losenord);
                    Thread.Sleep(3000);
                    Console.WriteLine("\nTryck på valfri knapp för att fortsätta...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Något gick fel vid registreringen: " + ex.InnerException); 
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }
        private static async Task LoggaIn()
        {
            using (var db = new MyDbContext())
            {
         
                while (true)
                {
                    Console.Clear();
                    Console.Write("ange ditt användarnamn: ");
                    string namn = Console.ReadLine();

                    if ( namn == "q")
                    {
                        break;
                    }

                    Console.Write("Ange ditt lösenord: ");
                    string losenord = Admin.LäsDoltLösenord();

                    if (losenord == "q")
                    {
                        break;
                    }

                    var logIn = await db.Kunder
                        .FirstOrDefaultAsync(k => k.Anvandarnamn == namn && k.Losenord == losenord );
                    
                   
                    if (logIn != null)
                    {
                        InloggadKundId = logIn.Id; // sparar kunden Id
                        Inloggad();

                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nFel namn eller lösenord! ");
                        Console.ResetColor();
                        Console.WriteLine("Tryck på valfri knappt för att försöka igen: ");
                        Console.ReadKey();
                    }
                }
            }
        }
        public static void Inloggad()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                ShopLayout.WelcomeLayout();
                ShopLayout.DrawLayout();
                ErbjudandeProdukter.VisaErbjudandeProdukter();
                string input = Console.ReadLine().ToLower();
                ErbjudandeProdukter.SeErbjudandeinfo(input);
                

                


                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    case '0': ProduktVisning.Sökning(); break;
                    case '1': ProduktVisning.VisaKategoriProdukter(1); break;
                    case '2': ProduktVisning.VisaKategoriProdukter(2); break;
                    case '3': ProduktVisning.VisaKategoriProdukter(3); break;

                    case 'v': VarukorgSidan.Show(); break;
                    case 'q': return;
                }
            }



            

        }

        
    }
}





    


