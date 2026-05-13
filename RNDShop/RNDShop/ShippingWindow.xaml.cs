using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RND_clothing_e_shop
{
    public partial class ShippingWindow : Window
    {
        private decimal cenaProduktov = 0;
        private decimal zakladnaCenaDopravy = 4.99m;
        private decimal prplatokZaStat = 0.00m;

        public ShippingWindow()
        {
            InitializeComponent();
            PopulateCountries();
            VypocitajCenu();
        }

        private void PopulateCountries()
        {
            try
            {
                var countries = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                    .Select(c => new RegionInfo(c.Name).DisplayName)
                    .Distinct()
                    .OrderBy(name => name)
                    .ToList();

                CountryComboBox.ItemsSource = countries;

                var slovakia = countries.FirstOrDefault(c => c.Contains("Slovak") || c.Contains("Slovensko"));
                if (slovakia != null) CountryComboBox.SelectedItem = slovakia;
            }
            catch { }
        }

        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem == null) return;

            string vybranyStat = CountryComboBox.SelectedItem.ToString();

            if (vybranyStat == "Slovakia" || vybranyStat == "Slovensko")
                prplatokZaStat = 0.00m;
            else if (new[] { "Czechia", "Czech Republic", "Poland", "Hungary", "Austria" }.Contains(vybranyStat))
                prplatokZaStat = 3.00m;
            else
                prplatokZaStat = 10.00m;

            VypocitajCenu();
        }

        private void VypocitajCenu()
        {
            if (ProductsPriceText == null || TotalPriceText == null) return;

            cenaProduktov = 0;
            if (ShopPage.KosikList != null)
            {
                foreach (var produkt in ShopPage.KosikList)
                    cenaProduktov += produkt.Price * produkt.Quantity;
            }

            decimal celkovaDoprava = zakladnaCenaDopravy + prplatokZaStat;
            decimal celkom = cenaProduktov + celkovaDoprava;

            ProductsPriceText.Text = $"{cenaProduktov:N2} €";
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

            // pokial je zvolena moznost platobna karta otvori sa okno, pokial nie uzivatel nepokracuje
            if (CardPaymentOption.IsChecked == true)
            {
                PaymentWindow platobneOkno = new PaymentWindow();
                platobneOkno.Owner = this;
                bool? vysledok = platobneOkno.ShowDialog();

                if (vysledok != true) return;
            }

            // po uspesnej platbe sa uzivatelovi zobrazi sprava
            MessageBox.Show("Objednávka bola úspešne prijatá a je na ceste k vám!", "Úspech", MessageBoxButton.OK, MessageBoxImage.Information);

            if (ShopPage.KosikList != null) ShopPage.KosikList.Clear();
            new MainWindow().Show();
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