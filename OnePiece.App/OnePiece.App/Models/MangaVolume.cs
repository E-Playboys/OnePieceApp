using System;
using System.Collections.ObjectModel;

namespace OnePiece.App.Models
{
    public class MangaVolume
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public ObservableCollection<MangaChapter> MangaChapters { get; set; }
    }
}
