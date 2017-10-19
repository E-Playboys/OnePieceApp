using System;
using Xamarin.Forms;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager;

namespace OnePiece.App.Views.Templates
{
    public partial class ViewPlayerView : ContentView
    {
        private IPlaybackController PlaybackController => CrossMediaManager.Current.PlaybackController;

        public ViewPlayerView()
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
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
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
