using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Produkt> Produkter { get; set; }
        public DbSet<Kategori> Kategorier { get; set; }

        public DbSet<Storlek> Storlekar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress02;Database=WebbShop2;Trusted_Connection=True; TrustServerCertificate=True;");


        }
    }
}
