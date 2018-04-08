using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices
{
    public class ListRequest
    {
        public int Skip { get; set; }

        public int Take { get; set; } = 20;
    }
}
