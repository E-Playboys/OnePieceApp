using OnePiece.App.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.Models
{
    public class Anime
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int EpisodeNumber { get; set; }

        public int ViewCount { get; set; }

        public string Cover { get; set; }
    }
}
