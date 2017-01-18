using OnePiece.App.Models;
using OnePiece.App.Services;
using OnePiece.App.Views.MainPageTabs;
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
        public DelegateCommand<MangaBook> LoadMoreCommand { get; set; }

        private ObservableCollection<MangaBook> _mangaBooks = new ObservableCollection<MangaBook>();
        public ObservableCollection<MangaBook> MangaBooks
        {
            get { return _mangaBooks ?? (_mangaBooks = new ObservableCollection<MangaBook>()); }
            set { SetProperty(ref _mangaBooks, value); }
        }

        public MangaPageViewModel(IAppService appService) : base(appService)
        {
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            ItemTappedCommand = DelegateCommand.FromAsyncHandler(ExecuteItemTappedCommand, CanExecuteItemTappedCommand);
            LoadMoreCommand = DelegateCommand<MangaBook>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public async Task ExecuteItemTappedCommand()
        {
            var item = LastTappedItem as MangaBook;

            await OpenChapter(item);
        }

        public async Task OpenChapter(MangaBook item)
        {
            var context = new MangaReaderPageViewModel(AppService)
            {
                MangaBook = item
            };
            await context.FetchAllPages();

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

        public async Task LoadMangaBooks(int skip = 0)
        {
            for (int i = skip; i < skip + 21; i++)
            {
                var book = new MangaBook
                {
                    Title = "Magi",
                    ChapterNum = $"Chapter {i}",
                    ImageUrl = "http://st.thichtruyentranh.com/images/icon/0048/one-piece1416866288.jpg",
                    PrevChapter = i - 1,
                    NextChapter = i + 1
                };
                MangaBooks.Add(book);
            }
        }

        public async Task<List<string>> LoadChapterPicker()
        {
            for (int i = 0; i < 800; i++)
            {
                ChapterNameIdMap.Add($"Chapter {i}", i);
            }
            return ChapterNameIdMap.Select(r => r.Key).ToList();
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            MangaBooks = new ObservableCollection<MangaBook>();
            await LoadMangaBooks();

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(MangaBook item)
        {
            return IsNotBusy;
        }

        public async Task ExecuteLoadMoreCommand(MangaBook item)
        {
            IsBusy = true;

            var skip = MangaBooks.Count;
            await LoadMangaBooks(skip);

            IsBusy = false;
        }
    }
}
