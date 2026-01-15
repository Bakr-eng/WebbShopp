using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class Kategori
    {
        public int Id { get; set; }
        public string KategoriNamn { get; set; }

        // Navigationsproperty
        public virtual ICollection<Produkt> Produkter { get; set; } = new List<Produkt>();
    }
}
