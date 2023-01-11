using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
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
    public partial class MoviesListP : ContentPage
    {
        MoviesRepo _moviesRepo = new MoviesRepo();
        UserRepo _userRepo = new UserRepo();
        private User _user = new User();
        TicketRepo _ticketRepo= new TicketRepo();
        Ticket _ticket = new Ticket();
        
        static Random r = new Random();
        private int r1 = r.Next(10);
        private int r2 = r.Next(10);
        private int r3 = r.Next(10);

        public MoviesListP()
        {
            InitializeComponent();
        }
        public MoviesListP(string UserId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _user.Id = UserId;
            _ticket.UserId = UserId;

        }
        protected override async void OnAppearing()
        {   
            base.OnAppearing();
            var movies = await _moviesRepo.GetAllMovies();

            List<Movies> promos = new List<Movies>()
            {
                new Movies(){ImageUrl=movies[r1].ImageUrl,TrailorUrl=movies[r1].TrailorUrl},
                new Movies(){ImageUrl=movies[r2].ImageUrl,TrailorUrl=movies[r2].TrailorUrl},
                new Movies(){ImageUrl=movies[r3].ImageUrl,TrailorUrl=movies[r3].TrailorUrl}
            };
            Carousel.ItemsSource = promos;

            //Automate the Carousel
            Device.StartTimer(TimeSpan.FromSeconds(5), (Func<bool>)(() =>
            {
                Carousel.Position = (Carousel.Position + 1) % promos.Count;
                return true;
            }));
            MoviesListView.ItemsSource= movies;
        }
        private async void CollectionView_MovieSelected(object sender, SelectionChangedEventArgs e)
        {
            var itemSelected = e.CurrentSelection[0] as Movies;

            if (itemSelected != null)
            {
                await Navigation.PushAsync(new MoviesDetailP(itemSelected.Id,_user.Id));
            }
        }
        private void ImgTrailer_Tapped(object sender, EventArgs e)
        {
            var img = sender as Image;
            var imgSelected = img.BindingContext as Movies;
            Navigation.PushAsync(new PlayerP(imgSelected.TrailorUrl,_user.Id));
        }
        private async void SearchMovie_TextChanged(object sender, TextChangedEventArgs e)
        {
            var movies = await _moviesRepo.GetAllMovies();
            MoviesListView.ItemsSource = movies.Where(s => s.Name.StartsWith(e.NewTextValue));

        }
    }
}