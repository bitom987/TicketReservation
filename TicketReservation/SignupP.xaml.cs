using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupP : ContentPage
	{
        UserRepo _userRepo = new UserRepo();
		public SignupP ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginP());
        }
        private async void SignupBtnClicked(object sender, EventArgs e)
        {
            try
            {
                string email = EntEmail.Text;
                string password = EntPassword.Text;
                string confpassword = EntConfirmPassword.Text;
                if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Thông báo", "Kiểm tra lại thông tin còn để trống", "Ok!");
                    return;
                }
                if (password != confpassword)
                {
                    await DisplayAlert("Thông báo", "Mật khẩu không trùng khớp", "Nhập lại");
                    return;
                }
                if (password.Length < 6 || confpassword.Length < 6)
                {
                    await DisplayAlert("Thông báo", "Mật khẩu tối thiểu 6 ký tự", "Nhập lại");
                    return;
                }
                bool IsSave = await _userRepo.Register(email, password);
                await DisplayAlert("Thông báo", "This:" + IsSave, "Ok");
                if (IsSave)
                {
                    await DisplayAlert("Thông báo", "Đăng ký thành công", "Đăng nhập");
                    await Navigation.PushAsync(new LoginP());
                }
                else
                {
                    await DisplayAlert("Thông báo", "Thất bại, vui lòng thử lại sau", "Ok!");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Thông báo", "Email sử dụng đăng ký đã tồn tại", "Ok!");
                }
                else
                {
                    await DisplayAlert("Lỗi", ex.Message, "Ok!");
                }
            }
            
        
        }
    }
}