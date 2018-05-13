namespace OnePiece.App
{
    public class AppSettings
    {
#if DEBUG
        public const string WEB_API_URL = "http://192.168.8.106:1412/api";
#else
        public const string WEB_API_URL = "";
#endif
    }
}
