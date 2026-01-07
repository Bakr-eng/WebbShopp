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
        public DbSet<Leverantor> Leverantorer { get; set; }
        public DbSet<ProduktStorlek> ProduktStorlekar { get; set; }
        public DbSet<Varukorg> Varukorgar { get; set; }

        public DbSet<Kund> Kunder { get; set; }
        public DbSet<Adress> Adresser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress02;Database=WebbShop2;Trusted_Connection=True; TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base .OnModelCreating(modelBuilder); 

            modelBuilder.Entity<ProduktStorlek>() 
            .HasKey(ps => new { ps.ProduktId, ps.StorlekId });

            modelBuilder.Entity<ProduktStorlek>()
                .HasOne(ps => ps.Produkt)
                .WithMany(p => p.ProduktStorlekar)
                .HasForeignKey(ps => ps.ProduktId);

            modelBuilder.Entity<ProduktStorlek>() 
                .HasOne(ps => ps.Storlek)
                .WithMany(s => s.ProduktStorlekar)
                .HasForeignKey(ps => ps.StorlekId);
        }

    }
}
