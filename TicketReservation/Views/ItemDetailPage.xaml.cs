using System.ComponentModel;
using TicketReservation.ViewModels;
using Xamarin.Forms;

namespace TicketReservation.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}