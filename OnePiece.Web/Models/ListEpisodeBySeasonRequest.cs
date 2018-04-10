using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePiece.Web.Models
{
    public class ListEpisodeBySeasonRequest : ListRequest
    {
        public int SeasonId { get; set; }
    }
}
