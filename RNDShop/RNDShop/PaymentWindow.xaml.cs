using System.Windows;

namespace RND_clothing_e_shop
{
    public partial class PaymentWindow : Window
    {
        public bool PlatbaUspesna { get; private set; } = false;

        public PaymentWindow()
        {
            InitializeComponent();
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            //kontrola udajov
            if (CardNumberBox.Text.Length < 12 || ExpiryBox.Text.Length < 4 || CVVBox.Password.Length < 3)
            {
                MessageBox.Show("Prosím, zadajte platné údaje o karte.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //simulacia platby
            MessageBox.Show("Platba prebehla úspešne!", "Hotovo", MessageBoxButton.OK, MessageBoxImage.Information);
            PlatbaUspesna = true;
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}