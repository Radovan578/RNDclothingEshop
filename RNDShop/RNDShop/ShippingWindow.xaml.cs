using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions; // Pridané pre kontrolu znakov pomocou Regex

namespace RND_clothing_e_shop
{
    public partial class ShippingWindow : Window
    {
        // celková cena produktov v košíku
        private decimal cenaProduktov = 0;

        // základná cena dopravy
        private decimal zakladnaCenaDopravy = 4.99m;

        // príplatok podľa krajiny
        private decimal prplatokZaStat = 0.00m;

        // percentuálna zľava (napr. 0.10 = 10%)
        private decimal percentualnaZlava = 0.00m;

        public ShippingWindow()
        {
            InitializeComponent(); // načíta UI z XAML

            PopulateCountries();  // naplní zoznam krajín

            InitializeCouponStatus();  // nastaví UI stav kupónu
            
            CalculatePrice();    // prvý výpočet ceny
        }

        // Nastavenie počiatočného stavu pre info text o kupóne
        private void InitializeCouponStatus()
        {
            if (CouponStatusText != null)
            {
                CouponStatusText.Text = "Zatiaľ ste nezadali žiaden kupón";
                CouponStatusText.Foreground = Brushes.Gray;
            }
            if (DiscountRow != null)
            {
                DiscountRow.Visibility = Visibility.Collapsed;    // skryje riadok zľavy
            }
        }

        // naplní ComboBox krajinami
        private void PopulateCountries()
        {
            List<string> countries = new List<string>
            {
                "Slovensko", "Cesko", "Polsko", "Madarsko", "Rakusko", "Velka Britania"
            };

            CountryComboBox.ItemsSource = countries;  // priradí zoznam do UI
            CountryComboBox.SelectedItem = "Slovensko";  // default výber
        }

        // keď sa zmení krajina, prepočíta príplatok za dopravu
        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem == null) return;

            string vybranyStat = CountryComboBox.SelectedItem.ToString();

            // Slovensko = bez príplatku
            if (vybranyStat == "Slovakia" || vybranyStat == "Slovensko")
            {
                prplatokZaStat = 0.00m;
            }
            // okolité štáty = menší príplatok
            else if (vybranyStat == "Cesko" || vybranyStat == "Polsko" || vybranyStat == "Madarsko" || vybranyStat == "Rakusko")
            {
                prplatokZaStat = 3.00m;
            }
            // vzdialené krajiny = vyšší príplatok
            else
            {
                prplatokZaStat = 10.00m;
            }

