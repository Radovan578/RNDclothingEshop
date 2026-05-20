using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RND_clothing_e_shop
{
    public class Produkt
    {
        public string Name { get; set; }     // nazov produktu
        public string Category { get; set; }    // kategoria produktu
        public decimal Price { get; set; }      // cena produktu
        public int Quantity { get; set; }       // mnozstvo
        public string ImagePath { get; set; }       // cesta k obrazku
        public string Size { get; set; }        // velkost produktu
        public string Color { get; set; }       // farba produktu
        public string Description { get; set; }   // popis produktu

    }
}
