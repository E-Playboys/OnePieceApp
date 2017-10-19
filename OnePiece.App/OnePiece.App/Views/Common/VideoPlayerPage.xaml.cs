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
                    if(e.Progress > 0)
                    {
                        ProgressBar.Maximum = e.Duration.TotalSeconds;
                        ProgressBar.Value = e.Position.TotalSeconds;
                        double durationSeconds = e.Duration.TotalSeconds / 1000;
                        double positionSeconds = e.Position.TotalSeconds / 1000;
                        Duration.Text = TimeSpan.FromSeconds(durationSeconds).ToString(@"h\:mm\:ss");
                        Position.Text = TimeSpan.FromSeconds(positionSeconds).ToString(@"h\:mm\:ss");
                    }
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
            PauseButton.IsVisible = true;
            PlayButton.IsVisible = false;
            PlaybackController.Play();
        }

        private void PlayClicked(object sender, EventArgs e)
        {
            PauseButton.IsVisible = true;
            PlayButton.IsVisible = false;
            PlaybackController.Play();
        }

        private void PauseClicked(object sender, EventArgs e)
        {
            PauseButton.IsVisible = false;
            PlayButton.IsVisible = true;
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
            var value = ProgressBar.Value / 1000;
            Position.Text = value.ToString(@"%h\:mm\:ss");
            PlaybackController.SeekTo(value);
        }
    }
}
