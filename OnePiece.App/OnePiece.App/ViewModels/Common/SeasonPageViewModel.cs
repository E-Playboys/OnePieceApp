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
    public class SeasonPageViewModel : BaseViewModel
    {
        private readonly ISeasonApiService _seasonService;
        private readonly IAnimeApiService _animeService;

        private Season _season;
        public Season Season
        {
            get { return _season; }
            set { SetProperty(ref _season, value); }
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

        public DelegateCommand RefreshDataCommand { get; set; }
        public DelegateCommand<Anime> SelectAnimeCommand { get; set; }

        public SeasonPageViewModel(IAppService appService, ISeasonApiService seasonService, IAnimeApiService animeService) : base(appService)
        {
            _seasonService = seasonService;
            _animeService = animeService;

            RefreshDataCommand = new DelegateCommand(async () => await ExecuteRefreshDataCommandAsync());
            SelectAnimeCommand = new DelegateCommand<Anime>(async(anime) => await ExecuteSelectAnimeCommandAsync(anime));
        }

        private async Task ExecuteRefreshDataCommandAsync()
        {
            await LoadAsync(true);
        }

        private async Task ExecuteSelectAnimeCommandAsync(Anime anime)
        {
            await PopupNavigation.PushAsync(new VideoPlayerPage(anime, "stories"));
        }

        public async Task LoadAsync(bool refresh = false)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var animes = await _animeService.ListEpisodeBySeasonAsync(new ListEpisodeBySeasonRequest { Skip = refresh ? 0 : Animes.Count, SeasonId = Season.Id });          

            if (animes != null)
            {
                Animes.Clear();
                Animes.AddRange(animes);
            }

            IsBusy = false;
        }
    }
}
