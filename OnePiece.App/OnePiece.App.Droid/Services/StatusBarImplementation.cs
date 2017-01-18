using Android.App;
using Android.Views;
using OnePiece.App.Services;
using OnePiece.App.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarImplementation))]
namespace OnePiece.App.Droid.Services
{
    public class StatusBarImplementation : IStatusBar
    {
        public StatusBarImplementation()
        {
        }

        #region IStatusBar implementation

        public void HideStatusBar()
        {
            var activity = (Activity)Forms.Context;
            activity.Window.AddFlags(WindowManagerFlags.Fullscreen);
        }

        public void ShowStatusBar()
        {
            var activity = (Activity)Forms.Context;
            activity.Window.ClearFlags(WindowManagerFlags.Fullscreen);
        }

        #endregion
    }
}