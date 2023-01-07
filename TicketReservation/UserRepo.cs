using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservation
{
    public class UserRepo
    {
        static string webAPIkey = "AIzaSyCyu9J60VuXysq91ovNQRHJh9SwEfBJxpo";
        FirebaseAuthProvider authProvider;
        FirebaseClient firebaseClient = new FirebaseClient("https://reservationticketdb-default-rtdb.asia-southeast1.firebasedatabase.app/");
        public UserRepo()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));
        }
        public async Task<bool> Register(string email, string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            // lưu uid mới tạo vào db
            var uid = token.User.LocalId;
            await firebaseClient.Child(nameof(User)).Child(uid).PutAsync(JsonConvert.ToString(email));
            // check uid đó còn tồn tại trong Auth không
            // lưu email uid
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {   
                return true;
            }
            return false; 
        }   
        public async Task<string> SignIn(string email, string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            return "";
        }
    }

}
