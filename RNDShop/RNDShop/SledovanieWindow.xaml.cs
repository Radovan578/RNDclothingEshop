using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Text.Json;

namespace RND_clothing_e_shop
{
    public partial class SledovanieWindow : Window
    {
        private string suborObjednavok = "objednavky.json";

        public SledovanieWindow()
        {
            InitializeComponent();
            ZistiStavZasielky();
        }

        private void ZistiStavZasielky()
        {
            if (!File.Exists(suborObjednavok))
            {
                return;
            }

            try
            {
                string jsonText = File.ReadAllText(suborObjednavok);
                List<Objednavka> vsetkyObjednavky = JsonSerializer.Deserialize<List<Objednavka>>(jsonText);

                if (vsetkyObjednavky != null)
                {
                    Objednavka najnovsiaObjednavka = null;

                    // hladame poslednu objednavku pre prihlaseneho uzivatela
                    foreach (var obj in vsetkyObjednavky)
                    {
                        if (obj.Uzivatel == MainWindow.PrihlasenyUzivatel)
                        {
                            najnovsiaObjednavka = obj; // kazda dalsia objednavka pre prihlaseneho uzivatela bude aktualizovat najnovsiu objednavku, cize nakoniec zostane len ta posledna
                        }

                        // ak najdeme aspon jednu objednavku pre prihlaseneho uzivatela, tak ju zobrazime
                        if (najnovsiaObjednavka != null)
                        {
                        // skryjeme text o ziadnej zasielke a ukazeme panel s detailmi
                        ZiadnaZasielkaTxt.Visibility = Visibility.Collapsed;
                        DetailZasielkyPanel.Visibility = Visibility.Visible;

                        // dosadime texty do XAML okna
                        CisloObjednavkyTxt.Text = $"Objednávka {najnovsiaObjednavka.IdObjednavky}";
                        StavZasielkyTxt.Text = najnovsiaObjednavka.StavZasielky;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nepodarilo sa overiť stav zásielky.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}