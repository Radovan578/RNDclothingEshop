using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RND_clothing_e_shop
{
    // Trieda ktorá zabezpečuje ukladanie a načítanie dát zo JSON súborov
    public class JsonServis
    {
        // Cesta/názov súboru pre používateľov
        private static string usersSubor = "users.json";

        // Cesta/názov súboru pre košík
        private static string kosikSubor = "kosik.json";

        // Metóda na uloženie používateľov do JSON súboru
        public static void SaveUsers(List<Uzivatel> users)
        {
            // Premení zoznam používateľov na JSON text
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                // JSON nebude v jednom riadku ale prehľadne naformátovaný
                WriteIndented = true
            });

            // Zapíše JSON text do súboru users.json
            // Ak súbor neexistuje -> vytvorí ho
            // Ak existuje -> prepíše ho
            File.WriteAllText(usersSubor, json);
        }

        // Metóda na načítanie používateľov zo súboru
        public static List<Uzivatel> LoadUsers()
        {
            // Skontroluje či súbor existuje
            if (!File.Exists(usersSubor))
            {
                // Ak neexistuje, vráti prázdny zoznam
                return new List<Uzivatel>();
            }

            // Načíta celý obsah súboru do stringu
            string json = File.ReadAllText(usersSubor);

            // Premení JSON text na zoznam používateľov
            List<Uzivatel>? users = JsonSerializer.Deserialize<List<Uzivatel>>(json);

            // Skontroluje či výsledok nie je null
            if (users == null)
            {
                // Ak je null, vytvorí prázdny zoznam
                users = new List<Uzivatel>();
            }

            // Vráti zoznam používateľov
            return users;
        }
        // Metóda na vymazanie súboru používateľov
        public static void DeleteUsers()
        {
            // Skontroluje či súbor existuje
            if (File.Exists(usersSubor))
            {
                // Vymaže súbor users.json
                File.Delete(usersSubor);
            }
        }

        // Metóda na uloženie košíka do JSON súboru
        public static void SaveKosik(List<Kosik> kosik)
        {
            // Premení zoznam košíka na JSON text
            string json = JsonSerializer.Serialize(kosik, new JsonSerializerOptions
            {
                // JSON bude odsadený a čitateľný
                WriteIndented = true
            });

            // Zapíše JSON do súboru kosik.json
            File.WriteAllText(kosikSubor, json);
        }

        // Metóda na načítanie košíka zo súboru
        public static List<Kosik> LoadKosik()
        {
            // Skontroluje existenciu súboru
            if (!File.Exists(kosikSubor))
            {
                // Ak súbor neexistuje -> vráti prázdny zoznam
                return new List<Kosik>();
            }

            // Načíta obsah súboru
            string json = File.ReadAllText(kosikSubor);

            // Premení JSON text na zoznam košíkov
            var kosik = JsonSerializer.Deserialize<List<Kosik>>(json);

            // Skontroluje či deserialize nevrátil null
            if (kosik == null)
            {
                // Ak je výsledok null, vytvorí prázdny zoznam
                kosik = new List<Kosik>();
            }

            // Vráti zoznam košíkov
            return kosik;
        }

        // Metóda na vymazanie súboru košíka
        public static void DeleteKosik()
        {
            // Skontroluje existenciu súboru
            if (File.Exists(kosikSubor))
            {
                // Vymaže súbor kosik.json
                File.Delete(kosikSubor);
            }
        }
    }
}