using Android.App;
using Android.Views;
using OnePiece.App.Services;
using OnePiece.App.Droid.Services;
using Xamarin.Forms;
using Android.Content.PM;

[assembly: Dependency(typeof(OrientationService))]
namespace OnePiece.App.Droid.Services
{
    public class OrientationService : IOrientationService
    {
        public void Landscape()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Landscape;
        }

        public void Portrait()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Portrait;
        }
    }
}