using OnePiece.App.Models;
using OnePiece.App.Services;
using OnePiece.App.Services.Manga;
using OnePiece.App.Views;
using OnePiece.App.Utilities;
using Prism.Commands;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnePiece.App.ViewModels
{
    class MangaPageViewModel : BaseViewModel
    {
        public object LastTappedItem { get; set; }
        public Dictionary<string, int> ChapterNameIdMap { get; set; } = new Dictionary<string, int>();

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand ItemTappedCommand { get; set; }
        public DelegateCommand<MangaChapter> LoadMoreCommand { get; set; }

        private ObservableRangeCollection<MangaChapter> _mangaChapters = new ObservableRangeCollection<MangaChapter>();
        public ObservableRangeCollection<MangaChapter> MangaChapters
        {
            get { return _mangaChapters ?? (_mangaChapters = new ObservableRangeCollection<MangaChapter>()); }
            set { SetProperty(ref _mangaChapters, value); }
        }

        private readonly IMangaService _mangaService;

        public MangaPageViewModel(IAppService appService, IMangaService mangaService) : base(appService)
        {
            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            ItemTappedCommand = new DelegateCommand(ExecuteItemTappedCommand, CanExecuteItemTappedCommand);
            LoadMoreCommand = new DelegateCommand<MangaChapter>(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);

            _mangaService = mangaService;
        }

        public async void ExecuteItemTappedCommand()
        {
            var item = LastTappedItem as MangaChapter;

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

        public async Task LoadMangaChapters(int skip = 0)
        {
            const int takeCount = 21;

            var chapters = await _mangaService.ListChaptersAsync(new ListMangaChaptersRq
            {
                Skip = skip,
                Take = takeCount
            });

            var screenWidth = App.ScreenWidth;
            foreach (var chapter in chapters)
            {
                chapter.CoverImageWidth = (screenWidth - 30) / 3;
            }

            MangaChapters.AddRange(chapters);
        }

        public async Task LoadChapterPicker()
        {
            var allChapters = await _mangaService.ListChaptersAsync(new ListMangaChaptersRq());
            ChapterNameIdMap = allChapters.ToDictionary(r => r.ChapterNum, r => r.Id);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async void ExecuteRefreshCommand()
        {
            IsBusy = true;

            MangaChapters = new ObservableRangeCollection<MangaChapter>();
            await LoadMangaChapters();

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(MangaChapter item)
        {
            return IsNotBusy && (!MangaChapters.Any() || !MangaChapters[0].IsLoading);
        }

        public async void ExecuteLoadMoreCommand(MangaChapter item)
        {
            IsBusy = true;

            var skip = MangaChapters.Count;
            await LoadMangaChapters(skip);

            IsBusy = false;
        }
    }
}
