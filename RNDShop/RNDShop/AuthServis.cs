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
        public string Message { get; set; }

        public bool Register(string username, string email, string password, string provePassword)
        {
            if (username == "" || email == "" || password == "" || provePassword == "")
            {
                Message = "Vyplň všetky polia.";
                return false;
            }

            if (password != provePassword)
            {
                Message = "Heslá sa nezhodujú.";
                return false;
            }

            bool rightUsername = true;
            foreach (char symbol in username)
            {
                if (!char.IsLetter(symbol) && symbol != ' ')
                {
                    rightUsername = false;
                    break;
                }
            }

            if (!rightUsername || username.Length == 0)
            {
                Message = "Meno nesmie obsahovať čísla ani špeciálne znaky.";
                return false;
            }

            List<Uzivatel> users = JsonServis.LoadUsers();

            Uzivatel existujuciUser = users.FirstOrDefault(u => u.Username == username || u.Email == email);

            if (existujuciUser != null)
            {
                Message = "Používateľ alebo email už existuje.";
                return false;
            }

            Uzivatel newUser = new Uzivatel();
            newUser.Username = username;
            newUser.Email = email;
            newUser.Password = password;

            users.Add(newUser);
            JsonServis.SaveUsers(users);

            Message = "Registrácia bola úspešná.";
            return true;

        }
        public bool Login(string nameOrMail, string password)
        {
            if (nameOrMail == "" || password == "")
            {
                Message = "Zadaj meno alebo email a heslo.";
                return false;
            }

            List<Uzivatel> users = JsonServis.LoadUsers();

            Uzivatel user = users.FirstOrDefault(u => u.Username == nameOrMail || u.Email == nameOrMail);

            if (user != null && user.Password == password)
            {
                Message = "Prihlásenie bolo úspešné.";
                return true;
            }
            else
            {
                Message = "Chybné meno, email alebo heslo.";
                return false;
            }
        }

    }
}
