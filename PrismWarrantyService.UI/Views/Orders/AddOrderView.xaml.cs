using System.Windows;

namespace PrismWarrantyService.UI.Views.Orders
{
    public partial class AddOrderView : Window
    {
        public AddOrderView()
        {
            InitializeComponent();
        }

        private void NeedNewClient_Checked(object sender, RoutedEventArgs e)
        {
            nameTextBox.IsReadOnly = false;
            companyTextBox.IsReadOnly = false;
            emailTextBox.IsReadOnly = false;
            phoneNumberTextBox.IsReadOnly = false;   
        }

        private void NeedNewClient_Unchecked(object sender, RoutedEventArgs e)
        {
            nameTextBox.IsReadOnly = true;
            companyTextBox.IsReadOnly = true;
            emailTextBox.IsReadOnly = true;
            phoneNumberTextBox.IsReadOnly = true;

            nameTextBox.Clear();
            companyTextBox.Clear();
            emailTextBox.Clear();
            phoneNumberTextBox.Clear();
        }
    }
}
