using OnePiece.App.Controls;
using OnePiece.App.Services;
using OnePiece.App.Services.Manga;
using OnePiece.App.Views;
using Plugin.DeviceInfo;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Ioc;
using Prism;
using FormsPlugin.Iconize;
using OnePiece.App.DataServices;
using OnePiece.App.DataServices.Anime;
using OnePiece.App.DataServices.NewsFeed;
using OnePiece.App.DataServices.Season;

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

            MainPage = new IconNavigationPage(new MainPage());
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TabbedPage>();
            containerRegistry.RegisterForNavigation<MasterDetailPage>();

            containerRegistry.RegisterForNavigation<LeftMenu>();
            containerRegistry.RegisterForNavigation<IconNavigationPage>();
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
            containerRegistry.Register<IRequestProvider, RequestProvider>();
            containerRegistry.Register<INewsFeedApiService, NewsFeedApiService>();
            containerRegistry.Register<ISeasonApiService, SeasonApiService>();
            containerRegistry.Register<IAnimeApiService, AnimeApiService>();
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
