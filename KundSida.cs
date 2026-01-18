using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Migrations;
using WebbShop2.Models;

namespace WebbShop2
{
    internal class KundSida
    {
        public static int InloggadKundId { get; private set; } // Sparar kunden Id 

        public static void Start()
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("Välj ett alternativ:");
                Console.WriteLine("Tryck q för att gå till startsidan");
                Console.WriteLine("--------------------");
                ShopLayout.CustomersLayout();

                var key = Console.ReadKey();
                switch (char.ToLower(key.KeyChar))
                {
                    case '1': LoggaIn(); break;
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
                int phone = int.Parse(Console.ReadLine());



                Console.Clear();
                Console.WriteLine("Adress");
                Console.WriteLine("-------");

                Console.Write("Gatuadress: ");
                string gatuAdress = Console.ReadLine();

                Console.Write("Stad: ");
                string stad = Console.ReadLine();

                Console.Write("Postnummer: ");
                int postnummer = int.Parse(Console.ReadLine());

                Console.Write("Land: ");
                string land = Console.ReadLine();


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
                    Console.WriteLine("Ditt användarnamn är: " + nyKund.Id);
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
        private static void LoggaIn()
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
                    string losenord = Console.ReadLine();

                    if (losenord == "q")
                    {
                        break;
                    }

                    var logIn = db.Kunder
                        .FirstOrDefault(k => k.Anvandarnamn == namn && k.Losenord == losenord );
                    
                   
                    if (logIn != null)
                    {
                        InloggadKundId = logIn.Id; // sparar kunden Id
                        LoggInSomKund();

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
        
        private static void LoggInSomKund()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
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





    


