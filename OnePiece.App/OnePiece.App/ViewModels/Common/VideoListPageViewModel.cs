using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePiece.App.DataModels;
using OnePiece.App.DataServices;
using OnePiece.App.DataServices.Anime;
using OnePiece.App.DataServices.Season;
using OnePiece.App.Services;
using OnePiece.App.Utilities;
using OnePiece.App.Views;
using Prism.Commands;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
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

        private bool _isRefreshingData;
        public bool IsRefreshingData
        {
            get { return _isRefreshingData; }
            set { SetProperty(ref _isRefreshingData, value); }
        }

        public DelegateCommand RefreshDataCommand { get; set; }

        public DelegateCommand PlayVideoCommand { get; set; }

        public VideoListPageViewModel(IAppService appService, ISeasonApiService seasonService, IAnimeApiService animeService) : base(appService)
        {
            _seasonService = seasonService;
            _animeService = animeService;

            RefreshDataCommand = new DelegateCommand(async () => await ExecuteRefreshDataCommandAsync());
            PlayVideoCommand = new DelegateCommand(() => ExecutePlayVideoCommandAsync());
        }

        private async Task ExecuteRefreshDataCommandAsync()
        {
            await LoadAsync();
            IsRefreshingData = false;
        }

        private async void ExecutePlayVideoCommandAsync()
        {
            //CrossMediaManager.Current.Play("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4", MediaFileType.Anime);
            //await AppService.Navigation.NavigateAsync(nameof(VideoPlayerPage));
            await PopupNavigation.PushAsync(new VideoPlayerPage(FeaturedVideo));
        }

        public async Task LoadAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (IsMultiSeason)
            {
                FeaturedVideo = await _animeService.GetLatestEpisodeAsync();

                var seasons = await _seasonService.ListAsync(new DataServices.ListRequest { Skip = Seasons.Count });
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
                    animes = await _animeService.ListTvSpecialsAsync(new DataServices.ListRequest { Skip = Animes.Count });
                }
                else if (DataSource.ToLower() == "movies")
                {
                    FeaturedVideo = await _animeService.GetLatestMovieAsync();
                    animes = await _animeService.ListMoviesAsync(new DataServices.ListRequest { Skip = Animes.Count });
                }

                Animes.Clear();
                Animes.AddRange(animes);
            }

            IsBusy = false;
        }
    }
}
