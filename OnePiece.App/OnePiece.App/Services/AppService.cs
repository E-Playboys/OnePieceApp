using Plugin.DeviceInfo;
using Prism.Navigation;

namespace OnePiece.App.Services
{
    public interface IAppService
    {
        INavigationService Navigation { get; }
        IHardwareInfo HardwareInfo { get;  }
    }

    public class AppService : IAppService
    {
        public INavigationService Navigation { get; }
        public IHardwareInfo HardwareInfo { get; }
        public AppService(INavigationService navigationService, IHardwareInfo hardwareInfo)
        {
            Navigation = navigationService;
            HardwareInfo = hardwareInfo;
        }
    }
}
