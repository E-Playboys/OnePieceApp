using System.Collections.Generic;

namespace OnePiece.Web.Data.Entities
{
    public class Anime : BaseEntity
    {
        public int EpisodeNumber { get; set; }

        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public decimal ImdbScore { get; set; }

        public decimal Rating { get; set; }

        public int ViewCount { get; set; }

        public AnimeType Type { get; set; }

        public List<Media> Medias { get; set; }

        public int SeasonId { get; set; }

        public Season Season { get; set; }
    }

    public enum AnimeType
    {
        Story,
        TvSpecial,
        Movie
    }
}
