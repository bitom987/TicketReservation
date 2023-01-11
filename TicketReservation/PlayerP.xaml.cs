using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace TicketReservation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerP : ContentPage
    {
        User _user = new User();
        public PlayerP(string videoUrl,string UserId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            PlayVideo(videoUrl);
            _user.Id= UserId;
        }

        private async void PlayVideo(string videoUrl)
        {
            var youtube = new YoutubeClient();

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

            if (streamInfo != null)
            {
                // Get the actual stream
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                AILoader.IsVisible = false;
                AILoader.IsRunning = false;
                MediaElementVideo.Source = streamInfo.Url;
            }
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}