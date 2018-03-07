using System;
using Xamarin.Forms;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using OnePiece.App.Services;

namespace OnePiece.App.Views.Templates
{
    public partial class VideoPlayerView : ContentView
    {
        private IPlaybackController PlaybackController => CrossMediaManager.Current.PlaybackController;

        public static readonly BindableProperty IsFullScreenProperty =
  BindableProperty.Create("IsFullScreen", typeof(bool), typeof(VideoPlayerView), false, BindingMode.TwoWay);

        public bool IsFullScreen
        {
            get { return (bool)GetValue(IsFullScreenProperty); }
            set { SetValue(IsFullScreenProperty, value); }
        }

        public VideoPlayerView()
        {
            InitializeComponent();

            CrossMediaManager.Current.PlayingChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (e.Progress > 0)
                    {
                        ProgressBar.Maximum = e.Duration.TotalSeconds;
                        ProgressBar.Value = e.Position.TotalSeconds;
                        double durationSeconds = e.Duration.TotalSeconds / 1000;
                        double positionSeconds = e.Position.TotalSeconds / 1000;
                        string format = durationSeconds >= 3600 ? @"h\:mm\:ss" : @"mm\:ss";
                        Duration.Text = TimeSpan.FromSeconds(durationSeconds).ToString(format);
                        Position.Text = TimeSpan.FromSeconds(positionSeconds).ToString(format);
                    }
                });
            };

            CrossMediaManager.Current.StatusChanged += (sender, e) =>
            {
                if(e.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Playing)
                {
                    PauseButton.IsVisible = true;
                    PlayButton.IsVisible = false;
                }
                else
                {
                    PauseButton.IsVisible = false;
                    PlayButton.IsVisible = true;
                }
            };

            Main.BindingContext = this;
            videoView.Source = "https://player.vimeo.com/external/258928251.hd.mp4?s=f5fe5d897c45c49807ad1be2ef3489d0a26f0455&profile_id=174";
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            PlaybackController.Play();
        }

        private void PlayClicked(object sender, EventArgs e)
        {
            PlaybackController.PlayPause();
        }

        private void PauseClicked(object sender, EventArgs e)
        {
            PlaybackController.PlayPause();
        }

        private void FullScreenClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IOrientationService>().Landscape();
            DependencyService.Get<IStatusBar>().HideStatusBar();
            IsFullScreen = true;
        }

        private void FullScreenExitClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IOrientationService>().Portrait();
            DependencyService.Get<IStatusBar>().ShowStatusBar();
            IsFullScreen = false;
        }

        private void ProgressBar_TouchDown(object sender, FocusEventArgs e)
        {
            //PlaybackController.Pause();
        }

        private void ProgressBar_TouchUp(object sender, FocusEventArgs e)
        {
            var value = ProgressBar.Value / 1000;
            //string format = value >= 3600 ? @"h\:mm\:ss" : @"mm\:ss";
            //Position.Text = value.ToString(format);
            PlaybackController.SeekTo(value);
        }
    }
}
