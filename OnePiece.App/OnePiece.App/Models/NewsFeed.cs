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

        private ObservableCollection<Media> _medias;
        public ObservableCollection<Media> Medias
        {
            get { return _medias; }
            set { SetProperty(ref _medias, value); }
        }
    }
}
