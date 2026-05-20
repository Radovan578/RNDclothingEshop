using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RND_clothing_e_shop
{
    public partial class DetailProduktu : Window
    {
        private Produkt produkt;

        // zaciatocne množstvo 
        private int quantity = 1;

        public DetailProduktu(Produkt produkt)
        {
            InitializeComponent();   // načíta UI z XAML


            this.produkt = produkt;  // uloží produkt do globálnej premennej tejto triedy

            LoadProdukt();    // načíta všetky údaje do UI

        }

        // načítanie dát produktu do obrazovky
        private void LoadProdukt()
        {
            // názov produktu
            ProductNameText.Text = produkt.Name;

            // cena produktu
            ProductPriceText.Text = $"{produkt.Price:N2} €";

            // popis produktu
            ProductDescriptionText.Text = produkt.Description;

            // nastaví farbu tlačidla podľa produktu
            ColorButton.Background = (Brush)new BrushConverter().ConvertFromString(produkt.Color);

            // obrázok produktu
            if (!string.IsNullOrEmpty(produkt.ImagePath))
            {
                // ak existuje cesta k obrázku
                try
                {
                    // načíta obrázok zo súboru
                    ProductImage.Source = new BitmapImage(
                       new Uri(System.IO.Path.GetFullPath(produkt.ImagePath)));

                    // schová placeholder (text "obrázok")
                    ProductImagePlaceholder.Visibility = Visibility.Collapsed;

                }
                catch
                {
                    // ak sa obrázok nepodarí načítať
                    ProductImagePlaceholder.Visibility = Visibility.Visible;
                }
            }

            // zobrazí aktuálne množstvo
            QuantityText.Text = quantity.ToString();
        }
        
        //Pridanie do kosika
        private void AddToCart()
        {
            // kontrola či používateľ vybral veľkosť
            if (SizeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Prosím, vyber si veľkosť!");
                return;  // stop
            }

            // získa vybranú veľkosť z ComboBoxu
            string size = ((ComboBoxItem)SizeComboBox.SelectedItem).Content.ToString();

            // skontroluje či už taký produkt v košíku existuje
            Produkt exist = null;
            foreach (var p in ShopPage.KosikList)
            {
                if (p.Name == produkt.Name && p.Size == size)
                {
                    exist = p;   // Našli sme rovnaký produkt s rovnakou veľkosťou
                    break;       // Ukončíme cyklus, lebo ďalej hľadať netreba
                }
            }

            // ak už existuje tak iba zvýš množstvo
            if (exist != null)
            {
                exist.Quantity += quantity;
            }
            else
            {
                // inak vytvor nový produkt v košíku
                ShopPage.KosikList.Add(new Produkt
                {
                    Name = produkt.Name,
                    Price = produkt.Price,
                    ImagePath = produkt.ImagePath,
                    Quantity = quantity,
                    Size = size
                });
            }

            // reset množstva po pridaní
            quantity = 1;
            QuantityText.Text = "1";
        }
        

        // BACK button
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new ShopPage().Show();
            this.Close();
        }

        // množstvo -
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            // ak je množstvo 1 alebo menej, nič nerob
            if (quantity <= 1)
            {
                // zámerne prázdne (nechce ísť pod 1)
            }
            else
                quantity--;   // zníži množstvo

            QuantityText.Text = quantity.ToString();   // aktualizuje text v UI
        }

        // množstvo +
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            quantity++;   // zvýši množstvo
            QuantityText.Text = quantity.ToString();  // aktualizuje UI
        }

        // add to cart
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            AddToCart();    // zavolá hlavnú logiku pridania
            MessageBox.Show("Pridané do košíka");   // sprava
        }

        //Button na prejdenie do kosika
        private void BuyNowButton_Click(object sender, RoutedEventArgs e)
        {
            new KosikWindow().Show();
            this.Close();
        }

        // Button na farbu produktu
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}