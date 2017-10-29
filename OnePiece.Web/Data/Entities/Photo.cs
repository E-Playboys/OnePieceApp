using System.ComponentModel.DataAnnotations.Schema;

namespace OnePiece.Web.Data.Entities
{
    [Table("Photos")]
    public class Photo : BaseEntity
    {
        public string Source { get; set; }
    }
}
