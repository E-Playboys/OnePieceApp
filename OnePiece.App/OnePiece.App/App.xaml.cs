﻿using OnePiece.App.Controls;
using OnePiece.App.Services;
using OnePiece.App.Services.Manga;
using OnePiece.App.Views;
using Plugin.DeviceInfo;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Ioc;
using Prism;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OnePiece.App
{
    public partial class App : PrismApplication
    {
        static public int ScreenWidth;

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            MainPage = new LeftMenu();
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TabbedPage>();
            containerRegistry.RegisterForNavigation<MasterDetailPage>();

            containerRegistry.RegisterForNavigation<LeftMenu>();
            containerRegistry.RegisterForNavigation<TabNavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<NewsFeedPage>();
            containerRegistry.RegisterForNavigation<AnimePage>();
            containerRegistry.RegisterForNavigation<MangaPage>();
            containerRegistry.RegisterForNavigation<VideoPage>();
            containerRegistry.RegisterForNavigation<VideoPlayerPage>();

            containerRegistry.Register<IAppService, AppService>();
            containerRegistry.Register<IMangaService, MangaService>();
            containerRegistry.Register<IHardwareInfo, HardwareInfo>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
