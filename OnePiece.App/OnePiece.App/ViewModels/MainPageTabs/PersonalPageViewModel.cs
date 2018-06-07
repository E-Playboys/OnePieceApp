using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePiece.App.LocalData;
using OnePiece.App.DataModels;
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

        private ObservableRangeCollection<Manga> _recentMangas;
        public ObservableRangeCollection<Manga> RecentMangas
        {
            get { return _recentMangas ?? (_recentMangas = new ObservableRangeCollection<Manga>()); }
            set { SetProperty(ref _recentMangas, value); }
        }

        public DelegateCommand<Anime> SelectAnimeCommand { get; set; }
        public DelegateCommand<Manga> SelectMangaCommand { get; set; }

        public PersonalPageViewModel(IAppService appService, IAppDataStorage appDataStorage) : base(appService)
        {
            _appDataStorage = appDataStorage;
            SelectAnimeCommand = new DelegateCommand<Anime>(async (anime) => await ExecuteSelectAnimeCommandAsync(anime));
            SelectMangaCommand = new DelegateCommand<Manga>(async (manga) => await ExecuteSelectMangaCommandAsync(manga));
        }

        private async Task ExecuteSelectAnimeCommandAsync(Anime anime)
        {
            await PopupNavigation.PushAsync(new VideoPlayerPage(anime, "stories"));
        }

        private async Task ExecuteSelectMangaCommandAsync(Manga manga)
        {
            await PopupNavigation.PushAsync(new MangaReaderPage(manga.ChapterNumber));
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

            var mangas = _appDataStorage.GetMangas();

            if (mangas != null)
            {
                RecentMangas.Clear();
                RecentMangas.AddRange(mangas);
            }

            IsBusy = false;
        }
    }
}
