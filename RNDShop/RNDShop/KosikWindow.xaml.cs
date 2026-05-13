using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

                Border card = new Border
                {
                    Background = (Brush)new BrushConverter().ConvertFromString("#FF262626"),
                    CornerRadius = new CornerRadius(18),
                    Padding = new Thickness(18),
                    Margin = new Thickness(0, 0, 0, 16)
                };

                Grid grid = new Grid();

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(130) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });

                Border imageBorder = new Border
                {
                    Width = 110,
                    Height = 110,
                    Background = (Brush)new BrushConverter().ConvertFromString("#FFFFFFFF"),
                    CornerRadius = new CornerRadius(14),
                    HorizontalAlignment = HorizontalAlignment.Left
                };

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
                    if (!string.IsNullOrEmpty(produkt.ImagePath))
                    {
                        img.Source = new BitmapImage(
                            new Uri(System.IO.Path.GetFullPath(produkt.ImagePath)));

                        imageBorder.Child = img;
                    }
                    else
                    {
                        imageBorder.Child = placeholder;
                    }
                }
                catch
                {
                    imageBorder.Child = placeholder;
                }

                StackPanel infoPanel = new StackPanel
                {
                    Margin = new Thickness(10, 0, 20, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };

                infoPanel.Children.Add(new TextBlock
                {
                    Text = produkt.Name,
                    Foreground = Brushes.White,
                    FontSize = 22,
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 8)
                });

                infoPanel.Children.Add(new TextBlock
                {
                    Text = $"Cena za kus: {produkt.Price:N2} €",
                    Foreground = (Brush)new BrushConverter().ConvertFromString("#DDDDDD"),
                    FontSize = 16,
                    Margin = new Thickness(0, 0, 0, 6)
                });

                infoPanel.Children.Add(new TextBlock
                {
                    Text = $"Veľkosť: {produkt.Size}",
                    Foreground = (Brush)new BrushConverter().ConvertFromString("#BBBBBB"),
                    FontSize = 15
                });

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

                StackPanel qtyRow = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Button minus = new Button
                {
                    Content = "-",
                    Style = (Style)FindResource("SmallButtonStyle")
                };

                TextBlock qtyText = new TextBlock
                {
                    Text = produkt.Quantity.ToString(),
                    Foreground = Brushes.White,
                    FontSize = 20,
                    Width = 40,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Button plus = new Button
                {
                    Content = "+",
                    Style = (Style)FindResource("SmallButtonStyle")
                };

                minus.Click += (s, e) =>
                {
                    MinusButton_Click(produkt);
                    ZobrazKosik();
                };

                plus.Click += (s, e) =>
                {
                    PlusButton_Click(produkt);
                    ZobrazKosik();
                };

                qtyRow.Children.Add(minus);
                qtyRow.Children.Add(qtyText);
                qtyRow.Children.Add(plus);

                Button remove = new Button
                {
                    Content = "Odstrániť",
                    Height = 42,
                    Margin = new Thickness(0, 14, 0, 0),
                    Background = (Brush)new BrushConverter().ConvertFromString("#FFB71C1C"),
                    Foreground = Brushes.White,
                    Cursor = Cursors.Hand,
                    Style = (Style)FindResource("RoundedButtonStyle")
                };

                remove.Click += (s, e) =>
                {
                    RemoveItem_Click(produkt);
                    ZobrazKosik();
                };

                actionPanel.Children.Add(qtyTitle);
                actionPanel.Children.Add(qtyRow);
                actionPanel.Children.Add(remove);

                Grid.SetColumn(imageBorder, 0);
                Grid.SetColumn(infoPanel, 1);
                Grid.SetColumn(actionPanel, 2);

                grid.Children.Add(imageBorder);
                grid.Children.Add(infoPanel);
                grid.Children.Add(actionPanel);

                card.Child = grid;
                KosikItemsPanel.Children.Add(card);
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

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            var tlacidlo = sender as Button;
            var polozka = tlacidlo?.DataContext as dynamic;

            if (polozka != null)
            {
                if (polozka.Quantity > 1)
                {
                    polozka.Quantity--;
                }
                else
                {
                    RemoveItem_Click(sender, e);
                }
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            var tlacidlo = sender as Button;
            var polozka = tlacidlo?.DataContext as dynamic;

            if (polozka != null)
            {
                polozka.Quantity++;
            }
        }

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