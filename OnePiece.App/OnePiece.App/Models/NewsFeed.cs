using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.Models
{
    public class NewsFeed
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Gif { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
