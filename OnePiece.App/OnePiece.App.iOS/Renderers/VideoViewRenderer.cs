using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AVFoundation;
using AVKit;
using CoreMedia;
using Foundation;
using OnePiece.App.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using VideoView = OnePiece.App.Controls.VideoView;

[assembly: ExportRenderer(typeof(VideoView), typeof(VideoViewRenderer))]
namespace OnePiece.App.iOS.Renderers
{
    public class VideoViewRenderer : ViewRenderer<VideoView, UIView>
    {
        private VideoView _videoView;
        private AVPlayer _avPlayer;
        private AVPlayerViewController _avController;

        protected override async void OnElementChanged(ElementChangedEventArgs<VideoView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            _videoView = e.NewElement;
            _avPlayer = new AVPlayer();
            _avController = new AVPlayerViewController {Player = _avPlayer};
            SetNativeControl(_avController.View);

            await SetSourceAsync(_videoView);

            if (_videoView.AutoPlay)
            {
                _avPlayer.Play();
            }
        }

        private async Task SetSourceAsync(VideoView videoView)
        {
            ImageSource source = videoView.Source;
            try
            {
                if (source is UriImageSource)
                {
                    object obj = source.GetValue(UriImageSource.UriProperty);
                    if (obj != null && (object)(obj as Uri) != null)
                    {
                        var item = new AVPlayerItem(new NSUrl(((Uri) obj).ToString()));
                        _avPlayer.ReplaceCurrentItemWithPlayerItem(item);
                    }
                }
                else if (source is FileImageSource)
                {
                    object obj = source.GetValue(FileImageSource.FileProperty);
                    if (obj != null && obj is string)
                    {
                        var item = new AVPlayerItem(NSUrl.FromFilename((string) obj));
                        _avPlayer.ReplaceCurrentItemWithPlayerItem(item);
                    }
                }
                else if (source is StreamImageSource)
                {
                    object obj = source.GetValue(StreamImageSource.StreamProperty);
                    if (obj != null && obj is Func<CancellationToken, Task<Stream>>)
                    {
                        Stream stream = await ((Func<CancellationToken, Task<Stream>>)obj)(new CancellationToken());
                        var item =  new AVPlayerItem(NSUrl.CreateWithDataRepresentation(NSData.FromStream(stream), (NSUrl) null));
                        _avPlayer.ReplaceCurrentItemWithPlayerItem(item);
                    }
                }
                
            }
            catch
            {
            }
        }
    }
}