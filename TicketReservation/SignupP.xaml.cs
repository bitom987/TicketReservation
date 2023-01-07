using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupP : ContentPage
	{
		public SignupP ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginP());
        }
        private async void SignupBtnClicked(object sender, EventArgs e)
        {
            await DisplayAlert("testsuccessfully", "thành công", "OK!");
            await Navigation.PushAsync(new LoginP());
        }

    }
}