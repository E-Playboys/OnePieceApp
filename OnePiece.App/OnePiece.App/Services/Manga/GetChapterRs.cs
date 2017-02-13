using OnePiece.App.Models;

namespace OnePiece.App.Services.Manga
{
    public class GetChapterRs
    {
        public MangaChapter Chapter { get; set; }
        public int? NextChapterId { get; set; }
        public int? PrevChapterId { get; set; }
    }
}
