namespace OnePiece.App
{
    public class AppSettings
    {
#if DEBUG
        public const string WEB_API_URL = "http://10.0.2.2:1412/api";
#else
        public const string WEB_API_URL = "";
#endif
    }
}
