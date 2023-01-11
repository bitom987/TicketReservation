using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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
        public async Task<bool> Register(User user)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(user.Email, user.Password);
            // lưu uid làm key - Email làm value vào db
            var uid = token.User.LocalId;
            // Khởi tạo dữ liệu cho database
            await firebaseClient.Child("User").Child(uid).Child("Email").PutAsync(JsonConvert.SerializeObject(user.Email));
            // lưu email uid
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;
        }
        public async Task<string> SignIn(User user)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(user.Email, user.Password);
            var uid = token.User.LocalId;
            user.Id = uid;
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            return "";
        }
        public async Task<bool> ResetPassword(User user)
        {
            await authProvider.SendPasswordResetEmailAsync(user.Email);
            return true;
        }
        public async Task<User> GetInfoById(User user)
        {
            return (await firebaseClient.Child(nameof(User) + "/" + user.Id).OnceSingleAsync<User>());
        }
    }

}
