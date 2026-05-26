using System;
using System.Collections.Generic;

namespace RND_clothing_e_shop
{
    public class Objednavka
    {
        public string IdObjednavky { get; set; } = ""; // unikatny identifikator objednavky, napr. "OBJ123456"
        public string Uzivatel { get; set; } = ""; // meno prihlaseneho uzivatela, ktory zadal objednavku, napr. "janedoe"
        public DateTime Datum { get; set; }  // datum a cas zadania objednavky
        public List<Produkt> Produkty { get; set; } = new List<Produkt>();  // zoznam produktov v objednavke
        public decimal CelkovaCena { get; set; }  // celkova cena objednavky, vypocitana ako sucet (cena produktu * mnozstvo) pre vsetky produkty
        public string StavZasielky { get; set; } = "Vašu objednávku momentálne spravujeme";  // stav zasielky, napr. "Vašu objednávku momentálne spravujeme", "Vaša objednávka je na ceste"
    }
}