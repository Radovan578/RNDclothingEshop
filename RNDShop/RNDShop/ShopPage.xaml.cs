using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace RND_clothing_e_shop
{
    public partial class ShopPage : Window
    {
        public static List<Produkt> KosikList = new List<Produkt>();
        private List<Produkt> VsetkyProdukty = new List<Produkt>();

        public ShopPage()
        {
            InitializeComponent();
            NacitajData();
            ZobrazProdukty("Všetko");
        }

        private void NacitajData()
        {
            VsetkyProdukty = new List<Produkt>
            {
                new Produkt { Name = "Biele tričko", Price = 19.99m, Category = "Tričká", ImagePath = "Images/tricko predok.jpeg" },
                new Produkt { Name = "Čierna mikina", Price = 39.99m, Category = "Mikiny", ImagePath = "Images/mikina pred.jpeg"},
                new Produkt { Name = "Rifle", Price = 49.99m, Category = "Nohavice", ImagePath = "Images/rifle predok.jpeg"},
                new Produkt { Name = "Bunda", Price = 89.99m, Category = "Bundy", ImagePath = "Images/bunda predok.jpg"},
                new Produkt { Name = "Čierné tenisky", Price = 59.99m, Category = "Topánky", ImagePath = "Images/tenisky.jpg"},
                new Produkt { Name = "Hodvábna šatka", Price = 12.50m, Category = "Doplnky", ImagePath = "Images/satka 2.jpg"},
                new Produkt { Name = "Béžové tričko s potlačou", Price = 23.99m, Category = "Tričká", ImagePath = "Images/bezove tricko s potlacou predok.jpg"},
                new Produkt { Name = "Čierne tričko", Price = 19.99m, Category = "Tričká", ImagePath = "Images/cierne tricko predok.jpg"},
                new Produkt { Name = "Čierne tričko s potlačou", Price = 23.99m, Category = "Tričká", ImagePath = "Images/cierne tricko s potlacou predok.jpg"},
                new Produkt { Name = "Rúžové tričko s potlačou", Price = 23.99m, Category = "Tričká", ImagePath = "Images/ruzove tricko s potalcou predok.jpg"},
                new Produkt { Name = "Fialová mikina", Price = 39.99m, Category = "Mikiny", ImagePath = "Images/fialova mikina predok.jpg"},
                new Produkt { Name = "Modrá mikina", Price = 40.99m, Category = "Mikiny", ImagePath = "Images/modra mikca 1.jpg"},
                new Produkt { Name = "Post Malone mikina", Price = 42.99m, Category = "Mikiny", ImagePath = "Images/post malone mikina predok.jpg"},
                new Produkt { Name = "Rúžová mikina", Price = 23.99m, Category = "Mikiny", ImagePath = "Images/ruzova mikina.jpg"},
                new Produkt { Name = "Sivá mikina", Price = 39.99m, Category = "Mikiny", ImagePath = "Images/siva mikca 4.jpg"},
                new Produkt { Name = " Čierne streetwear tenisky", Price = 45.99m, Category = "Topánky", ImagePath = "Images/cierne street wear tenisky.jpg"},
                new Produkt { Name = "Čierne elegantné topánky", Price = 69.99m, Category = "Topánky", ImagePath = "Images/cierne topanky elegantne.jpg"},
                new Produkt { Name = "Sive skater tenisky", Price = 49.99m, Category = "Topánky", ImagePath = "Images/sive skater tenisky.jpg"},
                new Produkt { Name = "Bielé tenisky", Price = 59.99m, Category = "Topánky", ImagePath = "Images/tenisky biele.jpg"},
                new Produkt { Name = "Bielé tričko", Price = 29.99m, Category = "Tričká", ImagePath = "Images/biele triko potlac.jpg"},
                new Produkt { Name = "Čierne tričko", Price = 29.99m, Category = "Tričká", ImagePath = "Images/cierne tricko s potlacou 2 predok.jpg"},
                new Produkt { Name = "Šedé tričko", Price = 29.99m, Category = "Tričká", ImagePath = "Images/sede triko 3.jpg"},
                new Produkt { Name = "Modrožltá mikina", Price = 39.99m, Category = "Mikiny", ImagePath = "Images/modrozlta mikina potlac.jpg"},
                new Produkt { Name = "Sivá mikina", Price = 39.99m, Category = "Mikiny", ImagePath = "Images/siva mikca 1.jpg"},
                new Produkt { Name = "Flared rifle", Price = 49.99m, Category = "Nohavice", ImagePath = "Images/flared jeans 1.jpg"},
                new Produkt { Name = "Flared rifle", Price = 49.99m, Category = "Nohavice", ImagePath = "Images/flared jeans 2.jpg"},
                new Produkt { Name = "Flared rifle", Price = 49.99m, Category = "Nohavice", ImagePath = "Images/flared jeans 3.jpg"},
                new Produkt { Name = "Wide leg rifle", Price = 39.99m, Category = "Nohavice", ImagePath = "Images/baggy jeans 4.jpg"},
                new Produkt { Name = "Regular fit rifle", Price = 39.99m, Category = "Nohavice", ImagePath = "Images/baggy jeans 5.jpg"},
                new Produkt { Name = "Straight fit rifle", Price = 39.99m, Category = "Nohavice", ImagePath = "Images/baggy jeans 6.jpg"},
                new Produkt { Name = "Baggy rifle", Price = 39.99m, Category = "Nohavice", ImagePath = "Images/baggy jeans 7.jpg"},
            };
        }

        private void ZobrazProdukty(string kategoria)
        {
            ProductsPanel.Children.Clear();

            List<Produkt> filtrovane;
            if (kategoria == "Všetko")
            {
                filtrovane = VsetkyProdukty;
            }
            else
            {
                filtrovane = VsetkyProdukty.Where(p => p.Category == kategoria).ToList();
            }

            foreach (var prod in filtrovane)
            {
                Border card = new Border
                {
                    Width = 220,
                    Height = 320,
                    Background = (Brush)new BrushConverter().ConvertFromString("#FF2A2A2A"),
                    CornerRadius = new CornerRadius(15),
                    Margin = new Thickness(10),
                    Padding = new Thickness(10),
                    Cursor = Cursors.Hand
                };

                StackPanel stack = new StackPanel();

                Border imageContainer = new Border
                {
                    Height = 120,
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(0, 0, 0, 10),
                    Cursor = Cursors.Hand
                };

                Image img = new Image
                {
                    Height = 110,
                    Stretch = Stretch.Uniform,
                    Cursor = Cursors.Hand
                };

                if (!string.IsNullOrEmpty(prod.ImagePath))
                {
                    try
                    {
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.UriSource = new Uri(prod.ImagePath, UriKind.RelativeOrAbsolute);
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                        img.Source = bi;
                    }
                    catch { }
                }

                imageContainer.Child = img;

                TextBlock nameTxt = new TextBlock
                {
                    Text = prod.Name,
                    Foreground = Brushes.White,
                    FontSize = 18,
                    FontWeight = FontWeights.SemiBold,
                    TextAlignment = TextAlignment.Center
                };

                TextBlock priceTxt = new TextBlock
                {
                    Text = $"{prod.Price:N2} €",
                    Foreground = Brushes.Gray,
                    Margin = new Thickness(0, 5, 0, 10),
                    TextAlignment = TextAlignment.Center
                };

                Button addBtn = new Button
                {
                    Content = "Pridať do košíka",
                    Height = 40,
                    Style = (Style)FindResource("RoundedButtonStyle"),
                    Background = Brushes.White,
                    Foreground = Brushes.Black,
                    Cursor = Cursors.Hand
                };

                addBtn.Click += (s, e) => AddToCart(prod.Name, prod.Price, prod.ImagePath);


                imageContainer.MouseLeftButtonDown += (s, e) =>
                {
                    new DetailProduktu(prod).Show();
                    this.Close();
                };


                stack.Children.Add(imageContainer);
                stack.Children.Add(nameTxt);
                stack.Children.Add(priceTxt);
                stack.Children.Add(addBtn);

                card.Child = stack;
                ProductsPanel.Children.Add(card);
            }
        }

        private void AddToCart(string name, decimal price, string imagePath)
        {
            var polozka = KosikList.FirstOrDefault(p => p.Name == name);
            if (polozka != null)
            {
                polozka.Quantity++;
            }
            else
            {

                KosikList.Add(new Produkt { Name = name, Price = price, Quantity = 1, ImagePath = imagePath });
            }
            MessageBox.Show($"{name} bol pridaný do košíka.");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            new KosikWindow().Show();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            JsonServis.DeleteUsers();
            JsonServis.DeleteKosik();
            new MainWindow().Show();
            this.Close();
        }

        // Kategórie
        private void AllCategory_Click(object sender, RoutedEventArgs e) => ZobrazProdukty("Všetko");
        private void TrickaCategory_Click(object sender, RoutedEventArgs e) => ZobrazProdukty("Tričká");
        private void MikinyCategory_Click(object sender, RoutedEventArgs e) => ZobrazProdukty("Mikiny");
        private void NohaviceCategory_Click(object sender, RoutedEventArgs e) => ZobrazProdukty("Nohavice");
        private void BundyCategory_Click(object sender, RoutedEventArgs e) => ZobrazProdukty("Bundy");
        private void TopankyCategory_Click(object sender, RoutedEventArgs e) => ZobrazProdukty("Topánky");
        private void DoplnkyCategory_Click(object sender, RoutedEventArgs e) => ZobrazProdukty("Doplnky");


        private void AddWhiteShirt_Click(object sender, RoutedEventArgs e)
            => AddToCart("Biele Tričko", 19.99m, "C:\\Users\\cipkod25\\source\\repos\\csharp\\obchod eshop\\RND clothing e-shop\\Images\\biele tricko predok.jpg");

        private void AddBlackHoodie_Click(object sender, RoutedEventArgs e)
            => AddToCart("Čierna Mikina", 39.99m, "C:\\Users\\cipkod25\\source\\repos\\csharp\\obchod eshop\\RND clothing e-shop\\Images\\sweater front.jpg");

        private void AddJeans_Click(object sender, RoutedEventArgs e)
            => AddToCart("Rifle", 49.99m, "C:\\Users\\cipkod25\\source\\repos\\csharp\\obchod eshop\\RND clothing e-shop\\Images\\rifle pred.jpg");

        private void AddJacket_Click(object sender, RoutedEventArgs e)
            => AddToCart("Bunda", 89.99m, "C:\\Users\\cipkod25\\source\\repos\\csharp\\obchod eshop\\RND clothing e-shop\\Images\\bunda predok.jpg");
    }
}