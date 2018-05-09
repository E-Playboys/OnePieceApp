using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePiece.App.DataModels;
using OnePiece.App.Services;
using OnePiece.App.Utilities;

namespace OnePiece.App.ViewModels
{
    public class VideoPlayerPageViewModel : BaseViewModel
    {
        private Anime _anime;
        public Anime Anime
        {
            get { return _anime; }
            set { SetProperty(ref _anime, value); }
        }

        private ObservableRangeCollection<Anime> _episodes;
        public ObservableRangeCollection<Anime> Episodes
        {
            get { return _episodes ?? (_episodes = new ObservableRangeCollection<Anime>()); }
            set { _episodes = value; }
        }

        private ObservableRangeCollection<InfoProperty> _videoInfoProperties;
        public ObservableRangeCollection<InfoProperty> VideoInfoProperties
        {
            get { return _videoInfoProperties ?? (_videoInfoProperties = new ObservableRangeCollection<InfoProperty>()); }
            set { _videoInfoProperties = value; }
        }

        private ObservableRangeCollection<Anime> _relatedVideos;
        public ObservableRangeCollection<Anime> RelatedVideos
        {
            get { return _relatedVideos ?? (_relatedVideos = new ObservableRangeCollection<Anime>()); }
            set { _relatedVideos = value; }
        }

        public VideoPlayerPageViewModel(IAppService appService) : base(appService)
        {
            var animes = new List<Anime>();
            for (int i = 1; i < 5; i++)
            {
                animes.Add(new Anime()
                {
                    Title = $"{i}"
                });
            }
            Episodes.AddRange(animes);

            VideoInfoProperties.AddRange(new List<InfoProperty>()
            {
                new InfoProperty()
                {
                    PropertyName = "Đạo diễn",
                    PropertyValue = "Troy"
                },
                new InfoProperty()
                {
                    PropertyName = "Diễn viên",
                    PropertyValue = "Troy Lee, Luffy, Zoro, Nami, Sanji"
                },
                new InfoProperty()
                {
                    PropertyName = "Thể loại",
                    PropertyValue = "Hoạt hình, Kinh điển"
                },
                new InfoProperty()
                {
                    PropertyName = "Thời lượng",
                    PropertyValue = "30 phút"
                },
                new InfoProperty()
                {
                    PropertyName = "Năm sản xuất",
                    PropertyValue = "2016"
                }
            });

            RelatedVideos.AddRange(animes);
        }
    }

    public class InfoProperty
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}
