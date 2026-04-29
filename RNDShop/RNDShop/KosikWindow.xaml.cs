using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RND_clothing_e_shop
{
    public partial class KosikWindow : Window
    {
        public KosikWindow()
        {
            InitializeComponent();
            ZobrazKosik();
        }

        private void ZobrazKosik()
        {
            KosikItemsPanel.Children.Clear();
            decimal celkovaSuma = 0;

            foreach (var produkt in ShopPage.KosikList)
            {
                celkovaSuma += produkt.Price * produkt.Quantity;

                Border polozkaBorder = new Border
                {
                    Background = (Brush)new BrushConverter().ConvertFromString("#FF1A1A1A"),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(0, 0, 0, 10),
                    Padding = new Thickness(15)
                };

                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                StackPanel infoPanel = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
                infoPanel.Children.Add(new TextBlock { Text = produkt.Name, Foreground = Brushes.White, FontSize = 18, FontWeight = FontWeights.Bold });
                infoPanel.Children.Add(new TextBlock { Text = $"{produkt.Price} € / ks", Foreground = Brushes.Gray, FontSize = 14 });

                StackPanel akciePanel = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };

                Button btnMinus = new Button { Content = "-", Width = 30, Height = 30, Margin = new Thickness(5), Style = (Style)FindResource("RoundedButtonStyle") };
                btnMinus.Click += (s, e) => { MinusButton_Click(produkt); };

                TextBlock txtPocet = new TextBlock { Text = produkt.Quantity.ToString(), Foreground = Brushes.White, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10, 0, 10, 0), FontSize = 16 };

                Button btnPlus = new Button { Content = "+", Width = 30, Height = 30, Margin = new Thickness(5), Style = (Style)FindResource("RoundedButtonStyle") };
                btnPlus.Click += (s, e) => { PlusButton_Click(produkt); };

                Button btnRemove = new Button { Content = "X", Width = 30, Height = 30, Margin = new Thickness(15, 0, 0, 0), Background = Brushes.DarkRed, Foreground = Brushes.White, Style = (Style)FindResource("RoundedButtonStyle") };
                btnRemove.Click += (s, e) => { RemoveItem_Click(produkt); };

                akciePanel.Children.Add(btnMinus);
                akciePanel.Children.Add(txtPocet);
                akciePanel.Children.Add(btnPlus);
                akciePanel.Children.Add(btnRemove);

                Grid.SetColumn(infoPanel, 0);
                Grid.SetColumn(akciePanel, 1);
                grid.Children.Add(infoPanel);
                grid.Children.Add(akciePanel);

                polozkaBorder.Child = grid;
                KosikItemsPanel.Children.Add(polozkaBorder);
            }

            TotalPriceTxt.Text = $"{celkovaSuma:F2} €";
        }

        private void MinusButton_Click(Produkt p)
        {
            if (p.Quantity > 1)
            {
                p.Quantity--;
            }
            else
            {
                ShopPage.KosikList.Remove(p);
            }
            ZobrazKosik();
        }

        private void PlusButton_Click(Produkt p)
        {
            p.Quantity++;
            ZobrazKosik();
        }

        private void RemoveItem_Click(Produkt p)
        {
            ShopPage.KosikList.Remove(p);
            ZobrazKosik();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShopPage.KosikList.Count > 0)
            {
                MessageBox.Show("Objednávka bola úspešne odoslaná!");
                ShopPage.KosikList.Clear();
                ZobrazKosik();
            }
            else
            {
                MessageBox.Show("Košík je prázdny.");
            }
        }

        private void ContinueShoppingButton_Click(object sender, RoutedEventArgs e)
        {
            ShopPage shopPage = new ShopPage();
            shopPage.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ShopPage shopPage = new ShopPage();
            shopPage.Show();
            this.Close();
        }

        // 1. Zníženie množstva
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            // Zistíme, na ktoré tlačidlo sa kliklo
            var tlacidlo = sender as Button;
            // Získame dáta produktu priradeného k tomuto riadku
            var polozka = tlacidlo?.DataContext as dynamic;

            if (polozka != null)
            {
                if (polozka.Quantity > 1)
                {
                    polozka.Quantity--;
                    // Ak máš metódu na prepočet celkovej ceny, zavolaj ju tu:
                    // PrepocitajCelkovuCenu();
                }
                else
                {
                    // Ak je množstvo 1 a klikneš na mínus, položku odstránime
                    RemoveItem_Click(sender, e);
                }
            }
        }

        // 2. Zvýšenie množstva
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            var tlacidlo = sender as Button;
            var polozka = tlacidlo?.DataContext as dynamic;

            if (polozka != null)
            {
                polozka.Quantity++;
                // PrepocitajCelkovuCenu();
            }
        }

        // 3. Odstránenie položky
        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var tlacidlo = sender as Button;
            var polozka = tlacidlo?.DataContext as dynamic;

            if (polozka != null)
            {


                MessageBox.Show("Položka bola odstránená z košíka.");
            }
        }
    }
}