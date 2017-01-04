using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Felipecsl.GifImageViewLibrary;
using OnePiece.App.Controls;
using OnePiece.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GifImage), typeof(GifImageViewRenderer))]
namespace OnePiece.App.Droid.Renderers
{
    public class GifImageViewRenderer : ViewRenderer<GifImage, GifImageView>
    {
        private HttpClient _client = new HttpClient();
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        GifImageView _gif;
        protected override void OnElementChanged(ElementChangedEventArgs<GifImage> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;

            _gif = new GifImageView(Forms.Context);

            SetNativeControl(_gif);
        }

        private bool _loaded;
        private bool _loading;
        private bool _autoPlay;
        protected override async void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            try
            {
                var gifImage = Element;
                if (gifImage != null)
                {
                    var s = gifImage.Url;
                    if (s != null && !_loaded && !_loading)
                    {
                        _loading = true;

                        byte[] bytes = await _client.GetByteArrayAsync(s);
                        if (bytes == null)
                            return;
                        _gif.StopAnimation();
                        _gif.SetBytes(bytes);
                        if (_autoPlay)
                        {
                            _gif.StartAnimation();
                        }

                        _gif.Click += (o, args) =>
                        {
                            if (_gif.IsAnimating)
                            {
                                _gif.StopAnimation();
                            }
                            else
                            {
                                _gif.StartAnimation();
                            }
                        };
                        _loaded = true;
                        _loading = false;
                    }

                    MessagingCenter.Subscribe<GifAppearingMessage>(this, "GifAppearingMessage", message =>
                    {
                        if (!_loaded)
                        {
                            _autoPlay = true;
                            return;
                        }

                        try
                        {
                            var appearingUrls = message.AppearingUrls;
                            if (appearingUrls.Contains(s))
                            {
                                _gif.StartAnimation();
                            }
                            else
                            {
                                _gif.StopAnimation();
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to load gif: " + ex.Message);
            }
        }
    }
}