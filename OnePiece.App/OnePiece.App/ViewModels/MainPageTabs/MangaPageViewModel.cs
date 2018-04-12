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

        private ObservableRangeCollection<Manga> _mangaChapters = new ObservableRangeCollection<Manga>();
        public ObservableRangeCollection<Manga> Mangas
        {
            get { return _mangaChapters ?? (_mangaChapters = new ObservableRangeCollection<Manga>()); }
            set { SetProperty(ref _mangaChapters, value); }
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

            await OpenChapter(item.Id);
        }

        public async Task OpenChapter(int chapterId)
        {
            var context = new MangaReaderPageViewModel(AppService, _mangaService)
            {
                MangaChapterId = chapterId
            };

            var mangaReaderPage = new MangaReaderPage()
            {
                BindingContext = context,
                BackgroundColor = Color.Black,
                Opacity = 1,
                CloseWhenBackgroundIsClicked = false
            };
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

            //var screenWidth = App.ScreenWidth;
            //foreach (var chapter in chapters)
            //{
            //    chapter.CoverImageWidth = (screenWidth - 30) / 3;
            //}

            Mangas.AddRange(chapters);
        }

        public async Task LoadChapterPicker()
        {
            //var allChapters = await _mangaService.ListChaptersAsync(new ListMangasRq());
            //ChapterNameIdMap = allChapters.ToDictionary(r => r.ChapterNum, r => r.Id);
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
