﻿using System.Collections.ObjectModel;
using System.Linq;

namespace OnePiece.App.Models
{
    public class MangaChapter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ChapterNum
        {
            get
            {
                return $"Chapter {Number}";
            }
        }

        public int Number { get; set; }

        public string CoverImageLink
        {
            get
            {
                return MangaImages?.Count > 0 ? MangaImages.FirstOrDefault().Link : string.Empty;
            }
        }

        public bool IsLoading { get; set; }

        public int CoverImageWidth { get; set; }

        public ObservableCollection<MangaImage> MangaImages { get; set; } = new ObservableCollection<MangaImage>() {
            new MangaImage() { Link = "http://www.onepiecepodcast.com/wp-content/uploads/2016/02/OPFilm-Gold-1-1.png" },
            new MangaImage() { Link = "http://www.onepiecepodcast.com/wp-content/uploads/2016/02/OPFilm-Gold-1-1.png" },
            new MangaImage() { Link = "http://www.onepiecepodcast.com/wp-content/uploads/2016/02/OPFilm-Gold-1-1.png" },
            new MangaImage() { Link = "http://www.onepiecepodcast.com/wp-content/uploads/2016/02/OPFilm-Gold-1-1.png" },
            new MangaImage() { Link = "http://www.onepiecepodcast.com/wp-content/uploads/2016/02/OPFilm-Gold-1-1.png" },
            new MangaImage() { Link = "http://www.onepiecepodcast.com/wp-content/uploads/2016/02/OPFilm-Gold-1-1.png" },
            new MangaImage() { Link = "http://www.onepiecepodcast.com/wp-content/uploads/2016/02/OPFilm-Gold-1-1.png" },
            new MangaImage() { Link = "http://www.onepiecepodcast.com/wp-content/uploads/2016/02/OPFilm-Gold-1-1.png" },
            new MangaImage() { Link = "http://www.onepiecepodcast.com/wp-content/uploads/2016/02/OPFilm-Gold-1-1.png" },
        };

        public string Avatar { get; set; }

        public string Description { get; set; }

        public int MangaVolumeId { get; set; }
    }
}
