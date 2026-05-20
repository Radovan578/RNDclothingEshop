using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Text.Json;

namespace RND_clothing_e_shop
{
    public partial class HistoriaWindow : Window
    {
        // Názov súboru, kde budeme držať všetky objednávky v aplikácii
        private string suborObjednavok = "objednavky.json";

        public HistoriaWindow()
        {
            InitializeComponent();
            NacitajDataZasielok();
        }

        private void NacitajDataZasielok()
        {
            // Ak súbor ešte neexistuje (nikto nič nekúpil), vykočíme z metódy
            if (!File.Exists(suborObjednavok))
            {
                return;
            }

            try
            {
                // 1. Prečítame celý text zo súboru
                string jsonText = File.ReadAllText(suborObjednavok);

                // 2. Prevedieme text na zoznam objektov Objednavka
                List<Objednavka> vsetkyObjednavky = JsonSerializer.Deserialize<List<Objednavka>>(jsonText);

                if (vsetkyObjednavky != null)
                {
                    List<Objednavka> prefiltrovaneObjednavky = new List<Objednavka>();

                    // 3. Prejdeme cyklom objednávky a vyberieme len tie, ktoré spravil prihlásený človek
                    foreach (var obj in vsetkyObjednavky)
                    {
                        if (obj.Uzivatel == MainWindow.PrihlasenyUzivatel)
                        {
                            prefiltrovaneObjednavky.Add(obj);
                        }
                    }

                    // Zoznam otočíme, aby bola najnovšia objednávka hore
                    prefiltrovaneObjednavky.Reverse();

                    // 4. Pošleme dáta do XAML grafického zoznamu
                    HistoriaList.ItemsSource = prefiltrovaneObjednavky;
                }
            }
            catch
            {
                // Ak by bola v súbore nejaká chyba, program nespadne
                MessageBox.Show("Nepodarilo sa načítať históriu nákupov.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}