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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                VideoView.HeightRequest = Acr.DeviceInfo.DeviceInfo.Hardware.ScreenWidth;
                VideoView.WidthRequest = Acr.DeviceInfo.DeviceInfo.Hardware.ScreenHeight;
            }
            else
            {
                VideoView.HeightRequest = 200;
                VideoView.WidthRequest = Acr.DeviceInfo.DeviceInfo.Hardware.ScreenWidth;
                Label.HeightRequest = 0;
            }
        }
    }
}
