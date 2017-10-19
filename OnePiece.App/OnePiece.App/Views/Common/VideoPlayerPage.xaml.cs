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

        public VideoPlayerPage()
        {
            InitializeComponent();
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
    }
}
