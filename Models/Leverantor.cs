using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class Leverantor
    {
        public int Id { get; set; }
        public string? Namn { get; set; }
        public string? KontaktInfo { get; set; }
        public string? telefonNummer { get; set; }

        // Navigationsproperty
        public virtual ICollection<Produkt> Produkter { get; set; } = new List<Produkt>();
    }
}
