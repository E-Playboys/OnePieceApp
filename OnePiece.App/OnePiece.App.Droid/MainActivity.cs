using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Felipecsl.GifImageViewLibrary;
using FormsPlugin.Iconize.Droid;
using Microsoft.Practices.Unity;
using OnePiece.App.Droid.Helpers;
using OnePiece.App.Droid.Renderers;
using Plugin.Iconize.Fonts;
using Prism.Unity;
using XLabs.Ioc;
using XLabs.Forms;
using XLabs.Platform.Device;

namespace OnePiece.App.Droid
{
    [Activity(Label = "OnePiece.App", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            UserDialogs.Init(this);
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init();
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
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

