using System;
using System.Collections.Generic;

namespace RND_clothing_e_shop
{
    public class Objednavka
    {
        public string IdObjednavky { get; set; } = "";
        public string Uzivatel { get; set; } = "";
        public DateTime Datum { get; set; }
        public List<Produkt> Produkty { get; set; } = new List<Produkt>();
        public decimal CelkovaCena { get; set; }
        public string StavZasielky { get; set; } = "Vašu objednávku momentálne spravujeme";
    }
}