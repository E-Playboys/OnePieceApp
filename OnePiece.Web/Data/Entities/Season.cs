using System.Collections.Generic;

namespace OnePiece.Web.Data.Entities
{
    public class Season : BaseEntity
    {
        public int SeasonNumber { get; set; }

        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public string Avatar { get; set; }

        public List<Anime> Episodes { get; set; }

        public List<Manga> Chapters { get; set; }
    }
}
