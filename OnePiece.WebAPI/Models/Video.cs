using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePiece.WebAPI.Models
{
    public class Video : BaseEntity
    {
        public string Source { get; set; }

        public int Resolution { get; set; }
    }
}
