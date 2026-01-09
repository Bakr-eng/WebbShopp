using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class Adress
    {
        public int Id { get; set; }
        public string GatuAdress { get; set; }
        public string Stad { get; set; }
        public int Postnummer { get; set; }
        public string Land { get; set; }

        // Navigationsproperty
        public virtual ICollection<Kund> Kunder { get; set; } = new List<Kund>();
    }
}
