using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginP : ContentPage
	{
		public LoginP ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void LoginBtnClicked(object sender, EventArgs e)
        {

        }
        private void ForgetPassBtn_Tapped(object sender, EventArgs e)
        {

        }
        private void SignupBtn_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignupP());
        }
    }
}