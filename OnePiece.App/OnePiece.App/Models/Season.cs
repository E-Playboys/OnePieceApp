using OnePiece.App.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.Models
{
    public class Season
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public int LatestEpisode { get; set; }
        public int TotalEpisodes { get; set; }

        public string EpisodeInfo => LatestEpisode + "/" + TotalEpisodes;
        public ObservableRangeCollection<Video> Videos { get; set; } =
        new ObservableRangeCollection<Video>
            {
                new Video { Title = "AAA", Description = "asfsgdsgdfgdgf", Thumbnail = "http://pre06.deviantart.net/e2ef/th/pre/f/2014/149/a/2/one_piece_poster_by_thebartrempillo-d7k9vwa.jpg" },
                new Video { Title = "AAA", Description = "asffdgnfg rtfjfgjhf dhh", Thumbnail = "http://pre06.deviantart.net/e2ef/th/pre/f/2014/149/a/2/one_piece_poster_by_thebartrempillo-d7k9vwa.jpg" },
                new Video { Title = "AAA", Description = "sdgdf dfh tr rty rgdgdfg", Thumbnail = "http://pre06.deviantart.net/e2ef/th/pre/f/2014/149/a/2/one_piece_poster_by_thebartrempillo-d7k9vwa.jpg" },
                new Video { Title = "AAA", Description = "erwerwet", Thumbnail = "http://pre06.deviantart.net/e2ef/th/pre/f/2014/149/a/2/one_piece_poster_by_thebartrempillo-d7k9vwa.jpg" },
                new Video { Title = "AAA", Description = "r werwerwer wet", Thumbnail = "http://pre06.deviantart.net/e2ef/th/pre/f/2014/149/a/2/one_piece_poster_by_thebartrempillo-d7k9vwa.jpg" },
                new Video { Title = "AAA", Description = "dbdgmdfv er qwfqwrfwer", Thumbnail = "http://pre06.deviantart.net/e2ef/th/pre/f/2014/149/a/2/one_piece_poster_by_thebartrempillo-d7k9vwa.jpg" }
            };
    }
}
