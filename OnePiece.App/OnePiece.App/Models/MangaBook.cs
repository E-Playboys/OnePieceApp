using System.Collections.ObjectModel;

namespace OnePiece.App.Models
{
    public class MangaBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public ObservableCollection<MangaPage> Pages { get; set; } = new ObservableCollection<MangaPage>();
    }
}
