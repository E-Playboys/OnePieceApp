using System.Collections.Generic;
using System.Threading.Tasks;
using OnePiece.App.DataModels;
using OnePiece.App.DataServices.Anime;
using OnePiece.App.DataServices.Season;
using OnePiece.App.Services;
using OnePiece.App.Utilities;
using OnePiece.App.Views;
using Prism.Commands;
using Rg.Plugins.Popup.Services;

namespace OnePiece.App.ViewModels
{
    public class VideoListPageViewModel : BaseViewModel
    {
        private readonly ISeasonApiService _seasonService;
        private readonly IAnimeApiService _animeService;

        private bool _isMultiSeason;
        public bool IsMultiSeason
        {
            get { return _isMultiSeason; }
            set { SetProperty(ref _isMultiSeason, value); }
        }

        public string DataSource { get; set; }

        private Anime _featuredVideo;
        public Anime FeaturedVideo
        {
            get { return _featuredVideo; }
            set { SetProperty(ref _featuredVideo, value); }
        }

        /// <summary>
        /// Single-season
        /// </summary>
        private ObservableRangeCollection<Anime> _animes;
        public ObservableRangeCollection<Anime> Animes
        {
            get { return _animes ?? (_animes = new ObservableRangeCollection<Anime>()); }
            set { _animes = value; }
        }

        /// <summary>
        /// Multi-season
        /// </summary>
        private ObservableRangeCollection<Season> _seasons;
        public ObservableRangeCollection<Season> Seasons
        {
            get { return _seasons ?? (_seasons = new ObservableRangeCollection<Season>()); }
            set { _seasons = value; }
        }

        public DelegateCommand RefreshDataCommand { get; set; }
        public DelegateCommand<Anime> SelectAnimeCommand { get; set; }
        public DelegateCommand<Season> SelectSeasonCommand { get; set; }
        public DelegateCommand PlayVideoCommand { get; set; }

        public VideoListPageViewModel(IAppService appService, ISeasonApiService seasonService, IAnimeApiService animeService) : base(appService)
        {
            _seasonService = seasonService;
            _animeService = animeService;

            RefreshDataCommand = new DelegateCommand(async () => await ExecuteRefreshDataCommandAsync());
            SelectAnimeCommand = new DelegateCommand<Anime>(async(anime) => await ExecuteSelectAnimeCommandAsync(anime));
            SelectSeasonCommand = new DelegateCommand<Season>(async (season) => {
                await PopupNavigation.PushAsync(new SeasonPage(season));
            });
            PlayVideoCommand = new DelegateCommand(async() => await ExecutePlayVideoCommandAsync());
        }

        private async Task ExecuteRefreshDataCommandAsync()
        {
            await LoadAsync(true);
        }

        private async Task ExecuteSelectAnimeCommandAsync(Anime anime)
        {
            await PopupNavigation.PushAsync(new VideoPlayerPage(anime, DataSource.ToLower()));
        }

        private async Task ExecutePlayVideoCommandAsync()
        {
            await PopupNavigation.PushAsync(new VideoPlayerPage(FeaturedVideo, DataSource.ToLower()));
        }

        public async Task LoadAsync(bool refresh = false)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (IsMultiSeason)
            {
                FeaturedVideo = await _animeService.GetLatestEpisodeAsync();

                var seasons = await _seasonService.ListAsync(new DataServices.ListRequest { Skip = refresh ? 0 : Seasons.Count });
                Seasons.Clear();
                Seasons.AddRange(seasons);

                foreach (var season in Seasons)
                {
                    var episodes = await _animeService.ListEpisodeBySeasonAsync(new ListEpisodeBySeasonRequest { SeasonId = season.Id, Take = 5 });
                    season.Episodes.AddRange(episodes);
                }
            }
            else
            {
                List<Anime> animes = null;
                if (DataSource.ToLower() == "tvspecials")
                {
                    FeaturedVideo = await _animeService.GetLatestTvSpecialAsync();
                    animes = await _animeService.ListTvSpecialsAsync(new DataServices.ListRequest { Skip = refresh ? 0 : Animes.Count });
                }
                else if (DataSource.ToLower() == "movies")
                {
                    FeaturedVideo = await _animeService.GetLatestMovieAsync();
                    animes = await _animeService.ListMoviesAsync(new DataServices.ListRequest { Skip = refresh ? 0 : Animes.Count });
                }

                if(animes != null)
                {
                    Animes.Clear();
                    Animes.AddRange(animes);
                }
            }

            IsBusy = false;
        }
    }
}
