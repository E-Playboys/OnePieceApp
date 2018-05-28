using System.Collections.Generic;

namespace OnePiece.Web.Data.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public string Content { get; set; }

        public string ContentEng { get; set; }

        public int? ViewCount { get; set; }

        public string Tags { get; set; }

        public string Cover { get; set; }

        public bool IsFeatured { get; set; }

        public ArticleType ArticleType { get; set; }
    }

    public enum ArticleType
    {
        News,
        Announce
    }
}
