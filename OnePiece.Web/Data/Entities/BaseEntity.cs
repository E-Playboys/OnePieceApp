using System.ComponentModel.DataAnnotations;

namespace OnePiece.Web.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
