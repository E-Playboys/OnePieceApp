using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePiece.App.LocalData;
using OnePiece.App.Models;
using OnePiece.App.Services;
using Prism.Commands;
using OnePiece.App.Views;
using Rg.Plugins.Popup.Services;
using OnePiece.App.Utilities;

namespace OnePiece.App.ViewModels
{
    public class PersonalPageViewModel : BaseViewModel
    {
        private readonly IAppDataStorage _appDataStorage;

        private ObservableRangeCollection<Anime> _recentAnimes;
        public ObservableRangeCollection<Anime> RecentAnimes
        {
            get { return _recentAnimes ?? (_recentAnimes = new ObservableRangeCollection<Anime>()); }
            set { SetProperty(ref _recentAnimes, value); }
        }

        public DelegateCommand<Anime> SelectAnimeCommand { get; set; }

        public PersonalPageViewModel(IAppService appService, IAppDataStorage appDataStorage) : base(appService)
        {
            _appDataStorage = appDataStorage;
            SelectAnimeCommand = new DelegateCommand<Anime>(async (anime) => await ExecuteSelectAnimeCommandAsync(anime));
        }

        private async Task ExecuteSelectAnimeCommandAsync(Anime anime)
        {
            await PopupNavigation.PushAsync(new VideoPlayerPage(anime.Id, "stories"));
        }

        public void LoadData()
        {
            IsBusy = true;

            var animes = _appDataStorage.GetAnimes();

            if(animes != null)
            {
                RecentAnimes.Clear();
                RecentAnimes.AddRange(animes);
            }

            IsBusy = false;
        }
    }
}
