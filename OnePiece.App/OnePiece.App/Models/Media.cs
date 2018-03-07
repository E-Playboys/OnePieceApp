using Prism.Mvvm;
using Xamarin.Forms;

namespace OnePiece.App.Models
{
    public class Media : BindableBase
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }

        public MediaType MediaType { get; set; }
    }

    public enum MediaType
    {
        Image = 1, Gif = 2, Video = 3
    }
}
