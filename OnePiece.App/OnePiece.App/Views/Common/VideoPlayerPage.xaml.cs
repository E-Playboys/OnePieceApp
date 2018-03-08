using OnePiece.App.Services;
using Plugin.DeviceInfo;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var hardwareInfo = CrossDevice.Hardware;
            if (width > height)
            {
                VideoView.HeightRequest = hardwareInfo.ScreenWidth;
                VideoView.WidthRequest = hardwareInfo.ScreenHeight;
                DependencyService.Get<IStatusBar>().HideStatusBar();
            }
            else
            {
                VideoView.HeightRequest = (int)(width * 9 / 16);
                VideoView.WidthRequest = hardwareInfo.ScreenWidth;
                DependencyService.Get<IStatusBar>().ShowStatusBar();
            }
        }
    }
}
