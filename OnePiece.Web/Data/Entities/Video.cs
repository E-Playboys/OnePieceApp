using System.ComponentModel.DataAnnotations.Schema;

namespace OnePiece.Web.Data.Entities
{
    [Table("Videos")]
    public class Video : BaseEntity
    {
        public string Source { get; set; }

        public int Resolution { get; set; }
    }
}
