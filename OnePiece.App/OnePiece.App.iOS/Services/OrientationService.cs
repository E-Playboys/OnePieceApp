using OnePiece.App.Services;
using OnePiece.App.iOS.Services;
using Xamarin.Forms;
using UIKit;
using Foundation;

[assembly: Dependency(typeof(OrientationService))]
namespace OnePiece.App.iOS.Services
{
    public class OrientationService : IOrientationService
    {
        public void Landscape()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
        }

        public void Portrait()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
        }
    }
}