using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                new Produkt { Name = "Biele Tričko", Price = 19.99m, Category = "Tričká" },
                new Produkt { Name = "Čierna Mikina", Price = 39.99m, Category = "Mikiny" },
                new Produkt { Name = "Rifle", Price = 49.99m, Category = "Nohavice" },
                new Produkt { Name = "Bunda", Price = 89.99m, Category = "Bundy" },
                new Produkt { Name = "Tenisky", Price = 59.99m, Category = "Topánky" },
                new Produkt { Name = "Hodvábna šatka", Price = 12.50m, Category = "Doplnky" }
            };

        }

        private void ZobrazProdukty(string kategoria)
        {
            ProductsPanel.Children.Clear();

            var filtrovane = kategoria == "Všetko"
                ? VsetkyProdukty
                : VsetkyProdukty.Where(p => p.Category == kategoria).ToList();


            foreach (var prod in filtrovane)
            {
                Border card = new Border
                {
                    Width = 220,
                    Height = 280,
                    Background = (Brush)new BrushConverter().ConvertFromString("#FF2A2A2A"),
                    CornerRadius = new CornerRadius(15),
                    Margin = new Thickness(10),
                    Padding = new Thickness(10)
                };

                StackPanel stack = new StackPanel();

                Border img = new Border { Height = 120, Background = (Brush)new BrushConverter().ConvertFromString("#FF3A3A3A"), CornerRadius = new CornerRadius(10), Margin = new Thickness(0, 0, 0, 10) };

                TextBlock nameTxt = new TextBlock { Text = prod.Name, Foreground = Brushes.White, FontSize = 18, FontWeight = FontWeights.SemiBold };
                TextBlock priceTxt = new TextBlock { Text = $"{prod.Price} €", Foreground = Brushes.Gray, Margin = new Thickness(0, 5, 0, 10) };

                Button addBtn = new Button { Content = "Pridať do košíka", Height = 40, Style = (Style)FindResource("RoundedButtonStyle"), Background = Brushes.White, Foreground = Brushes.Black };
                addBtn.Click += (s, e) => AddToCart(prod.Name, prod.Price);

                stack.Children.Add(img);
                stack.Children.Add(nameTxt);
                stack.Children.Add(priceTxt);
                stack.Children.Add(addBtn);

                card.Child = stack;
                ProductsPanel.Children.Add(card);
            }
        }

        private void AddToCart(string name, decimal price)
        {
            var polozka = KosikList.FirstOrDefault(p => p.Name == name);
            if (polozka != null)
            {
                polozka.Quantity++;
            }
            else
            {
                KosikList.Add(new Produkt { Name = name, Price = price, Quantity = 1 });
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

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void AllCategory_Click(object sender, RoutedEventArgs e)
        {
            ZobrazProdukty("Všetko");
        }
        private void TrickaCategory_Click(object sender, RoutedEventArgs e)
        {
            ZobrazProdukty("Tričká");
        }
        private void MikinyCategory_Click(object sender, RoutedEventArgs e)
        {
            ZobrazProdukty("Mikiny");
        }
        private void NohaviceCategory_Click(object sender, RoutedEventArgs e)
        {
            ZobrazProdukty("Nohavice");
        }
        private void BundyCategory_Click(object sender, RoutedEventArgs e)
        {
            ZobrazProdukty("Bundy");
        }
        private void TopankyCategory_Click(object sender, RoutedEventArgs e) => ZobrazProdukty("Topánky");
        private void DoplnkyCategory_Click(object sender, RoutedEventArgs e) => ZobrazProdukty("Doplnky");

        private void AddWhiteShirt_Click(object sender, RoutedEventArgs e) => AddToCart("Biele Tričko", 19.99m);
        private void AddBlackHoodie_Click(object sender, RoutedEventArgs e) => AddToCart("Čierna Mikina", 39.99m);
        private void AddJeans_Click(object sender, RoutedEventArgs e) => AddToCart("Rifle", 49.99m);
        private void AddJacket_Click(object sender, RoutedEventArgs e) => AddToCart("Bunda", 89.99m);
    }
}