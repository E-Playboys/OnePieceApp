using OnePiece.App.DataModels;
using OnePiece.App.Services;
using OnePiece.App.Utilities;
using OnePiece.App.ViewModels;
using Plugin.DeviceInfo;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;

namespace OnePiece.App.Views
{
    public partial class VideoPlayerPage : PopupPage
    {
        private readonly VideoPlayerPageViewModel _context;

        public VideoPlayerPage(Anime anime, string animeType)
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;

            _context = BindingContext as VideoPlayerPageViewModel;
            if (_context != null)
            {
                _context.Anime = anime;
                _context.AnimeType = animeType;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if(_context != null)
            {
                await _context.LoadAsync();
            }
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
