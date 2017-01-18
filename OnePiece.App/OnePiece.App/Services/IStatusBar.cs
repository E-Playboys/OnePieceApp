namespace OnePiece.App.Services
{
    // NOTE: For iOS, you need to update the Info.plist file to allow the application to change the status bar visibility.
    public interface IStatusBar
    {
        void HideStatusBar();
        void ShowStatusBar();
    }
}
