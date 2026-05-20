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
    /// Interaction logic for Registracia.xaml
    /// </summary>
    public partial class Registracia : Window
    {
        public Registracia()
        {
            InitializeComponent();   // načíta UI z XAML

            Loaded += MainWindow_Loaded;     // keď sa okno zobrazí, spustí sa video
        }

        // služba ktorá rieši registráciu a login logiku
        private AuthServis authServis = new AuthServis();

        // keď sa okno úplne načíta
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // nastaví video ako pozadie
            BackgroundVideo.Source = new Uri("Videos/wpf projekt rnd.mp4", UriKind.Relative);

            // keď video skončí, zopakuje sa
            BackgroundVideo.MediaEnded += BackgroundVideo_MediaEnded;

            // spustí video
            BackgroundVideo.Play();
        }

        // keď video skončí
        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            // vráti video na začiatok
            BackgroundVideo.Position = TimeSpan.Zero;

            // znova ho spustí (loop)
            BackgroundVideo.Play();
        }

        //Register button
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // zoberie meno z inputu
            string username = MenoTextBox.Text;

            // zoberie email
            string email = EmailTextBox.Text;

            // zoberie heslo
            string password = HesloPasswordBox.Password;

            // zoberie potvrdenie hesla
            string provePassword = PotvrdHesloPasswordBox.Password;

            // zavolá registráciu v AuthServis
            bool uspech = authServis.Register(username, email, password, provePassword);

            // sprava
            MessageBox.Show(authServis.Message);

            if (uspech)
            {
                // JEDINÁ ZMENA: Ak registrácia prebehne úspešne, vyskočí tento zľavový kód pre košík
                MessageBox.Show("Ako darček k prvej registrácii získavaš zľavový kód: VITAJ10\n\nZadaj ho pri dokončovaní objednávky v košíku a získaš 10% zľavu na tovar!", "Uvítací bonus 🎉", MessageBoxButton.OK, MessageBoxImage.Information);

                Prihlasenie prihlasenie = new Prihlasenie();
                prihlasenie.Show();

                this.Close();
            }
        }

        //Prechod na login
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            Prihlasenie prihlasenie = new Prihlasenie();
            prihlasenie.Show();

            this.Close();
        }

        //Sipka naspat
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}