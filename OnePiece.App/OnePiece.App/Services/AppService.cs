using Prism.Navigation;

namespace OnePiece.App.Services
{
    public interface IAppService
    {
        INavigationService Navigation { get; }
    }

    public class AppService : IAppService
    {
        public INavigationService Navigation { get; }
        public AppService(INavigationService navigationService)
        {
            Navigation = navigationService;
        }
    }
}
