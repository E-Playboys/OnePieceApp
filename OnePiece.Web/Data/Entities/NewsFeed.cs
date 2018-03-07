using System.Collections.Generic;

namespace OnePiece.Web.Data.Entities
{
    public class NewsFeed : BaseEntity
    {
        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public List<Media> Medias { get; set; }
    }
}
