using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePiece.WebAPI.Models
{
    public class Feed : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<Photo> Photos { get; set; }

        public List<Video> Videos { get; set; }
    }
}
