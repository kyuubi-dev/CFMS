using Crypto;
using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace CFMS
{
    public partial class SendPage : ContentPage
    {
        public SendPage()
        {
            InitializeComponent();
            SendBitcoinButton.IsEnabled = false;
        }

        private async void OnSendBTCClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddressEntry.Text) ||
                string.IsNullOrWhiteSpace(AmountEntry.Text) ||
                NetworkPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            string address = AddressEntry.Text;
            double amount;
            if (!double.TryParse(AmountEntry.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out amount))
            {
                await DisplayAlert("Error", "Invalid amount format.", "OK");
                return;
            }

            string network = NetworkPicker.SelectedItem.ToString();

            string message = $"Address: {address}\nAmount: {amount}\nNetwork: {network}";
            bool result = await DisplayAlert("Confirm sending", message, "Cancel", "OK");

            if (!result)
            {
                // User confirmed sending
                // Perform sending operation here
                await DisplayAlert("Sending", "Bitcoin sent successfully!", "OK");

                await Navigation.PushAsync(new WalletPage());
            }
            else
            {
                // User canceled sending
                await DisplayAlert("Cancelled", "Sending operation cancelled.", "OK");
            }
        }

        private void OnEntryChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AddressEntry.Text) &&
                double.TryParse(AmountEntry.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _) &&
                NetworkPicker.SelectedIndex != -1)
            {
                SendBitcoinButton.IsEnabled = true;
            }
            else
            {
                SendBitcoinButton.IsEnabled = false;
            }
        }
    }
}
