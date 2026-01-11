using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class Kund
    {
        public int Id { get; set; }
        public string? Anvandarnamn { get; set; }
        public string Losenord { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public int? AdressId { get; set; }

        // Navigationsproperty
        public virtual Adress? Adress { get; set; }

    }
}
