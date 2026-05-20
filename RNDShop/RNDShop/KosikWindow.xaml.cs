using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RND_clothing_e_shop
{
    public partial class KosikWindow : Window
    {
        // Konštruktor - spustí sa pri otvorení okna
        public KosikWindow()
        {
            InitializeComponent(); // načíta UI z XAML súboru
            ShowCart(); // hneď zobrazí obsah košíka
        }

        // Metóda ktorá vykreslí celý košík na obrazovku
        private void ShowCart()
        {
            // Vymaže staré položky (aby sa nezdvojovali)
            KosikItemsPanel.Children.Clear();

            decimal celkovaSuma = 0;      // zaciatocna cena košíka
            int pocetProdukt = 0;         // zaciatocny počet kusov

            // Prejde všetky produkty v košíku
            foreach (Produkt produkt in ShopPage.KosikList)
            {
                // vypočíta cenu (cena * množstvo)
                celkovaSuma += produkt.Price * produkt.Quantity;
                // spočíta počet kusov
                pocetProdukt += produkt.Quantity;
                // zobrazí počet produktov v UI
                ProduktCountText.Text = pocetProdukt.ToString();

                //Vytvorenie karty produktu
                Border card = new Border
                {
                    Background = (Brush)new BrushConverter().ConvertFromString("#FF262626"),
                    CornerRadius = new CornerRadius(18),
                    Padding = new Thickness(18),
                    Margin = new Thickness(0, 0, 0, 16)
                };

                // rozloženie karty (3 stĺpce)
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(130) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });

                //Vytvorenie obrazka
                Border imageBorder = new Border
                {
                    Width = 110,
                    Height = 110,
                    Background = (Brush)new BrushConverter().ConvertFromString("#FFFFFFFF"),
                    CornerRadius = new CornerRadius(14),
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                // text ak obrázok neexistuje
                TextBlock placeholder = new TextBlock
                {
                    Text = "Obrázok",
                    Foreground = (Brush)new BrushConverter().ConvertFromString("#AAAAAA"),
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Image img = new Image { Stretch = Stretch.Uniform };

                try
                {
                    // ak existuje cesta k obrázku
                    if (!string.IsNullOrEmpty(produkt.ImagePath))
                    {
                        // načíta obrázok zo súboru
                        img.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(produkt.ImagePath)));
                        // vloží obrázok do rámika
                        imageBorder.Child = img;
                    }
                    else
                    {
                        // ak obrázok neexistuje, zobrazí text
                        imageBorder.Child = placeholder;
                    }
                }
                catch
                {
                    // ak nastane chyba pri načítaní obrázka > fallback text
                    imageBorder.Child = placeholder;
                }

                //Info o produkte
                StackPanel infoPanel = new StackPanel
                {
                    Margin = new Thickness(10, 0, 20, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };

                // názov produktu
                infoPanel.Children.Add(new TextBlock
                {
                    Text = produkt.Name,
                    Foreground = Brushes.White,
                    FontSize = 22,
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 8)
                });

                // cena za kus
                infoPanel.Children.Add(new TextBlock
                {
                    Text = $"Cena za kus: {produkt.Price:N2} €",
                    Foreground = (Brush)new BrushConverter().ConvertFromString("#DDDDDD"),
                    FontSize = 16,
                    Margin = new Thickness(0, 0, 0, 6)
                });

                // veľkosť produktu
                infoPanel.Children.Add(new TextBlock
                {
                    Text = $"Veľkosť: {produkt.Size}",
                    Foreground = (Brush)new BrushConverter().ConvertFromString("#BBBBBB"),
                    FontSize = 15
                });

                //Ovladacie tlacidla
                StackPanel actionPanel = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Right
                };

                TextBlock qtyTitle = new TextBlock
                {
                    Text = "Množstvo",
                    Foreground = Brushes.White,
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 8)
                };

                // horizontálny riadok: -  číslo  +
                StackPanel qtyRow = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                //Minus tlacidlo
                Button minus = new Button
                {
                    Content = "-",
                    Style = (Style)FindResource("SmallButtonStyle"),
                    Tag = produkt
                    // uloží referenciu na produkt
                };
                minus.Click += MinusButton_Click;

                // zobrazí aktuálne množstvo
                TextBlock qtyText = new TextBlock
                {
                    Text = produkt.Quantity.ToString(),
                    Foreground = Brushes.White,
                    FontSize = 20,
                    Width = 40,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                //Plus tlacidlo
                Button plus = new Button
                {
                    Content = "+",
                    Style = (Style)FindResource("SmallButtonStyle"),
                    Tag = produkt
                };
                plus.Click += PlusButton_Click;

                // pridanie do riadku
                qtyRow.Children.Add(minus);
                qtyRow.Children.Add(qtyText);
                qtyRow.Children.Add(plus);

                // Tlacidlo odstranenia produktu
                Button remove = new Button
                {
                    Content = "Odstrániť",
                    Height = 42,
                    Margin = new Thickness(0, 14, 0, 0),
                    Background = (Brush)new BrushConverter().ConvertFromString("#FFB71C1C"),
                    Foreground = Brushes.White,
                    Cursor = Cursors.Hand,
                    Style = (Style)FindResource("RoundedButtonStyle"),
                    Tag = produkt
                };
                remove.Click += RemoveItem_Click;

                // pridanie do panelu
                actionPanel.Children.Add(qtyTitle);
                actionPanel.Children.Add(qtyRow);
                actionPanel.Children.Add(remove);

                //Zlozenie karty
                Grid.SetColumn(imageBorder, 0);
                Grid.SetColumn(infoPanel, 1);
                Grid.SetColumn(actionPanel, 2);

                grid.Children.Add(imageBorder);
                grid.Children.Add(infoPanel);
                grid.Children.Add(actionPanel);

                card.Child = grid;
                KosikItemsPanel.Children.Add(card);
                // pridanie karty do UI
            }

            // zobrazenie celkovej ceny
            TotalPriceTxt.Text = $"{celkovaSuma:F2} €";
        }

        // Button na znizenie mnozstva
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is Produkt produkt)
            {
                if (produkt.Quantity > 1)
                {
                    produkt.Quantity--;  // zníženie
                }
                else
                {
                    ShopPage.KosikList.Remove(produkt);  // odstránenie
                }
                ShowCart();  // refresh UI
            }
        }

        // Button na zvysenie mnozstva
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is Produkt produkt)
            {
                produkt.Quantity++;   // zvýšenie
                ShowCart();   // refresh UI
            }
        }

        // Button na odstranenie produktu
        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is Produkt produkt)
            {
                ShopPage.KosikList.Remove(produkt);        // odstránenie z listu
                MessageBox.Show("Položka bola odstránená z košíka.");      // hláška
                ShowCart();    // refresh UI
            }
        }
        
        // Objednavka
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShopPage.KosikList.Count <= 0)
            {
                MessageBox.Show("Košík je prázdny.");
                return;
            }

            // vytvorenie novej stránky objednávky
            ShippingWindow shippingWindow = new ShippingWindow();
            shippingWindow.Show();      // otvorí nové okno
            this.Close();       // zatvorí aktuálne okno
        }

        // Tlacidlo ktore vracia na shop page
        private void ContinueShoppingButton_Click(object sender, RoutedEventArgs e)
        {
            ShopPage shopPage = new ShopPage();
            shopPage.Show();
            this.Close();
        }

        // Sipka ktora vracia na shop page
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ShopPage shopPage = new ShopPage();
            shopPage.Show();
            this.Close();
        }
    }
}