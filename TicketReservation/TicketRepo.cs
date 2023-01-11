using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Models;
using Xamarin.Essentials;


namespace TicketReservation
{
    public class TicketRepo
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://reservationticketdb-default-rtdb.asia-southeast1.firebasedatabase.app/");

        public async void AddTicket(Ticket ticket)
        {
            await firebaseClient.Child(ticket.UserId).Child(ticket.MoviesId).PutAsync(JsonConvert.SerializeObject(ticket));
        }
        public async void UpdateTicket(Ticket ticket)
        {
            await firebaseClient.Child(ticket.UserId).Child(ticket.MoviesId).PutAsync(JsonConvert.SerializeObject(ticket));
        }
        public async void DeleteTicket(Ticket ticket)
        {
            await firebaseClient.Child(ticket.UserId).Child(ticket.MoviesId).DeleteAsync();
        }
        
        public async Task<List<Ticket>> GetAllTickets(Ticket ticket)
        {
            return(await firebaseClient
                .Child(ticket.UserId)
                .OnceAsync<Ticket>()).Select(t => new Ticket
                {
                    UserId = t.Object.UserId,
                    MoviesId = t.Object.MoviesId,
                    Ammount = t.Object.Ammount,
                }).ToList();
        }
    }
}
