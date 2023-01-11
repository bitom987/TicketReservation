using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserP : ContentPage
    {
        TicketRepo _ticketRepo = new TicketRepo();
        UserRepo _UserRepo = new UserRepo();
        MoviesRepo _moviesRepo = new MoviesRepo();
        Ticket _ticket = new Ticket();
        User _user = new User();
        Movies _movies = new Movies();
        public UserP(Ticket ticket)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _ticket = ticket;
        }

        private async void LogOut_Clicked(object sender, EventArgs e)
        {
           var action =   await DisplayActionSheet("Bạn có muốn đăng xuất", "Cancel", "Ok");

            switch (action)
            {
                case "Cancel":
                    break;
                case "Ok":
                    await Navigation.PushAsync(new LoginP());
                    break;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var tickets = await _ticketRepo.GetAllTickets(_ticket);
            List<Movies> movies = new List<Movies>();
            foreach(var t in tickets)
            {
                movies.Add(await _moviesRepo.GetMovieById(t.MoviesId));
            }
            _user.Id = _ticket.UserId;
            var uid = await _UserRepo.GetInfoById(_user);
            UserEmail.Text = uid.Email;
            foreach(var m in movies)
            {
                int price = Int32.Parse(m.TicketPrice);
                foreach(var t in tickets)
                {
                    if (t.MoviesId == m.Id)
                    {
                        int ticketammount = Int32.Parse(t.Ammount);
                        price = price * ticketammount;
                        m.Description = ticketammount.ToString();
                    }
                }
                m.TicketPrice = price.ToString();
            }
            
           
            TicketListView.ItemsSource = movies;
        }
        private async void CollectionView_MovieSelected(object sender, SelectionChangedEventArgs e)
        {
            var itemSelected = e.CurrentSelection[0] as Movies;

            if (itemSelected != null)
            {
                await Navigation.PushAsync(new MoviesDetailP(itemSelected.Id, _ticket.UserId));
            }
        }
        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesListP(_ticket.UserId));
        }

    }
}