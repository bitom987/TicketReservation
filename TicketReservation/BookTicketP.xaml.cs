using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookTicketP : ContentPage
    {
        public BookTicketP()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}