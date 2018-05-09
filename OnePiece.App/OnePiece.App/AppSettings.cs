namespace OnePiece.App
{
    public class AppSettings
    {
#if DEBUG
        public const string WEB_API_URL = "http://10.164.0.62:1412/api";
#else
        public const string WEB_API_URL = "";
#endif
    }
}
