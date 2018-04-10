using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.Anime
{
    public class ListEpisodeBySeasonRequest : ListRequest
    {
        public int SeasonId { get; set; }
    }
}
