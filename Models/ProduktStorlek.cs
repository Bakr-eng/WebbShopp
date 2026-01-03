using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class ProduktStorlek
    {
        public int? ProduktId { get; set; }
        public virtual Produkt Produkt { get; set; }

        public int? StorlekId { get; set; }
        public virtual Storlek Storlek { get; set; }

        public int? EnheterIlager { get; set; }
    }
}
