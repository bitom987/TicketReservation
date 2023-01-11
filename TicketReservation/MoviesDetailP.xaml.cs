using Firebase.Auth;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesDetailP : ContentPage
    {
        private Movies _movie = new Movies();
        private User _user = new User();
        private Ticket _ticket = new Ticket();
        MoviesRepo _movieRepo = new MoviesRepo();
        TicketRepo _ticketRepo = new TicketRepo();
        public MoviesDetailP(string movieId,string UserId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            GetMovieDetail(movieId);
            _user.Id = UserId;
            _ticket.UserId = UserId;
            _ticket.MoviesId = movieId;
            _movie.Id = movieId;
        }
        private async void GetMovieDetail(string movieId)
        {
            _movie = await _movieRepo.GetMovieById(movieId);
            MovieDetailImg.Source = _movie.ImageUrl;
            MovieDetailTitle.Text = _movie.Name;
            MovieDetailPlayingDate.Text = _movie.PlayingDate;
            MovieDetailPlayingTime.Text = _movie.PlayingTime;
            LblTicketPrice.Text = _movie.TicketPrice;
            LblMovieDescription.Text = _movie.Description;
            ImgMovieCover.Source = _movie.ImageUrl;
        }
        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesListP(_user.Id));
        }


        private async void BookTicketClicked(object sender, EventArgs e)
        {
           var ticket = await Navigation.ShowPopupAsync(new PopupP(_ticket));
            _ticket.Ammount = ticket.Ammount;
            if(_ticket.Ammount!="")
            _ticketRepo.AddTicket(_ticket);
            await Navigation.PushAsync(new UserP(_ticket));
            
        }
        private void TapVideo_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlayerP(_movie.TrailorUrl, _user.Id));
        }
    }
}