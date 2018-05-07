using OnePiece.App.Services;
using Plugin.DeviceInfo;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;

namespace OnePiece.App.Views
{
    public partial class VideoPlayerPage : PopupPage
    {

        public VideoPlayerPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VideoView.PlaybackController.Play();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                VideoView.HeightRequest = CrossDevice.Device.ScreenWidth;
                VideoView.WidthRequest = CrossDevice.Device.ScreenHeight;
                DependencyService.Get<IStatusBar>().HideStatusBar();
            }
            else
            {
                VideoView.HeightRequest = (int)(width * 9 / 16);
                VideoView.WidthRequest = CrossDevice.Device.ScreenWidth;
                DependencyService.Get<IStatusBar>().ShowStatusBar();
            }
        }
    }
}
