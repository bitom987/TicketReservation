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
    public partial class ForgotPasswordP : ContentPage
    {
        UserRepo _userRepo = new UserRepo();
        public ForgotPasswordP()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginP());
        }
        private async void SendLinkBtnClicked(object sender, EventArgs e)
        {
            string email = EntEmail.Text;
            User user = new User();
            user.Email = email;
            if(string.IsNullOrEmpty(email) )
            {
                await DisplayAlert("Thông báo", "Vui lòng nhập email", "Ok!");
                return;
            }
            bool isSend = await _userRepo.ResetPassword(user);
            if(isSend)
            {
                await DisplayAlert("Reset Password", "Gửi link reset password thành công, vui lòng kiểm tra email", "Ok!");
                return;
            }
            await DisplayAlert("Reset Password", "Gửi link reset password thất bại, vui lòng quay lại sau ít phút", "Ok!");
        }
    }
}