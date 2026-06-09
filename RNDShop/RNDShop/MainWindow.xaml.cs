using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RND_clothing_e_shop
{
    public partial class MainWindow : Window
    {
        // Predvolene je nastavená na "Hosť", ak by niekto klikol na "Pokračovať ako Hosť"
        public static string PrihlasenyUzivatel = "Hosť";

        public MainWindow()
        {
            // načíta všetky UI prvky z XAML (tlačidlá, video, layout)
            InitializeComponent();

            // keď sa okno úplne načíta → spustí sa MainWindow_Loaded
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // nastaví video ako pozadie
            BackgroundVideo.Source = new Uri("Videos/wpf projekt rnd.mp4", UriKind.Relative);
            // relatívna cesta k videu v projekte

            // keď video skončí, zavolá sa metóda loopu
            BackgroundVideo.MediaEnded += BackgroundVideo_MediaEnded;

            // spustí prehrávanie videa
            BackgroundVideo.Play();
        }

        // spustí sa keď video skončí
        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero; // vráti na video začiatok
            BackgroundVideo.Play(); // znova spustí video (loop efekt)
        }

        // Login tlacidlo
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // vytvorí nové okno pre prihlásenie
            Prihlasenie prihlasenie = new Prihlasenie();
            // zobrazí login okno
            prihlasenie.Show();

            // zavrie aktuálne hlavné okno
            this.Close();
        }

        // Register tlacidlo
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Registracia registracia = new Registracia();
            registracia.Show();

            this.Close();
        }

        // Spusti aplikaciu bez prihlasenia/vytvorenia uctu
        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            // Pre istotu nastavíme, že ide o hosťa
            MainWindow.PrihlasenyUzivatel = "Hosť";

            ShopPage shopPage = new ShopPage();
            shopPage.Show();

            this.Close();
        }
    }
}