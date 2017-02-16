using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePiece.App.Models;
using OnePiece.App.Services;
using OnePiece.App.Utilities;
using OnePiece.App.Views;
using Prism.Commands;

namespace OnePiece.App.ViewModels
{
    public class VideoListPageViewModel : BaseViewModel
    {
        private bool _isMultiSeason;
        public bool IsMultiSeason
        {
            get { return _isMultiSeason; }
            set { SetProperty(ref _isMultiSeason, value); }
        }

        public string DataSource { get; set; }

        private Video _featuredVideo;
        public Video FeaturedVideo
        {
            get { return _featuredVideo; }
            set { SetProperty(ref _featuredVideo, value); }
        }

        /// <summary>
        /// Single-season
        /// </summary>
        private ObservableRangeCollection<Video> _videos;
        public ObservableRangeCollection<Video> Videos
        {
            get { return _videos ?? (_videos = new ObservableRangeCollection<Video>()); }
            set { _videos = value; }
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

        public VideoListPageViewModel(IAppService appService) : base(appService)
        {
            RefreshDataCommand = new DelegateCommand(async () => await ExecuteRefreshDataCommandAsync());
            PlayVideoCommand = new DelegateCommand(async () => await ExecutePlayVideoCommandAsync());
        }

        private async Task ExecuteRefreshDataCommandAsync()
        {
            await LoadAsync();
            IsRefreshingData = false;
        }

        private async Task ExecutePlayVideoCommandAsync()
        {
            await AppService.Navigation.NavigateAsync(nameof(VideoPlayerPage));
        }

        public async Task LoadAsync()
        {
            if(IsBusy)
                return;

            IsBusy = true;

            FeaturedVideo = new Video { Title = "Leo xuống lưng voi. Ra khơi mang Sanji trở về!", Description = "One Piece 776", Cover = "http://thegioiphimhay.com/wp-content/uploads/2016/11/review-danh-gia-phim-one-pice-dao-hai-tac-3.jpg", Poster = "http://static.hdonline.vn/i/resources/new/film/215x311/2016/07/20/one-piece-film-gold.jpg" };

            if (IsMultiSeason)
            {
                var seasons = await LoadSeasonsAsync();
                Seasons.Clear();
                Seasons.AddRange(seasons);
            }
            else
            {
                var videos = await LoadVideosAsync();
                Videos.Clear();
                Videos.AddRange(videos);
            }

            IsBusy = false;
        }

        private async Task<List<Video>> LoadVideosAsync()
        {
            var fakeData = new List<Video>
            {
                new Video { Title = "AAA", Description = "asfsgdsgdfgdgf", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" },
                new Video { Title = "AAA", Description = "asffdgnfg rtfjfgjhf dhh", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" },
                new Video { Title = "AAA", Description = "sdgdf dfh tr rty rgdgdfg", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" },
                new Video { Title = "AAA", Description = "erwerwet", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" },
                new Video { Title = "AAA", Description = "r werwerwer wet", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" },
                new Video { Title = "AAA", Description = "dbdgmdfv er qwfqwrfwer", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" }
            };

            return fakeData;
        }

        private async Task<List<Season>> LoadSeasonsAsync()
        {
            var fakeData = new List<Season>
            {
                new Season { Title = "Season 1", LatestEpisode = 1, TotalEpisodes = 10, Description = "asfsgdsgdfgdgf", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" },
                new Season { Title = "Season 2", LatestEpisode = 10, TotalEpisodes = 10, Description = "asffdgnfg rtfjfgjhf dhh", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb"},
                new Season { Title = "Season 3", LatestEpisode = 10, TotalEpisodes = 10, Description = "sdgdf dfh tr rty rgdgdfg", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqm"},
                new Season { Title = "Season 4", LatestEpisode = 10, TotalEpisodes = 10, Description = "erwerwet", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" },
                new Season { Title = "Season 5", LatestEpisode = 10, TotalEpisodes = 10, Description = "r werwerwer wet", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" },
                new Season { Title = "Season 6", LatestEpisode = 10, TotalEpisodes = 10, Description = "dbdgmdfv er qwfqwrfwer", Thumbnail = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQOcKyBoWtLojBu04h-QrtdDkiZagCB62dQUfWHjLcm6AiTSqmb" }
            };

            return fakeData;
        }
    }
}
