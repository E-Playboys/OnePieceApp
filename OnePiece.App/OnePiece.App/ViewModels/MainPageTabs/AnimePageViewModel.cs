using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePiece.App.Models;
using OnePiece.App.Services;
using Prism.Commands;

namespace OnePiece.App.ViewModels
{
    public class AnimePageViewModel : BaseViewModel
    {
        private Anime _activeAnime;
        public Anime ActiveAnime
        {
            get { return _activeAnime; }
            set { SetProperty(ref _activeAnime, value); }
        }

        private ObservableCollection<Anime> _animes;
        public ObservableCollection<Anime> Animes
        {
            get { return _animes ?? (_animes = new ObservableCollection<Anime>()); }
            set { SetProperty(ref _animes, value); }
        }

        public DelegateCommand LoadAnimesCommand { get; set; }
        public DelegateCommand<Anime> ViewAnimeCommand { get; set; }

        public AnimePageViewModel(IAppService appService) : base(appService)
        {
            LoadAnimesCommand = new DelegateCommand(async () => await ExecuteLoadAnimesCommandAsync());
            ViewAnimeCommand = new DelegateCommand<Anime>(async (anime) => await ExecuteViewAnimeCommandAsync(anime));
        }

        public async Task ExecuteLoadAnimesCommandAsync()
        {
            IsBusy = true;

            var animes = await LoadAnimesAsync();
            foreach (var anime in animes)
            {
                Animes.Add(anime);
            }

            IsBusy = false;
        }

        private async Task ExecuteViewAnimeCommandAsync(Anime anime)
        {

        }

        private async Task<List<Anime>> LoadAnimesAsync()
        {
            var fakeData = new List<Anime>
            {
                new Anime { Title = "AAA"},
                new Anime { Title = "AAA"},
                new Anime { Title = "AAA"},
                new Anime { Title = "AAA"},
                new Anime { Title = "AAA"},
                new Anime { Title = "AAA"}
            };

            return fakeData;
        }
    }
}
