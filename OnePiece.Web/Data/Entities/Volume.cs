using System.Collections.Generic;

namespace OnePiece.Web.Data.Entities
{
    public class Volume : BaseEntity
    {
        public int VolumeNumber { get; set; }

        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public string Poster { get; set; }

        public string ChapterRange { get; set; }

        public List<Manga> Chapters { get; set; }

        public VolumeType VolumeType { get; set; }
    }

    public enum VolumeType
    {
        Anime,
        Manga
    }
}
