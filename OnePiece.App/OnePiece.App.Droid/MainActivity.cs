using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Felipecsl.GifImageViewLibrary;
using FormsPlugin.Iconize.Droid;
using Microsoft.Practices.Unity;
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
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            FFImageLoading.Forms.Droid.CachedImageRenderer.Init();
            IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);
            Plugin.Iconize.Iconize
                .With(new MaterialModule())
                .With(new TypiconsModule());
            GifImageViewRenderer.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));

            if (!Resolver.IsSet)
            {
                this.SetIoc();
            }
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

