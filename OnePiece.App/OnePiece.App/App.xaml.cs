using Microsoft.Practices.Unity;
using OnePiece.App.Controls;
using OnePiece.App.Services;
using OnePiece.App.Services.Manga;
using OnePiece.App.Views;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            var url = $"{nameof(LeftMenu)}/{nameof(Views.MainPage)}/{nameof(TabNavigationPage)}/{nameof(NewsFeedPage)}";
            NavigationService.NavigateAsync(url);
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<TabbedPage>();
            Container.RegisterTypeForNavigation<MasterDetailPage>();

            Container.RegisterTypeForNavigation<LeftMenu>();
            Container.RegisterTypeForNavigation<TabNavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<NewsFeedPage>();
            Container.RegisterTypeForNavigation<AnimePage>();
            Container.RegisterTypeForNavigation<MangaPage>();
            Container.RegisterTypeForNavigation<VideoPage>();
            Container.RegisterTypeForNavigation<VideoPlayerPage>();

            Container.RegisterType<IAppService, AppService>();
            Container.RegisterType<IMangaService, MangaService>();
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
