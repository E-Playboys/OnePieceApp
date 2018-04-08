using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataModels
{
    public class Season
    {
        public int Id { get; set; }

        public int SeasonNumber { get; set; }

        public string Title { get; set; }

        public string TitleEng { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public string Avatar { get; set; }

        public List<Anime> Episodes { get; set; }

        public List<Manga> Chapters { get; set; }
    }
}
