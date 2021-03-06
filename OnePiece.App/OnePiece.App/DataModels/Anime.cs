﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataModels
{
    public class Anime
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int EpisodeNumber { get; set; }

        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public string IntroText { get; set; }

        public string IntroTextEng { get; set; }

        public decimal? ImdbScore { get; set; }

        public decimal? Rating { get; set; }

        public int ViewCount { get; set; }

        public AnimeType Type { get; set; }

        public string Cover { get; set; }

        public string Poster { get; set; }

        public string Links { get; set; }

        [Ignore]
        public List<Media> Medias { get; set; }

        public int? SeasonId { get; set; }
    }

    public enum AnimeType
    {
        Story,
        TvSpecial,
        Movie
    }
}
