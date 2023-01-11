using Firebase.Auth;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupP : Popup<Ticket>
    {
        private Ticket _ticket;
        public PopupP(Ticket ticket)
        {
            InitializeComponent();
            _ticket = ticket;
        }

        void Booked_Clicked(System.Object sender, System.EventArgs e)
        {
            if (_ticket.Ammount != null)
                Dismiss(_ticket);
            else
                Navigation.PopAsync();
        }
       public void AmmountSelected(object sender, EventArgs e)
        {
            string selectedItem = string.Empty;

            selectedItem = SelectAmmount.Items[SelectAmmount.SelectedIndex];
            if (selectedItem != "")
            {
                _ticket.Ammount = selectedItem;
            }
        }
    }
}