using System;
using System.Collections.Generic;
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

            SearchBox.Foreground = Brushes.Gray;

            NacitajData();
            ZobrazProdukty("Všetko");
        }

        private void NacitajData()
        {
            VsetkyProdukty = new List<Produkt>
            {
                new Produkt { Name = "Biele tričko", Price = 19.99m, Category = "Tričká", Color = "White", ImagePath = "Images/tricko predok.jpeg", Description = "Klasické biele tričko zo 100% bavlny. Ideálny základný kúsok pod mikinu alebo k džínsom." },

                new Produkt { Name = "Čierna mikina", Price = 39.99m, Category = "Mikiny", Color = "Black", ImagePath = "Images/mikina pred.jpeg", Description = "Moderná mikina z príjemného materiálu vhodná na každodenné nosenie. Má pohodlný strih, kvalitné spracovanie a hodí sa ku jeansom aj teplákom." },

                new Produkt { Name = "Rifle", Price = 49.99m, Category = "Nohavice", Color = "Black", ImagePath = "Images/rifle predok.jpeg", Description = "Kvalitné džínsy z pevného denimu s mierne vyšúchaným efektom. Klasický strih, ktorý nikdy nevyjde z módy." },

                new Produkt { Name = "Bunda", Price = 89.99m, Category = "Bundy", Color = "Black", ImagePath = "Images/bunda predok.jpg", Description = "Odolná bunda do nepriaznivého počasia. Vodoodpudivý materiál and praktické vrecká z nej robia ideálneho spoločníka na cesty." },

                new Produkt { Name = "Čierné tenisky", Price = 59.99m, Category = "Topánky", Color = "Black", ImagePath = "Images/tenisky.jpg", Description = "Všestranné čierne tenisky s odpruženou podrážkou. Skvelé na dlhé prechádzky mestom." },

                new Produkt { Name = "Hodvábna šatka", Price = 12.50m, Category = "Doplnky", Color = "Red", ImagePath = "Images/satka 2.jpg", Description = "Jemná hodvábna šatka s elegantným vzorom. Luxusný doplnok, ktorý dodá šmrnc každému outfitu." },

                new Produkt { Name = "Béžové tričko s potlačou", Price = 23.99m, Category = "Tričká", Color = "Beige", ImagePath = "Images/bezove tricko s potlacou predok.jpg", Description = "Štýlové tričko v béžovej farbe s modernou potlačou. Mäkký materiál zabezpečí pohodlie počas celého dňa." },

                new Produkt { Name = "Čierne tričko", Price = 19.99m, Category = "Tričká", Color = "Black", ImagePath = "Images/cierne tricko predok.jpg", Description = "Minimalistické čierne tričko s krátkym rukávom. Univerzálny kúsok, ktorý sa hodí ku každému outfitu." },

                new Produkt { Name = "Čierne tričko s potlačou", Price = 23.99m, Category = "Tričká", Color = "Black", ImagePath = "Images/cierne tricko s potlacou predok.jpg", Description = "Moderné čierne tričko s výraznou grafikou. Kvalitná potlač odolná voči praniu a príjemný strih." },

                new Produkt { Name = "Rúžové tričko s potlačou", Price = 23.99m, Category = "Tričká", Color = "Pink", ImagePath = "Images/ruzove tricko s potalcou predok.jpg", Description = "Svieže ružové tričko s unikátnym dizajnom. Skvelá voľba pre oživenie vášho každodenného šatníka." },

                new Produkt { Name = "Fialová mikina", Price = 39.99m, Category = "Mikiny", Color = "Purple", ImagePath = "Images/fialova mikina predok.jpg", Description = "Štýlová fialová mikina s kapucňou a klokaním vreckom. Hrejivý materiál vás udrží v teple počas chladných dní." },

                new Produkt { Name = "Modrá mikina", Price = 40.99m, Category = "Mikiny", Color = "Blue", ImagePath = "Images/modra mikca 1.jpg", Description = "Pánska modrá mikina s elastickými manžetami. Kvalitný materiál minimalizuje žmolkovanie." },

                new Produkt { Name = "Post Malone mikina", Price = 42.99m, Category = "Mikiny", Color = "Black", ImagePath = "Images/post malone mikina predok.jpg", Description = "Limitovaná edícia mikiny s motívom Post Maloneho. Povinný kúsok pre každého fanúšika moderného streetwearu." },

                new Produkt { Name = "Rúžová mikina", Price = 23.99m, Category = "Mikiny", Color = "Pink", ImagePath = "Images/ruzova mikina.jpg", Description = "Ľahká ružová mikina ideálna na jarné večery. Mäkká podšívka zaisťuje vysoký komfort nosenia." },

                new Produkt { Name = "Sivá mikina", Price = 39.99m, Category = "Mikiny", Color = "Gray", ImagePath = "Images/siva mikca 4.jpg", Description = "Klasická sivá mikina bez potlače. Vďaka neutrálnemu dizajnu je veľmi ľahko kombinovateľná." },

                new Produkt { Name = " Čierne streetwear tenisky", Price = 45.99m, Category = "Topánky", Color = "Black", ImagePath = "Images/cierne street wear tenisky.jpg", Description = "Štýlové mestské tenisky s moderným dizajnom. Ľahká konštrukcia zabezpečí komfort pri každom kroku." },

                new Produkt { Name = "Čierne elegantné topánky", Price = 69.99m, Category = "Topánky", Color = "Black", ImagePath = "Images/cierne topanky elegantne.jpg", Description = "Kožené elegantné topánky vhodné na formálne príležitosti. Kvalitná koža a precízne detaily." },

                new Produkt { Name = "Sive skater tenisky", Price = 49.99m, Category = "Topánky", Color = "Gray", ImagePath = "Images/sive skater tenisky.jpg", Description = "Odolné šedé tenisky so spevnenou pätou. Navrhnuté špeciálne pre skateboarding a aktívny pohyb." },

                new Produkt { Name = "Bielé tenisky", Price = 59.99m, Category = "Topánky", Color = "White", ImagePath = "Images/tenisky biele.jpg", Description = "Čisté biele tenisky s nadčasovým dizajnom. Ľahko sa čistia a skvele doplnia akýkoľvek outfit." },

                new Produkt { Name = "Bielé tričko", Price = 29.99m, Category = "Tričká", Color = "White", ImagePath = "Images/biele triko potlac.jpg", Description = "Prémiové biele tričko s detailnou potlačou. Vyššia gramáž bavlny zaručuje dlhú životnosť." },

                new Produkt { Name = "Čierne tričko", Price = 29.99m, Category = "Tričká", Color = "Black", ImagePath = "Images/cierne tricko s potlacou 2 predok.jpg", Description = "Exkluzívne čierne tričko s umeleckou potlačou. Navrhnuté pre maximálne pohodlie a štýlový vzhľad." },

                new Produkt { Name = "Šedé tričko", Price = 29.99m, Category = "Tričká", Color = "Gray", ImagePath = "Images/sede triko 3.jpg", Description = "Pohodlné šedé tričko v melírovanom prevedení. Vhodné na športové aktivity aj voľný čas." },

                new Produkt { Name = "Čiernožltá mikina", Price = 39.99m, Category = "Mikiny", Color = "Black", ImagePath = "Images/modrozlta mikina potlac.jpg", Description = "Originálna farebná kombinácia. Výrazný kúsok, ktorý zvýrazní vašu osobnosť." },

                new Produkt { Name = "Sivá mikina", Price = 39.99m, Category = "Mikiny", Color = "Gray", ImagePath = "Images/siva mikca 1.jpg", Description = "Športová sivá mikina s kvalitným prešívaním. Odolná voči opotrebeniu." },

                new Produkt { Name = "Flared rifle", Price = 49.99m, Category = "Nohavice", Color = "Gray", ImagePath = "Images/flared jeans 1.jpg", Description = "Štýlové 'zvonové' džínsy inšpirované retro módou. Priliehavý strih v oblasti stehien." },

                new Produkt { Name = "Flared rifle", Price = 49.99m, Category = "Nohavice", Color = "LightBlue", ImagePath = "Images/flared jeans 2.jpg", Description = "Svetlé flared džínsy s vysokým pásom. Opticky predlžujú nohy." },

                new Produkt { Name = "Flared rifle", Price = 49.99m, Category = "Nohavice", Color = "DarkBlue", ImagePath = "Images/flared jeans 3.jpg", Description = "Tmavomodré flared džínsy pre uvoľnený vzhľad. Pružný materiál zabezpečí voľnosť pohybu." },

                new Produkt { Name = "Wide leg rifle", Price = 39.99m, Category = "Nohavice", Color = "DarkBlue", ImagePath = "Images/baggy jeans 4.jpg", Description = "Pohodlné džínsy so širokými nohavicami. Trendy strih pre milovníkov voľného štýlu." },

                new Produkt { Name = "Regular fit rifle", Price = 39.99m, Category = "Nohavice", Color = "Black", ImagePath = "Images/baggy jeans 5.jpg", Description = "Tradičné džínsy rovného strihu. Ideálna voľba pre those, ktorí preferujú klasický vzhľad." },

                new Produkt { Name = "Straight fit rifle", Price = 39.99m, Category = "Nohavice", Color = "White", ImagePath = "Images/baggy jeans 6.jpg", Description = "Džínsy s rovnými nohavicami a precíznym spracovaním. Hodia sa k teniskám aj topánkam." },

                new Produkt { Name = "Baggy rifle", Price = 39.99m, Category = "Nohavice", Color = "LightBlue", ImagePath = "Images/baggy jeans 7.jpg", Description = "Extra voľné baggy džínsy. Maximálne pohodlie a autentický streetwear štýl." },
            };
        }

        private void ZobrazProdukty(string kategoria)
        {
            ProductsPanel.Children.Clear();

            // Prepísané z LINQ na klasický foreach cyklus
            foreach (var prod in VsetkyProdukty)
            {
                if (kategoria == "Všetko" || prod.Category == kategoria)
                {
                    VytvorKartickuProduktu(prod);
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "Hľadať...")
                return;

            string hladanyText = SearchBox.Text.ToLower();
            ProductsPanel.Children.Clear();

            // Prepísané z LINQ na klasický foreach s podmienkou .Contains
            foreach (var prod in VsetkyProdukty)
            {
                if (prod.Name.ToLower().Contains(hladanyText))
                {
                    VytvorKartickuProduktu(prod);
                }
            }
        }

        // Spoločná prehľadná metóda na generovanie UI prvku (kartičky) - odstraňuje duplicitu kódu
        private void VytvorKartickuProduktu(Produkt prod)
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
                Cursor = Cursors.Hand,
                Tag = prod // Uložíme objekt do Tagu pre neskorší klik
            };
            imageContainer.MouseLeftButtonDown += ImageContainer_MouseLeftButtonDown;

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
                Cursor = Cursors.Hand,
                Tag = prod // Uložíme produkt do Tagu tlačidla
            };
            addBtn.Click += AddBtn_Click;

            stack.Children.Add(imageContainer);
            stack.Children.Add(nameTxt);
            stack.Children.Add(priceTxt);
            stack.Children.Add(addBtn);

            card.Child = stack;
            ProductsPanel.Children.Add(card);
        }

        // Klasický event namiesto podozrivej lambdy
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is Produkt prod)
            {
                AddToCart(prod.Name, prod.Price, prod.ImagePath);
            }
        }

        // Klasický event pre kliknutie na obrázok/detail produktu
        private void ImageContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            if (border != null && border.Tag is Produkt prod)
            {
                DetailProduktu detailWindow = new DetailProduktu(prod);
                detailWindow.Show();
                this.Close();
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "Hľadať...")
            {
                SearchBox.Text = "";
                SearchBox.Foreground = Brushes.White;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Hľadať...";
                SearchBox.Foreground = Brushes.Gray;
            }
        }

        private void AddToCart(string name, decimal price, string imagePath)
        {
            Produkt polozka = null;

            // Prepísané z .FirstOrDefault() na klasický hľadací foreach cyklus
            foreach (var p in KosikList)
            {
                if (p.Name == name)
                {
                    polozka = p;
                    break;
                }
            }

            if (polozka != null)
            {
                polozka.Quantity++;
            }
            else
            {
                KosikList.Add(new Produkt
                {
                    Name = name,
                    Price = price,
                    Quantity = 1,
                    ImagePath = imagePath,
                    Size = "M"
                });
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
            JsonServis.DeleteKosik();
            new MainWindow().Show();
            this.Close();
        }

        // Kategórie prepísané na normálne ľudské metódy s kučeravými zátvorkami
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

        private void TopankyCategory_Click(object sender, RoutedEventArgs e)
        {
            ZobrazProdukty("Topánky");
        }

        private void DoplnkyCategory_Click(object sender, RoutedEventArgs e)
        {
            ZobrazProdukty("Doplnky");
        }

        private void AddWhiteShirt_Click(object sender, RoutedEventArgs e)
        {
            AddToCart("Biele Tričko", 19.99m, "C:\\Users\\cipkod25\\source\\repos\\csharp\\obchod eshop\\RND clothing e-shop\\Images\\biele tricko predok.jpg");
        }

        private void AddBlackHoodie_Click(object sender, RoutedEventArgs e)
        {
            AddToCart("Čierna Mikina", 39.99m, "C:\\Users\\cipkod25\\source\\repos\\csharp\\obchod eshop\\RND clothing e-shop\\Images\\sweater front.jpg");
        }

        private void AddJeans_Click(object sender, RoutedEventArgs e)
        {
            AddToCart("Rifle", 49.99m, "C:\\Users\\cipkod25\\source\\repos\\csharp\\obchod eshop\\RND clothing e-shop\\Images\\rifle pred.jpg");
        }

        private void AddJacket_Click(object sender, RoutedEventArgs e)
        {
            AddToCart("Bunda", 89.99m, "C:\\Users\\cipkod25\\source\\repos\\csharp\\obchod eshop\\RND clothing e-shop\\Images\\bunda predok.jpg");
        }
    }
}