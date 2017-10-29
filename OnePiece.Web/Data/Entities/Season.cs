using System.Collections.Generic;

namespace OnePiece.Web.Data.Entities
{
    public class Season : BaseEntity
    {
        public int No { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
