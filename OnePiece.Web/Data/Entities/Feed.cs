using System.Collections.Generic;

namespace OnePiece.Web.Data.Entities
{
    public class Feed : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<Photo> Photos { get; set; }

        public List<Video> Videos { get; set; }
    }
}
