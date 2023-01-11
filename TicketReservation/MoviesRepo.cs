using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TicketReservation
{
    public class MoviesRepo
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://reservationticketdb-default-rtdb.asia-southeast1.firebasedatabase.app/");
        
        public async Task<List<Movies>> GetAllMovies()
        {
            return (await firebaseClient
                .Child("Movies")
                .OnceAsync<Movies>()).Select(item=>new Movies
            {
                Description = item.Object.Description,
                Duration = item.Object.Duration,
                Genre = item.Object.Genre,
                Id = item.Object.Id,
                ImageUrl = item.Object.ImageUrl,
                Language = item.Object.Language,
                Name = item.Object.Name,
                PlayingDate =  item.Object.PlayingDate,
                PlayingTime = item.Object.PlayingTime,  
                Rating= item.Object.Rating,
                TicketPrice = item.Object.TicketPrice,
                TrailorUrl = item.Object.TrailorUrl
            }).ToList();
        }
        public async Task<Movies>GetMovieById(string id)
        {
            return (await firebaseClient
                .Child(("Movies")+"/"+id)
                .OnceSingleAsync<Movies>());
        }
    }
}
