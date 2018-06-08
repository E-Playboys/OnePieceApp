using System;
using Xamarin.Forms;
using OnePiece.App.Services;
using OnePiece.App.DataModels;
using System.Runtime.CompilerServices;
using OnePiece.App.Controls.VideoLibrary;

namespace OnePiece.App.Views.Templates
{
    public partial class VideoPlayerView : ContentView
    {
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
                VideoPlayer.Source = VideoSource.FromUri(Anime.Links);
            }
        }

        private void PlayPauseClicked(object sender, EventArgs e)
        {
            if (VideoPlayer.Status == VideoStatus.Playing)
            {
                VideoPlayer.Pause();
            }
            else if (VideoPlayer.Status == VideoStatus.Paused)
            {
                VideoPlayer.Play();
            }
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

        private void ToggleFullScreenClicked(object sender, EventArgs e)
        {
            IsFullScreen = !IsFullScreen;
        }

        private void VideoView_Tapped(object sender, EventArgs e)
        {
            VideoControl.IsVisible = !VideoControl.IsVisible;
        }
    }
}
