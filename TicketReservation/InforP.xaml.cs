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
    public partial class InforP : ContentPage
    {
        public InforP()
        {
            InitializeComponent();
        }

        private async void ToMap_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapP());
        }
    }
}