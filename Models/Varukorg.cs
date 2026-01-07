using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop2.Models
{
    internal class Varukorg
    {
        public int Id { get; set; }
        public int KundId { get; set; }
        public int ProduktId { get; set; }
        public int StorlekId { get; set; }
        public int Antal { get; set; }
    }
}
