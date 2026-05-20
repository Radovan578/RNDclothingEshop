using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RND_clothing_e_shop
{
    public class AuthServis
    {
        // uloženie správa pre používateľa 
        public string Message { get; set; }

        // registrácia nového používateľa
        public bool Register(string username, string email, string password, string provePassword)
        {
            // kontrola či niečo nie je prázdne
            if (username == "" || email == "" || password == "" || provePassword == "")
            {
                Message = "Vyplň všetky polia.";     // povie userovi že niečo chýba
                return false;     // stop registrácie
            }

            // kontrola či sa heslá zhodujú
            if (password != provePassword)
            {
                Message = "Heslá sa nezhodujú.";   // sprava ze je chybný login
                return false;    // stop
            }

            // kontrola mena (iba písmená)
            bool rightUsername = true;
            foreach (char symbol in username)
            {
                // ak nájde číslo alebo znak tak je zle
                if (!char.IsLetter(symbol) && symbol != ' ')
                {
                    rightUsername = false;  // meno je zlé
                    break;   // ukončí kontrolu
                }
            }

            // ak meno nie je v poriadku
            if (!rightUsername || username.Length == 0)
            {
                Message = "Meno nesmie obsahovať čísla ani špeciálne znaky.";    //sprava
                return false;   // stop registrácie
            }

            // načítanie všetkých userov zo súboru
            List<Uzivatel> users = JsonServis.LoadUsers();

            // hľadanie či už existuje rovnaký user alebo email
            Uzivatel existujuciUser = null;
            foreach (var u in users)
            {
                // Kontrola, či už niekto nemá rovnaké meno ALEBO rovnaký email
                if (u.Username == username || u.Email == email)
                {
                    existujuciUser = u;   // Našli sme zhodu, uložíme si používateľa
                    break;                // Zastavíme cyklus, netreba ďalej hľadať
                }
            }

            // ak sa niečo našlo tak už existuje
            if (existujuciUser != null)
            {
                Message = "Používateľ alebo email už existuje.";    //sprava
                return false;   // stop
            }

            // vytvorenie nového používateľa
            Uzivatel newUser = new Uzivatel();
            newUser.Username = username;     // uloží meno
            newUser.Email = email;           // uloží email
            newUser.Password = password;     // uloží heslo

            // pridanie do zoznamu userov
            users.Add(newUser);

            // uloženie do JSON súboru
            JsonServis.SaveUsers(users);

            Message = "Registrácia bola úspešná.";  //sprava
            return true;    // všetko OK

        }

        //Login pouzivatela
        public bool Login(string nameOrMail, string password)
        {
            // kontrola či niečo nie je prázdne
            if (nameOrMail == "" || password == "")
            {
                Message = "Zadaj meno alebo email a heslo.";
                return false;   // stop login
            }

            // načítanie userov zo súboru
            List<Uzivatel> users = JsonServis.LoadUsers();

            // hľadanie usera podľa mena alebo emailu
            Uzivatel user = null;
            foreach (var u in users)
            {
                // Skontrolujeme, či sa zadaný text zhoduje s používateľským menom ALEBO s emailom
                if (u.Username == nameOrMail || u.Email == nameOrMail)
                {
                    user = u;   // Používateľ nájdený, uložíme si ho
                    break;      // Ukončíme cyklus, máme čo sme chceli
                }
            }

            // ak user existuje a heslo sedí
            if (user != null && user.Password == password)
            {
                Message = "Prihlásenie bolo úspešné.";
                return true;  // login OK
            }
            else
            {
                Message = "Chybné meno, email alebo heslo.";
                return false;    // login zlyhal
            }
        }

    }
}
