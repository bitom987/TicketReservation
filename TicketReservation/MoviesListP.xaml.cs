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
        public MoviesListP()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override async void OnAppearing()
        {   
            base.OnAppearing();
            var movies = await _moviesRepo.GetAllMovies();
            MoviesListView.ItemsSource= movies;
        }
    }
}