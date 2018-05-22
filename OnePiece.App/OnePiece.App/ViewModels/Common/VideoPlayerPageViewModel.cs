using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsPlugin.Iconize;
using OnePiece.App.DataModels;
using OnePiece.App.DataServices.Anime;
using OnePiece.App.DataServices.Season;
using OnePiece.App.Services;
using OnePiece.App.Utilities;
using OnePiece.App.Views;
using Prism.Commands;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;

namespace OnePiece.App.ViewModels
{
    public class VideoPlayerPageViewModel : BaseViewModel
    {
        private readonly ISeasonApiService _seasonService;
        private readonly IAnimeApiService _animeService;

        private Anime _anime;
        public Anime Anime
        {
            get { return _anime; }
            set { SetProperty(ref _anime, value); }
        }

        private string _animeType;
        public string AnimeType
        {
            get { return _animeType; }
            set { SetProperty(ref _animeType, value); }
        }

        private ObservableRangeCollection<Anime> _animes;
        public ObservableRangeCollection<Anime> Animes
        {
            get { return _animes ?? (_animes = new ObservableRangeCollection<Anime>()); }
            set { _animes = value; }
        }

        private ObservableRangeCollection<Season> _seasons;
        public ObservableRangeCollection<Season> Seasons
        {
            get { return _seasons ?? (_seasons = new ObservableRangeCollection<Season>()); }
            set { _seasons = value; }
        }

        private ObservableRangeCollection<InfoProperty> _videoInfoProperties;
        public ObservableRangeCollection<InfoProperty> VideoInfoProperties
        {
            get { return _videoInfoProperties ?? (_videoInfoProperties = new ObservableRangeCollection<InfoProperty>()); }
            set { _videoInfoProperties = value; }
        }

        public DelegateCommand<Anime> SelectAnimeCommand { get; set; }
        public DelegateCommand<Season> SelectSeasonCommand { get; set; }

        public VideoPlayerPageViewModel(IAppService appService, IAnimeApiService animeService, ISeasonApiService seasonService) : base(appService)
        {
            SelectAnimeCommand = new DelegateCommand<Anime>((anime) => Anime = anime);
            SelectSeasonCommand = new DelegateCommand<Season>(async(season) => {
                await PopupNavigation.PushAsync(new SeasonPage(season));
            });

            VideoInfoProperties.AddRange(new List<InfoProperty>()
            {
                new InfoProperty()
                {
                    PropertyName = "Đạo diễn",
                    PropertyValue = "Takuji Suzuki, Shinobu Yaguchi"
                },
                new InfoProperty()
                {
                    PropertyName = "Biên tập",
                    PropertyValue = "Takuji Suzuki, Shinobu Yaguchi"
                },
                new InfoProperty()
                {
                    PropertyName = "Diễn viên",
                    PropertyValue = " Travis Mullenix, Satoru Jitsunashi, Jason C. Kane, Masumi Kuichi, Nao Nekota, Yôji Tanaka"
                },
                new InfoProperty()
                {
                    PropertyName = "Quay phim",
                    PropertyValue = "Shinobu Yaguchi	"
                },
                new InfoProperty()
                {
                    PropertyName = "Biên tập phim",
                    PropertyValue = "Shinobu Yaguchi	"
                },
                new InfoProperty()
                {
                    PropertyName = "Âm thanh",
                    PropertyValue = "Joseph Shalack"
                },
                new InfoProperty()
                {
                    PropertyName = "Lòng tiếng",
                    PropertyValue = "Chuck Powers"
                }
            });
            _seasonService = seasonService;
            _animeService = animeService;
        }

        public async Task LoadAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            List<Anime> animes = null;
            switch (AnimeType.ToLower())
            {
                case "stories":
                    animes = await _animeService.ListStoriesAsync(new DataServices.ListRequest { Skip = Animes.Count });
                    break;
                case "tvspecials":
                    animes = await _animeService.ListTvSpecialsAsync(new DataServices.ListRequest { Skip = Animes.Count });
                    break;
                case "movies":
                    animes = await _animeService.ListMoviesAsync(new DataServices.ListRequest { Skip = Animes.Count });
                    break;
            }

            Animes.Clear();
            Animes.AddRange(animes);

            var seasons = await _seasonService.ListAsync(new DataServices.ListRequest { Skip = Seasons.Count });
            Seasons.Clear();
            Seasons.AddRange(seasons);

            IsBusy = false;
        }
    }

    public class InfoProperty
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}
