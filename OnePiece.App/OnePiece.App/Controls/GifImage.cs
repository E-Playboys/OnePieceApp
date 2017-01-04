using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public class GifImage : ContentView
    {
        public static readonly BindableProperty UrlProperty =
            BindableProperty.Create(nameof(Url), typeof(string), typeof(GifImage), string.Empty);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }
    }
}
