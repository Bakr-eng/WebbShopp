using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class Produkt
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public decimal? Pris { get; set; }
        public int? KategoriId { get; set; }
        public string? Beskrivning { get; set; }
        public int? EnheterILager { get; set; }
        

        // Navigationsproperty
        public virtual Kategori? Kategori { get; set; }

        public virtual ICollection<Storlek> Storlekar { get; set; } = new List<Storlek>();
    }
}
