using System;
using Xamarin.Forms;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using OnePiece.App.Services;
using System.Threading;
using Plugin.MediaManager.Forms;
using OnePiece.App.DataModels;
using System.Runtime.CompilerServices;
using Plugin.MediaManager.Abstractions.Enums;

namespace OnePiece.App.Views.Templates
{
    public partial class VideoPlayerView : ContentView
    {
        public IPlaybackController PlaybackController => CrossMediaManager.Current.PlaybackController;

        public static readonly BindableProperty IsFullScreenProperty =
            BindableProperty.Create("IsFullScreen", typeof(bool), typeof(VideoPlayerView), false, BindingMode.TwoWay);

        public bool IsFullScreen
        {
            get { return (bool)GetValue(IsFullScreenProperty); }
            set { SetValue(IsFullScreenProperty, value); }
        }

        public static readonly BindableProperty AnimeProperty =
            BindableProperty.Create("Anime", typeof(Anime), typeof(VideoPlayerView), null, BindingMode.TwoWay);

        public Anime Anime
        {
            get { return (Anime)GetValue(AnimeProperty); }
            set { SetValue(AnimeProperty, value); }
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
                if(e.Status == MediaPlayerStatus.Playing)
                {
                    ActivityIndicator.IsRunning = false;

                    PauseButton.IsVisible = true;
                    PlayButton.IsVisible = false;
                }
                else 
                {
                    if(e.Status == MediaPlayerStatus.Loading || e.Status == MediaPlayerStatus.Buffering)
                    {
                        ActivityIndicator.IsRunning = true;
                    }

                    PauseButton.IsVisible = false;
                    PlayButton.IsVisible = true;
                }
            };
            Main.BindingContext = this;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "IsFullScreen")
            {
                ToggleFullScreen();
            }
            if (propertyName == "Anime")
            {
                VideoView.Source = Anime.Links;
                PlaybackController.Play();
            }
        }

        private void PlayPauseClicked(object sender, EventArgs e)
        {
            PlaybackController.PlayPause();
        }

        private void ToggleFullScreen()
        {
            if (IsFullScreen)
            {
                DependencyService.Get<IOrientationService>().Landscape();
                DependencyService.Get<IStatusBar>().HideStatusBar();
            }
            else
            {
                DependencyService.Get<IOrientationService>().Portrait();
                DependencyService.Get<IStatusBar>().ShowStatusBar();
            }
        }

        private void FullScreenClicked(object sender, EventArgs e)
        {
            IsFullScreen = true;
            ToggleFullScreen();
        }

        private void FullScreenExitClicked(object sender, EventArgs e)
        {
            IsFullScreen = false;
            ToggleFullScreen();
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

        private void VideoView_Tapped(object sender, EventArgs e)
        {
            VideoControl.IsVisible = !VideoControl.IsVisible;
        }
    }
}
