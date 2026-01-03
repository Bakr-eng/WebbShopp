using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbShop2.Migrations;

namespace WebbShop2.Models
{
    internal class Storlek
    {
        public int Id { get; set; }
        public string? Namn { get; set; }

        // Navigationsproperty
        public virtual ICollection<ProduktStorlek> ProduktStorlekar { get; set; } = new List<ProduktStorlek>();
    }
}
