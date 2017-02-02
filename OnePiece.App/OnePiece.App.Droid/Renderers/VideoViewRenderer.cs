using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using OnePiece.App.Controls;
using OnePiece.App.Droid.Helpers;
using OnePiece.App.Droid.Renderers;
using Rox;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Orientation = Android.Content.Res.Orientation;
using VideoView = OnePiece.App.Controls.VideoView;

[assembly: ExportRenderer(typeof(VideoView), typeof(VideoViewRenderer))]
namespace OnePiece.App.Droid.Renderers
{
    public class VideoViewRenderer : ViewRenderer<VideoView, FrameLayout>
    {
        private Android.Widget.VideoView _nativeVideoView;
        private VideoView _videoView;
        private MediaController _mediaController;

        private bool _isShowingController;

        protected override async void OnElementChanged(ElementChangedEventArgs<VideoView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            InitializeView();

            _videoView = e.NewElement;
            _nativeVideoView = (Android.Widget.VideoView)Control.GetChildAt(0);
            _mediaController = new MediaController(_nativeVideoView.Context);
            _nativeVideoView.SetMediaController(_mediaController);
            _nativeVideoView.SetOnPreparedListener(new VideoViewOnPreparedListener());

            await SetSourceAsync(_videoView);

            if (_videoView.AutoPlay)
            {
                UserDialogs.Instance.ShowLoading("Buffering ...", MaskType.None);
                _nativeVideoView.Start();
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                if (_isShowingController)
                {
                    _mediaController.Hide();
                    _isShowingController = false;
                }
                else
                {
                    _mediaController.Show();
                    _isShowingController = true;
                }
            }

            return base.OnTouchEvent(e);
        }

        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            if (newConfig.Orientation == Orientation.Landscape)
            {
                var uiOptions = (int)SystemUiFlags.LayoutStable;
                uiOptions |= (int)SystemUiFlags.LayoutHideNavigation;
                uiOptions |= (int)SystemUiFlags.LayoutFullscreen;
                uiOptions |= (int)SystemUiFlags.HideNavigation;
                uiOptions |= (int)SystemUiFlags.Fullscreen;
                uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

                StatusBarHelper.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
                //StatusBarHelper.AppActionBar?.Hide();
            }
            else
            {
                var uiOptions = (int)SystemUiFlags.LayoutStable;
                uiOptions |= (int)SystemUiFlags.LayoutFullscreen;
                uiOptions |= (int)SystemUiFlags.Fullscreen;
                StatusBarHelper.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
                //StatusBarHelper.AppActionBar?.Show();
            }
        }

        private void InitializeView()
        {
            FrameLayout control = Control;
            if (control == null)
            {
                control = new FrameLayout(this.Context)
                {
                    LayoutParameters = new FrameLayout.LayoutParams(-1, -1)
                    {
                        Gravity = GravityFlags.Fill
                    }
                };

                Android.Widget.VideoView videoView = new Android.Widget.VideoView(this.Context)
                {
                    LayoutParameters = new FrameLayout.LayoutParams(-1, -1)
                    {
                        Gravity = GravityFlags.Center
                    }
                };

                control.AddView(videoView);
                SetNativeControl(control);
            }
        }

        private async Task SetSourceAsync(VideoView videoView)
        {
            try
            {
                ImageSource source = videoView.Source;
                if (source is UriImageSource)
                {
                    object obj = source.GetValue(UriImageSource.UriProperty);
                    if (obj != null && (object)(obj as System.Uri) != null)
                    {
                        _nativeVideoView.SetVideoURI(Android.Net.Uri.Parse(((System.Uri)obj).ToString()));
                    }
                }
                else if (source is FileImageSource)
                {
                    object obj = source.GetValue(FileImageSource.FileProperty);
                    if (obj != null && obj is string)
                    {
                        _nativeVideoView.SetVideoPath((string)obj);
                    }
                }
                else if (source is StreamImageSource)
                {
                    object obj = source.GetValue(StreamImageSource.StreamProperty);
                    if (obj != null && obj is Func<CancellationToken, Task<System.IO.Stream>>)
                    {
                        System.IO.Stream stream = await ((Func<CancellationToken, Task<System.IO.Stream>>)obj)(new CancellationToken());

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            MemoryStream memoryStream1 = memoryStream;
                            await stream.CopyToAsync(memoryStream1);
                            memoryStream.Position = 0L;
                            Java.IO.File file = Java.IO.File.CreateTempFile("Temp", "mp4");
                            file.DeleteOnExit();

                            using (FileInputStream fileInputStream = new FileInputStream(file))
                            {
                                int num = await fileInputStream.ReadAsync(memoryStream.ToArray());
                                _nativeVideoView.SetVideoURI(Android.Net.Uri.FromFile(file));
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        class VideoViewOnPreparedListener : Java.Lang.Object, MediaPlayer.IOnPreparedListener
        {
            public void OnPrepared(MediaPlayer mp)
            {
                mp.SetOnInfoListener(new VideoViewOnInfoListener());
                mp.SetOnErrorListener(new VideoViewOnErrorListener());
            }

            class VideoViewOnInfoListener : Java.Lang.Object, MediaPlayer.IOnInfoListener
            {
                public bool OnInfo(MediaPlayer mp, MediaInfo what, int extra)
                {
                    if (what == MediaInfo.BufferingStart)
                    {
                        UserDialogs.Instance.ShowLoading("Buffering ...", MaskType.None);
                    }

                    if (what == MediaInfo.BufferingEnd)
                    {
                        UserDialogs.Instance.HideLoading();
                    }

                    return false;
                }
            }

            class VideoViewOnErrorListener : Java.Lang.Object, MediaPlayer.IOnErrorListener
            {
                public bool OnError(MediaPlayer mp, MediaError what, int extra)
                {
                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.AlertAsync("Error loading video!", okText: "OK");
                    return false;
                }
            }

            // THIS IS UNSTABLE
            //class VideoViewOnBufferingUpdateListener : Java.Lang.Object, MediaPlayer.IOnBufferingUpdateListener
            //{
            //    private int _lastBufferedPercent;

            //    public void OnBufferingUpdate(MediaPlayer mp, int percent)
            //    {
            //        if (percent == 100)
            //        {
            //            UserDialogs.Instance.HideLoading();
            //        }
            //        else
            //        {
            //            if (_lastBufferedPercent != percent)
            //            {
            //                _lastBufferedPercent = percent;
            //                UserDialogs.Instance.ShowLoading($"Buffering {percent}% ...");
            //            }
            //        }
            //    }
            //}
        }
    }
}