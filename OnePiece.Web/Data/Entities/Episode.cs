using System.Collections.Generic;

namespace OnePiece.Web.Data.Entities
{
    public class Episode : BaseEntity
    {
        public int No { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal ImdbScore { get; set; }

        public decimal Rating { get; set; }

        public int ViewCount { get; set; }

        public EpisodeType Type { get; set; }

        public List<Photo> Photos { get; set; }

        public List<Video> Videos { get; set; }

        public int SeasonId { get; set; }

        public Season Season { get; set; }
    }

    public enum EpisodeType
    {
        Story,
        TvSpecial,
        Movie
    }
}
