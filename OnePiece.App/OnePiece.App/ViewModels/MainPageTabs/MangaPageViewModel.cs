using OnePiece.App.DataModels;
using OnePiece.App.DataServices;
using OnePiece.App.Views;
using OnePiece.App.Utilities;
using Prism.Commands;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OnePiece.App.DataServices.Manga;
using OnePiece.App.Services;
using Xamarin.Forms;

namespace OnePiece.App.ViewModels
{
    class MangaPageViewModel : BaseViewModel
    {
        public object LastTappedItem { get; set; }
        public Dictionary<string, int> ChapterNameIdMap { get; set; } = new Dictionary<string, int>();

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand ItemTappedCommand { get; set; }
        public DelegateCommand<Manga> LoadMoreCommand { get; set; }

        private ObservableRangeCollection<Manga> _mangas = new ObservableRangeCollection<Manga>();
        public ObservableRangeCollection<Manga> Mangas
        {
            get { return _mangas ?? (_mangas = new ObservableRangeCollection<Manga>()); }
            set { SetProperty(ref _mangas, value); }
        }

        private ObservableRangeCollection<Manga> _featuredMangas = new ObservableRangeCollection<Manga>();
        public ObservableRangeCollection<Manga> FeaturedMangas
        {
            get { return _featuredMangas ?? (_featuredMangas = new ObservableRangeCollection<Manga>()); }
            set { SetProperty(ref _featuredMangas, value); }
        }

        private readonly IMangaApiService _mangaService;

        public MangaPageViewModel(IAppService appService, IMangaApiService mangaService) : base(appService)
        {
            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            ItemTappedCommand = new DelegateCommand(ExecuteItemTappedCommand, CanExecuteItemTappedCommand);
            LoadMoreCommand = new DelegateCommand<Manga>(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);

            _mangaService = mangaService;
        }

        public async void ExecuteItemTappedCommand()
        {
            var item = LastTappedItem as Manga;

            await OpenChapter(item.ChapterNumber);
        }

        public async Task OpenChapter(int chapterNumber)
        {
            var mangaReaderPage = new MangaReaderPage(chapterNumber);
            await PopupNavigation.PushAsync(mangaReaderPage);
        }

        public bool CanExecuteItemTappedCommand()
        {
            return IsNotBusy;
        }

        public async Task LoadMangas(int skip = 0)
        {
            const int takeCount = 21;

            var chapters = await _mangaService.ListAsync(new DataServices.ListRequest
            {
                Skip = skip,
                Take = takeCount
            });

            foreach (var chapter in chapters)
            {
                var pages = chapter.PagesJson?.Split('|').Where(x => !string.IsNullOrEmpty(x));
                if (pages.Any())
                {
                    var poster = string.IsNullOrEmpty(chapter.Poster) ? pages.FirstOrDefault() : chapter.Poster;
                    chapter.Poster = poster.Replace("upload/", "upload/c_thumb,w_300/");
                }
            }

            Mangas.Clear();
            Mangas.AddRange(chapters);

            FeaturedMangas.Clear();
            FeaturedMangas.AddRange(chapters.OrderByDescending(x => x.ChapterNumber).Take(3));
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async void ExecuteRefreshCommand()
        {
            IsBusy = true;

            Mangas = new ObservableRangeCollection<Manga>();
            await LoadMangas();

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(Manga item)
        {
            return IsNotBusy && (!Mangas.Any() /*|| !Mangas[0].IsLoading*/);
        }

        public async void ExecuteLoadMoreCommand(Manga item)
        {
            IsBusy = true;

            var skip = Mangas.Count;
            await LoadMangas(skip);

            IsBusy = false;
        }
    }
}
