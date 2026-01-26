using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public int KundId { get; set; }
        public DateTime Datom { get; set; }
        public decimal TotalPris { get; set; }

        // Navigationsproperty
        public virtual Kund Kund { get; set; }
        public virtual BetalningsSätt? BetalningsSätt { get; set; }
        public virtual Frakt? Frakt { get; set; }

        public virtual ICollection<OrderRad> OrderRader { get; set; } = new List<OrderRad>();
    }
}
