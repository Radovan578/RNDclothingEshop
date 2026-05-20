using System.Windows;
using System.Windows.Controls;

namespace RND_clothing_e_shop
{
    public partial class PaymentWindow : Window
    {
        // tu si program pamätá či platba prešla alebo nie
        public bool PlatbaUspesna { get; private set; } = false;

        public PaymentWindow()
        {
            InitializeComponent();    // načíta UI z XAML(textboxy, buttony atď.)
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            // kontrola či používateľ zadal údaje o karte
            if (CardNumberBox.Text.Length < 12 || ExpiryBox.Text.Length < 4 || CVVBox.Password.Length < 3)
            {
                // ak sú údaje zlé tak zobrazí chybu
                MessageBox.Show("Prosím, zadajte platné údaje o karte.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //simulacia platby
            MessageBox.Show("Platba prebehla úspešne!", "Hotovo", MessageBoxButton.OK, MessageBoxImage.Information);    // ukáže že platba prebehla úspešne

            // nastaví že platba bola úspešná
            PlatbaUspesna = true;
            // povie oknu že všetko OK
            this.DialogResult = true;
            // zavrie okno platby
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // povie že platba sa neuskutočnila
            this.DialogResult = false;
            // zavrie okno
            this.Close();
        }
    }
}