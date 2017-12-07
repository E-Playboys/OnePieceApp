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
                        Duration.Text = TimeSpan.FromSeconds(durationSeconds).ToString(@"h\:mm\:ss");
                        Position.Text = TimeSpan.FromSeconds(positionSeconds).ToString(@"h\:mm\:ss");
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
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
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
            Position.Text = value.ToString(@"%h\:mm\:ss");
            PlaybackController.SeekTo(value);
        }
    }
}
