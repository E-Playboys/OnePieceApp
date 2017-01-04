using Prism.Mvvm;
using Xamarin.Forms;

namespace OnePiece.App.Models
{
    public class Gif : BindableBase
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }
    }
}
