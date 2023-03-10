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
	public partial class LoginP : ContentPage
	{
        UserRepo _userRepo = new UserRepo();
		public LoginP ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void LoginBtnClicked(object sender, EventArgs e)
        {
            try
            {
                string email = EntEmail.Text;
                string password = EntPassword.Text;
                User user = new User();
                user.Email = email;
                user.Password = password;
                string token = await _userRepo.SignIn(user);
                if (!string.IsNullOrEmpty(token))
                {
                    await Navigation.PushAsync(new MoviesListP(user.Id));
                }
                else
                {
                    await DisplayAlert("Thông báo", "Đăng nhập thất bại, vui lòng quay lại sau vài phút", "Ok");
                }
                if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Thông báo", "Kiểm tra lại thông tin còn để trống", "Ok!");
                    return;
                }
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("Thông báo", "Email không tồn tại", "Ok");
                }
                else if (ex.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("Thông báo", "Mật khẩu không đúng", "Ok");
                }
                else
                {
                    await DisplayAlert("Lỗi", "Đã xảy ra lỗi bất ngờ, vui lòng quay lại sau vài phút", "Ok");
                }
            }
            
        }
        private void ForgetPassBtn_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordP());
        }
        private void SignupBtn_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignupP());
        }
        bool toogle = true;
        private void ShowPasswordCheckedChanged(object sender, EventArgs e)
        {
            if (toogle)
            {
                EntPassword.IsPassword = false;
                toogle= false;
            }
            else
            {
                EntPassword.IsPassword = true;
                toogle= true;
            }

        }
    }
}