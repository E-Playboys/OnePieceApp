using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePiece.WebAPI.Models
{
    public class Season : BaseEntity
    {
        public int No { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
