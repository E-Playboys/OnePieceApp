﻿using System.Collections.Generic;

namespace OnePiece.Web.Data.Entities
{
    public class Season : BaseEntity
    {
        public int SeasonNumber { get; set; }

        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public string Cover { get; set; }

        public string Poster { get; set; }

        public string EpisodeRange { get; set; }

        public string ChapterRange { get; set; }

        public List<Anime> Episodes { get; set; }

        public List<Manga> Chapters { get; set; }
    }
}
