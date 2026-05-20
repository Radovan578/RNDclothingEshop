using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RND_clothing_e_shop
{
    public class Kosik
    {
        public string ProductName { get; set; }   // nazov produktu
        public decimal Price { get; set; }       // cena produktu
        public int Quantity { get; set; }        // mnozstvo
        public string Color { get; set; }        // farba produktu
        public string Size { get; set; }         // velkost produktu
    }
}
