using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataModels
{
    public class Manga
    {
        public int Id { get; set; }

        public int ChapterNumber { get; set; }

        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public decimal Rating { get; set; }

        public int ViewCount { get; set; }

        public string Poster { get; set; }

        public MangaType Type { get; set; }

        public List<Media> Medias { get; set; }
    }

    public enum MangaType
    {
        BlackWhite,
        Color
    }
}
