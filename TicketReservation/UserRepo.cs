using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            // lưu uid mới tạo vào db
            var uid = token.User.LocalId;
            await firebaseClient.Child(nameof(User)).Child(uid).PutAsync(JsonConvert.SerializeObject(user));
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
        public async Task<User> GetInfoById()
        {
            string id="";
            var savedfirebaseauth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
            var refreshedcontent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
            Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refreshedcontent));
            id = savedfirebaseauth.User.LocalId;
            return (await firebaseClient.Child(nameof(User) + "/" + id).OnceSingleAsync<User>());
        }
    }

}
