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
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }
        private AuthServis authServis = new AuthServis();
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Source = new Uri("Videos/wpf projekt rnd.mp4", UriKind.Relative);

            // LOOP
            BackgroundVideo.MediaEnded += BackgroundVideo_MediaEnded;

            BackgroundVideo.Play();
        }
        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero;
            BackgroundVideo.Play();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = MenoTextBox.Text;
            string email = EmailTextBox.Text;
            string password = HesloPasswordBox.Password;
            string provePassword = PotvrdHesloPasswordBox.Password;

            bool uspech = authServis.Register(username, email, password, provePassword);

            MessageBox.Show(authServis.Message);

            if (uspech)
            {
                Prihlasenie prihlasenie = new Prihlasenie();
                prihlasenie.Show();

                this.Close();

            }

        }
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            Prihlasenie prihlasenie = new Prihlasenie();
            prihlasenie.Show();

            this.Close();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }

}
