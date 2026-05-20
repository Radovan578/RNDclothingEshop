using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RND_clothing_e_shop
{
    /// <summary>
    /// Interaction logic for Prihlasenie.xaml
    /// </summary>
    public partial class Prihlasenie : Window
    {
        public Prihlasenie()
        {
            InitializeComponent();         // načíta UI z XAML
            Loaded += MainWindow_Loaded;    // keď sa okno načíta, spustí sa video pozadie
        }

        // vytvorenie služby na login a registráciu
        private AuthServis authServis = new AuthServis();

        // spustí sa keď sa okno úplne zobrazí
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // nastaví video ako pozadie
            BackgroundVideo.Source = new Uri("Videos/wpf projekt rnd.mp4", UriKind.Relative);

            // keď video skončí, spustí sa loop
            BackgroundVideo.MediaEnded += BackgroundVideo_MediaEnded;

            // spustí video
            BackgroundVideo.Play();
        }

        // keď video skončí
        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            // vráti video na začiatok
            BackgroundVideo.Position = TimeSpan.Zero;
            // znova spustí video (loop)
            BackgroundVideo.Play();
        }

        //Login tlacidlo
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // zoberie text z textboxu (meno alebo email)
            string nameOrEmail = MenoTextBox.Text;

            // zoberie heslo z password boxu
            string password = HesloPasswordBox.Password;

            // zavolá login funkciu z AuthServis
            bool uspech = authServis.Login(nameOrEmail, password);

            // sprava
            MessageBox.Show(authServis.Message);

            // ak login prešiel
            if (uspech)
            {
                // KĽÚČOVÁ OPRAVA: Uložíme meno úspešne prihláseného užívateľa do globálnej premennej
                MainWindow.PrihlasenyUzivatel = nameOrEmail;

                // otvorí shop stránku
                ShopPage shopPage = new ShopPage();
                shopPage.Show();

                // zavrie login okno
                this.Close();
            }
        }

        // Prechod na registraciu
        private void GoToRegister_Click(object sender, RoutedEventArgs e)
        {
            Registracia registracia = new Registracia();
            registracia.Show();

            this.Close();
        }

        // Sipka naspat
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}