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

                    // Hľadáme úplne poslednú zapísanú objednávku tohto užívateľa
                    foreach (var obj in vsetkyObjednavky)
                    {
                        if (obj.Uzivatel == MainWindow.PrihlasenyUzivatel)
                        {
                            najnovsiaObjednavka = obj; // Každá ďalšia nájdená prepíše staršiu, čiže na konci budeme mať najnovšiu
                        }
                    }

                    // Ak sme našli aspoň jednu objednávku
                    if (najnovsiaObjednavka != null)
                    {
                        // Skryjeme text o žiadnej zásielke a ukážeme panel s detailmi
                        ZiadnaZasielkaTxt.Visibility = Visibility.Collapsed;
                        DetailZasielkyPanel.Visibility = Visibility.Visible;

                        // Dosadíme texty do XAML okna
                        CisloObjednavkyTxt.Text = $"Objednávka {najnovsiaObjednavka.IdObjednavky}";
                        StavZasielkyTxt.Text = najnovsiaObjednavka.StavZasielky;
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