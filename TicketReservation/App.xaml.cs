using System;
using TicketReservation.Services;
using TicketReservation.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Device.SetFlags(new string[] { "MediaElement_Experimental" });
            MainPage = new NavigationPage(new LoginP());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
