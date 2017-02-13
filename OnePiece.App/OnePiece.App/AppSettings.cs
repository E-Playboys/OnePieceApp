namespace OnePiece.App
{
    public class AppSettings
    {
#if DEBUG
        public const string WEB_API_URL = "http://192.168.1.5:5000/api/";
#else
        public const string WEB_API_URL = "";
#endif
    }
}
