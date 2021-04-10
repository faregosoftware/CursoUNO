using Faregosoft.Helpers;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FaregosoftJR.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog messageDialog;
            if (string.IsNullOrEmpty(EmailTbx.Text))
            {
                messageDialog = new MessageDialog("Debes ingresar tu email", "Error");
                await messageDialog.ShowAsync();
                return;
            }

            if (!RegexUtilities.IsValidEmail(EmailTbx.Text))
            {
                messageDialog = new MessageDialog("Debes ingresar un email valido", "Error");
                await messageDialog.ShowAsync();
                return;
            }

            if (string.IsNullOrEmpty(PasswordPbx.Password))
            {
                messageDialog = new MessageDialog("Debes ingresar tu contraseña", "Error");
                await messageDialog.ShowAsync();
                return;
            }

            messageDialog = new MessageDialog("Vamos bien!", "OK");
            await messageDialog.ShowAsync();

        }
    }
}
