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
        public string Title { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public ObservableRangeCollection<Video> RelatedVideos { get; set; } = new ObservableRangeCollection<Video> {
            new Video(),
            new Video(),
            new Video(),
            new Video(),
            new Video(),
            new Video(),
            new Video(),
            new Video(),
            new Video(),
        };
    }
}
