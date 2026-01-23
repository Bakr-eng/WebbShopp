using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class OrderRad
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProduktId { get; set; }
        public int StorlekId { get; set; }
        public int Antal { get; set; }
        public decimal PrisVidKop { get; set; }

        // Navigationsproperty
        public virtual Order Order { get; set; }
        public virtual Produkt Produkt { get; set; }
        public virtual Storlek Storlek { get; set; }
    }
}
