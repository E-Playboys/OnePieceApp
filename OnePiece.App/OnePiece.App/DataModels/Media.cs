using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataModels
{
    public class Media
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public MediaType Type { get; set; }

        public MediaQuality Quality { get; set; }
    }

    public enum MediaType
    {
        Image = 1, Gif = 2, Video = 3
    }

    public enum MediaQuality
    {
        P360, P540, P720, P1080
    }
}
