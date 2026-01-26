using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class BetalningsSätt
    {
        public int Id { get; set; }
        public string Namn { get; set; }

        // Navigationsproperty
        public virtual ICollection<Order> Ordrar { get; set; } = new List<Order>();
    }
}
