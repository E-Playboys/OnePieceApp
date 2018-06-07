using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataModels
{
    public class NewsFeed
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public int ColumnCount { get; set; }

        public List<Media> Medias { get; set; }

        public LinkTo LinkTo { get; set; }

        public int LinkToId { get; set; }
    }

    public enum LinkTo
    {
        Anime,
        Manga,
        Season
    }
}
