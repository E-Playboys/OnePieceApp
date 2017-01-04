using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace OnePiece.App.Models
{
    public class NewsFeed : BindableBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private ObservableCollection<Gif> _gifs;
        public ObservableCollection<Gif> Gifs
        {
            get { return _gifs; }
            set { SetProperty(ref _gifs, value); }
        }
    }
}
