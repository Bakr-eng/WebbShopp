using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Migrations;

namespace WebbShop2.Models
{
    internal class Produkt
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public decimal? Pris { get; set; }
        public int? KategoriId { get; set; }
        public string? Beskrivning { get; set; }
        public int? LeverantorId { get; set; }


        // Navigationsproperty
        public virtual Kategori? Kategori { get; set; }
        public virtual Leverantor? Leverantor { get; set; }

        public virtual ICollection<ProduktStorlek> ProduktStorlekar { get; set; } = new List<ProduktStorlek>();
    }
}
