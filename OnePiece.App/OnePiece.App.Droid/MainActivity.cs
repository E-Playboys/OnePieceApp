using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using FormsPlugin.Iconize.Droid;
using OnePiece.App.Droid.Helpers;
using OnePiece.App.Droid.Renderers;
using Plugin.Iconize.Fonts;
using XLabs.Ioc;
using XLabs.Platform.Device;
using Prism;
using Prism.Ioc;
using System.Threading.Tasks;
using Android.Content;

namespace OnePiece.App.Droid
{
    [Activity(Label = "OnePiece.Wiki", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Current = this;

            App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            UserDialogs.Init(this);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);
            Plugin.Iconize.Iconize
                .With(new MaterialModule())
                .With(new TypiconsModule());
            GifImageViewRenderer.Init();

            //Set our status bar helper DecorView. This enables us to hide the notification bar for fullscreen
            StatusBarHelper.DecorView = Window.DecorView;
            
            int statusBarResourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (statusBarResourceId > 0)
            {
                StatusBarHelper.StatusBarHeight = Resources.GetDimensionPixelSize(statusBarResourceId);
            }

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));

            if (!Resolver.IsSet)
            {
                this.SetIoc();
            }
        }

        // Field, properties, and method for Video Picker
        public static MainActivity Current { private set; get; }

        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<string> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    // Set the filename as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(data.DataString);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            //Set our status bar helper AppActionBar. This enables us to hide the ActionBar for fullscreen
            //Always hide the actionbar/toolbar if you are hiding the notification bar
            if (ActionBar != null)
                StatusBarHelper.AppActionBar = ActionBar;
        }

        private void SetIoc()
        {
            var resolverContainer = new SimpleContainer();

            resolverContainer.Register(t => AndroidDevice.CurrentDevice)
                .Register(t => t.Resolve<IDevice>().Display);

            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}

