using Plugin.DeviceInfo;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OnePiece.App.Views
{
    public partial class VideoPlayerPage : ContentPage
    {
        private IPlaybackController PlaybackController => CrossMediaManager.Current.PlaybackController;

        public VideoPlayerPage()
        {
            InitializeComponent();

            CrossMediaManager.Current.PlayingChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressBar.Maximum = e.Duration.TotalSeconds;
                    ProgressBar.Value = e.Position.TotalSeconds;
                    Duration.Text = e.Duration.ToString(@"hh\:mm\:ss");
                    Position.Text = e.Position.ToString(@"hh\:mm\:ss");
                });
            };
        }

        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);
        //    var hardwareInfo = DependencyService.Get<IHardwareInfo>();
        //    if (width > height)
        //    {
        //        VideoView.HeightRequest = hardwareInfo.ScreenWidth;
        //        VideoView.WidthRequest = hardwareInfo.ScreenHeight;
        //    }
        //    else
        //    {
        //        VideoView.HeightRequest = (int)(width * 9 / 16);
        //        VideoView.WidthRequest = hardwareInfo.ScreenWidth;
        //    }
        //}

        protected override void OnAppearing()
        {
            PlaybackController.Play();
        }

        private void PlayClicked(object sender, EventArgs e)
        {
            PlaybackController.Play();
        }

        private void PauseClicked(object sender, EventArgs e)
        {
            PlaybackController.Pause();
        }

        private void StopClicked(object sender, EventArgs e)
        {
            PlaybackController.Stop();
        }

        private void ProgressBar_TouchDown(object sender, FocusEventArgs e)
        {
            //PlaybackController.Pause();
        }

        private void ProgressBar_TouchUp(object sender, FocusEventArgs e)
        {
            var value = ProgressBar.Value;
            Position.Text = value.ToString(@"hh\:mm\:ss");
            PlaybackController.SeekTo(value);
        }
    }
}
