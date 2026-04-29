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
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string nameOrEmail = MenoTextBox.Text;
            string password = HesloPasswordBox.Password;

            bool uspech = authServis.Login(nameOrEmail, password);

            MessageBox.Show(authServis.Message);

            if (uspech)
            {
                ShopPage shopPage = new ShopPage();
                shopPage.Show();

                this.Close();
            }
        }
        private void GoToRegister_Click(object sender, RoutedEventArgs e)
        {
            Registracia registracia = new Registracia();
            registracia.Show();

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
