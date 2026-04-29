using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RND_clothing_e_shop
{
    public class JsonServis
    {
        private static string usersSubor = "users.json";
        private static string kosikSubor = "kosik.json";


        public static void SaveUsers(List<Uzivatel> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(usersSubor, json);
        }

        public static List<Uzivatel> LoadUsers()
        {
            if (!File.Exists(usersSubor))
            {
                return new List<Uzivatel>();
            }

            string json = File.ReadAllText(usersSubor);

            return JsonSerializer.Deserialize<List<Uzivatel>>(json) ?? new List<Uzivatel>();
        }

        public static void DeleteUsers()
        {
            if (File.Exists(usersSubor))
            {
                File.Delete(usersSubor);
            }
        }


        public static void SaveKosik(List<Kosik> kosik)
        {
            string json = JsonSerializer.Serialize(kosik, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(kosikSubor, json);
        }

        public static List<Kosik> LoadKosik()
        {
            if (!File.Exists(kosikSubor))
            {
                return new List<Kosik>();
            }

            string json = File.ReadAllText(kosikSubor);

            return JsonSerializer.Deserialize<List<Kosik>>(json) ?? new List<Kosik>();
        }

        public static void DeleteKosik()
        {
            if (File.Exists(kosikSubor))
            {
                File.Delete(kosikSubor);
            }
        }
    }
}