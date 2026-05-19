using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RND_clothing_e_shop
{
    public partial class ShippingWindow : Window
    {
        private decimal cenaProduktov = 0;
        private decimal zakladnaCenaDopravy = 4.99m;
        private decimal prplatokZaStat = 0.00m;

        // NOVÉ: Premenná, ktorá drží percentuálnu hodnotu zľavy (0.10m = 10%)
        private decimal percentualnaZlava = 0.00m;

        public ShippingWindow()
        {
            InitializeComponent();
            PopulateCountries();
            VypocitajCenu();
        }

        private void PopulateCountries()
        {
            List<string> countries = new List<string>
            {
                "Slovensko", "Cesko", "Polsko", "Madarsko", "Raksuko", "", "United Kingdom"
            };

            CountryComboBox.ItemsSource = countries;
            CountryComboBox.SelectedItem = "Slovensko";
        }

        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem == null) return;

            string vybranyStat = CountryComboBox.SelectedItem.ToString();

            if (vybranyStat == "Slovakia" || vybranyStat == "Slovensko")
            {
                prplatokZaStat = 0.00m;
            }
            else if (vybranyStat == "Cesko" || vybranyStat == "Polsko" || vybranyStat == "Madarsko" || vybranyStat == "Rakusko")
            {
                prplatokZaStat = 3.00m;
            }
            else
            {
                prplatokZaStat = 10.00m;
            }
            
            VypocitajCenu();
        }

        // NOVÉ: Logika tlačidla pre uplatnenie zľavového kódu
        private void ApplyDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            if (DiscountBox == null) return;

            string zadanyKod = DiscountBox.Text.Trim();

            if (zadanyKod == "VITAJ10")
            {
                percentualnaZlava = 0.10m; // Nastavíme 10% zľavu
                MessageBox.Show("Zľavový kód VITAJ10 bol úspešne uplatnený! Získavaš 10% zľavu na produkty.", "Zľava uplatnená 🎉", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (string.IsNullOrWhiteSpace(zadanyKod))
            {
                MessageBox.Show("Najskôr zadaj nejaký zľavový kód.", "Upozornenie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Ak zadal hlúposť, zľava sa vynuluje a vypíše sa chyba
                percentualnaZlava = 0.00m;
                MessageBox.Show("Tento zľavový kód nie je platný alebo vypršala jeho platnosť.", "Neplatný kód ❌", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Po kliknutí hneď prepočítame celkovú sumu na obrazovke
            VypocitajCenu();
        }

        private void VypocitajCenu()
        {
            if (ProductsPriceText == null || TotalPriceText == null || ShippingPriceText == null) return;

            cenaProduktov = 0;
            if (ShopPage.KosikList != null)
            {
                foreach (var produkt in ShopPage.KosikList)
                {
                    cenaProduktov += produkt.Price * produkt.Quantity;
                }
            }

            // NOVÉ: Odpočítanie zľavy z ceny produktov pred pripočítaním dopravy
            decimal zlavaVSumach = cenaProduktov * percentualnaZlava;
            decimal cenaProduktovPoZlave = cenaProduktov - zlavaVSumach;

            if (CourierRadio != null)
                CourierRadio.Content = $"Kuriér - {(4.99m + prplatokZaStat):N2} €";

            if (PacketaRadio != null)
                PacketaRadio.Content = $"Packeta - {(2.99m + prplatokZaStat):N2} €";

            if (PickupRadio != null)
            {
                decimal cenaOdber = 0.00m + prplatokZaStat;
                if (cenaOdber == 0)
                    PickupRadio.Content = "Osobný odber - zadarmo";
                else
                    PickupRadio.Content = $"Osobný odber - {cenaOdber:N2} €";
            }

            decimal celkovaDoprava = zakladnaCenaDopravy + prplatokZaStat;

            // NOVÉ: Celková suma berie do úvahy cenu po zľave
            decimal celkom = cenaProduktovPoZlave + celkovaDoprava;

            ProductsPriceText.Text = $"{cenaProduktovPoZlave:N2} €";
            ShippingPriceText.Text = $"{celkovaDoprava:N2} €";
            TotalPriceText.Text = $"{celkom:N2} €";
        }

        private void CountryComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CountryComboBox.IsFocused) CountryComboBox.IsDropDownOpen = true;
        }

        private bool SkontrolujUdaje()
        {
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text)) return Chyba("Zadaj meno.");
            if (string.IsNullOrWhiteSpace(LastNameBox.Text)) return Chyba("Zadaj priezvisko.");
            if (string.IsNullOrWhiteSpace(EmailBox.Text) || !EmailBox.Text.Contains("@")) return Chyba("Zadaj platný email.");
            if (string.IsNullOrWhiteSpace(PhoneBox.Text)) return Chyba("Zadaj telefónne číslo.");
            if (string.IsNullOrWhiteSpace(StreetBox.Text)) return Chyba("Zadaj ulicu.");
            if (string.IsNullOrWhiteSpace(CityBox.Text)) return Chyba("Zadaj mesto.");
            if (string.IsNullOrWhiteSpace(ZipBox.Text)) return Chyba("Zadaj PSČ.");
            if (string.IsNullOrEmpty(CountryComboBox.Text)) return Chyba("Vyber si štát.");

            return true;
        }

        private bool Chyba(string sprava)
        {
            MessageBox.Show(sprava, "Chýbajúce údaje", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SkontrolujUdaje()) return;

            if (CardPaymentOption.IsChecked == true)
            {
                PaymentWindow platobneOkno = new PaymentWindow();
                platobneOkno.Owner = this;
                bool? vysledok = platobneOkno.ShowDialog();

                if (vysledok != true) return;
            }

            MessageBox.Show("Objednávka bola úspešne prijatá a je na ceste k vám!", "Úspech", MessageBoxButton.OK, MessageBoxImage.Information);

            if (ShopPage.KosikList != null) ShopPage.KosikList.Clear();
            new ShopPage().Show();
            this.Close();
        }
        

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new KosikWindow().Show();
            this.Close();
        }

        private void CourierRadio_Checked(object sender, RoutedEventArgs e) { zakladnaCenaDopravy = 4.99m; VypocitajCenu(); }
        private void PacketaRadio_Checked(object sender, RoutedEventArgs e) { zakladnaCenaDopravy = 2.99m; VypocitajCenu(); }
        private void PickupRadio_Checked(object sender, RoutedEventArgs e) { zakladnaCenaDopravy = 0.00m; VypocitajCenu(); }
    }
}