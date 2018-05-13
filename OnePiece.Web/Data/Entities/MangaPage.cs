using System.ComponentModel.DataAnnotations.Schema;

namespace OnePiece.Web.Data.Entities
{
    [Table("MangaPages")]
    public class MangaPage : BaseEntity
    {
        public string Url { get; set; }

        public int PageNumber { get; set; }

        public int? MangaId { get; set; }

        public Manga Manga { get; set; }
    }
}