            CalculatePrice();    // prepočíta celkovú cenu
        }

        //Logika tlačidla pre uplatnenie zľavového kódu
        private void ApplyDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            if (DiscountBox == null || CouponStatusText == null) return;

            string zadanyKod = DiscountBox.Text.Trim();

            // správny kód
            if (zadanyKod == "VITAJ10")
            {
                percentualnaZlava = 0.10m; // Nastavíme 10% zľavu
                CouponStatusText.Text = "Kupón je platný";
                CouponStatusText.Foreground = Brushes.Green;
                MessageBox.Show("Zľavový kód VITAJ10 bol úspešne uplatnený! Získavaš 10% zľavu na produkty.", "Zľava uplatnená 🎉", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // prázdny vstup
            else if (string.IsNullOrWhiteSpace(zadanyKod))
            {
                percentualnaZlava = 0.00m;
                CouponStatusText.Text = "Zatiaľ ste nezadali žiaden kupón";
                CouponStatusText.Foreground = Brushes.Gray;
                MessageBox.Show("Najskôr zadaj nejaký zľavový kód.", "Upozornenie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            // zlý kód
            else
            {
                // Ak zadal hlúposť, zľava sa vynuluje a vypíše sa chyba
                percentualnaZlava = 0.00m;
                CouponStatusText.Text = "Zadaný kupón je neplatný";
                CouponStatusText.Foreground = Brushes.Red;
                MessageBox.Show("Tento zľavový kód nie je platný alebo vypršala jeho platnosť.", "Neplatný kód ❌", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Po kliknutí hneď prepočítame celkovú sumu na obrazovke
            CalculatePrice();
        }

        // hlavný výpočet ceny
        private void CalculatePrice()
        {
            if (ProductsPriceText == null || TotalPriceText == null || ShippingPriceText == null || DiscountPriceText == null || DiscountRow == null) return;

            cenaProduktov = 0;

            // spočíta všetky produkty v košíku
            if (ShopPage.KosikList != null)
            {
                foreach (var produkt in ShopPage.KosikList)
                {
                    cenaProduktov += produkt.Price * produkt.Quantity;
                }
            }

            // Odpočítanie zľavy z ceny produktov pred pripočítaním dopravy
            decimal zlavaVSumach = cenaProduktov * percentualnaZlava;
            decimal cenaProduktovPoZlave = cenaProduktov - zlavaVSumach;

            // Zobrazenie alebo skrytie riadku so zľavou na základe toho, či je zľava aktívna
            if (zlavaVSumach > 0)
            {
                DiscountPriceText.Text = $"- {zlavaVSumach:N2} €";
                DiscountRow.Visibility = Visibility.Visible;
            }
            else
            {
                DiscountRow.Visibility = Visibility.Collapsed;
            }

            // aktualizácia cien dopravy v UI
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

            // finálna cena dopravy
            decimal celkovaDoprava = zakladnaCenaDopravy + prplatokZaStat;

            // Celková suma berie do úvahy cenu po zľave
            decimal celkom = cenaProduktovPoZlave + celkovaDoprava;

            // aktualizácia UI
            ProductsPriceText.Text = $"{cenaProduktov:N2} €"; // Ukazujeme pôvodnú plnú cenu produktov
            ShippingPriceText.Text = $"{celkovaDoprava:N2} €";
            TotalPriceText.Text = $"{celkom:N2} €";
        }

        // otvorí dropdown keď používateľ píše
        private void CountryComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CountryComboBox.IsFocused) CountryComboBox.IsDropDownOpen = true;
        }

        // kontrola všetkých formulárov
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text)) return Error("Zadaj meno.");
            if (string.IsNullOrWhiteSpace(LastNameBox.Text)) return Error("Zadaj priezvisko.");
            if (string.IsNullOrWhiteSpace(EmailBox.Text) || !EmailBox.Text.Contains("@")) return Error("Zadaj platný email.");
            if (string.IsNullOrWhiteSpace(PhoneBox.Text)) return Error("Zadaj telefónne číslo.");
            if (string.IsNullOrWhiteSpace(StreetBox.Text)) return Error("Zadaj ulicu.");
            if (string.IsNullOrWhiteSpace(CityBox.Text)) return Error("Zadaj mesto.");
            if (string.IsNullOrWhiteSpace(ZipBox.Text)) return Error("Zadaj PSČ.");
            if (string.IsNullOrEmpty(CountryComboBox.Text)) return Error("Vyber si štát.");

            return true;
        }

        // pomocná funkcia na error popup
        private bool Error(string sprava)
        {
            MessageBox.Show(sprava, "Chýbajúce údaje", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        // potvrdenie objednávky
        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckData()) return;  // kontrola údajov

            if (CardPaymentOption.IsChecked == true)    // ak karta
            {
                PaymentWindow platobneOkno = new PaymentWindow();  // otvorenie platby
                platobneOkno.Owner = this;
                bool? vysledok = platobneOkno.ShowDialog();    // čakanie na výsledok

                if (vysledok != true) return;  // zrušenie platby
            }

            MessageBox.Show("Objednávka bola úspešne prijatá a je na ceste k vám!", "Úspech", MessageBoxButton.OK, MessageBoxImage.Information);

            if (ShopPage.KosikList != null) ShopPage.KosikList.Clear();  // vymazanie košíka
            new ShopPage().Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new KosikWindow().Show();
            this.Close();
        }

        private void CourierRadio_Checked(object sender, RoutedEventArgs e) { zakladnaCenaDopravy = 4.99m; CalculatePrice(); }
        private void PacketaRadio_Checked(object sender, RoutedEventArgs e) { zakladnaCenaDopravy = 2.99m; CalculatePrice(); }
        private void PickupRadio_Checked(object sender, RoutedEventArgs e) { zakladnaCenaDopravy = 0.00m; CalculatePrice(); }

        private void Numbers_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))   // kontrola čísla
            {
                e.Handled = true;   // blokovanie vstupu
            }
        }
        private void Phone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox policko = sender as TextBox;    // získanie textboxu

            // povolenie + na začiatku
            if (e.Text == "+" && policko != null && policko.Text.Length == 0)
            {
                e.Handled = false;
                return;
            }
            // blokovanie iných znakov
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true; 
            }
        }
    }
}