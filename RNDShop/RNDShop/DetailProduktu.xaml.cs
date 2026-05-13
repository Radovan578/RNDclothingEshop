using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RND_clothing_e_shop
{
    public partial class DetailProduktu : Window
    {
        private Produkt produkt;
        private int quantity = 1;

        public DetailProduktu(Produkt produkt)
        {
            InitializeComponent();

            this.produkt = produkt;

            LoadProdukt();

        }

        private void LoadProdukt()
        {
            // názov
            ProductNameText.Text = produkt.Name;

            // cena
            ProductPriceText.Text = $"{produkt.Price:N2} €";

            //popis
            ProductDescriptionText.Text = produkt.Description;

            // obrázok
            if (!string.IsNullOrEmpty(produkt.ImagePath))
            {
                try
                {
                    ProductImage.Source = new BitmapImage(
                       new Uri(System.IO.Path.GetFullPath(produkt.ImagePath)));

                    ProductImagePlaceholder.Visibility = Visibility.Collapsed;
                }
                catch
                {
                    ProductImagePlaceholder.Visibility = Visibility.Visible;
                }
            }

            // default množstvo
            QuantityText.Text = quantity.ToString();
        }
        private void AddToCart()
        {
            if (SizeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Prosím, vyber si veľkosť!");
                return;
            }

            string size = ((ComboBoxItem)SizeComboBox.SelectedItem).Content.ToString();

            var exist = ShopPage.KosikList.FirstOrDefault(p => p.Name == produkt.Name && p.Size == size);

            if (exist != null)
            {
                exist.Quantity += quantity;
            }
            else
            {
                ShopPage.KosikList.Add(new Produkt
                {
                    Name = produkt.Name,
                    Price = produkt.Price,
                    ImagePath = produkt.ImagePath,
                    Quantity = quantity,
                    Size = size
                });
            }

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
            if (quantity <= 1)
            {

            }
            else
                quantity--;

            QuantityText.Text = quantity.ToString();
        }

        // množstvo +
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            quantity++;
            QuantityText.Text = quantity.ToString();
        }

        // add to cart
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            AddToCart();
            MessageBox.Show("Pridané do košíka");
        }

        private void BuyNowButton_Click(object sender, RoutedEventArgs e)
        {
            new KosikWindow().Show();
            this.Close();
        }

        // farby (len UI demo)
        private void BlackColorButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void WhiteColorButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void GrayColorButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BlueColorButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}