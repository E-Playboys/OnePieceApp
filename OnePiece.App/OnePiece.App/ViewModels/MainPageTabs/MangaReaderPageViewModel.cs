using OnePiece.App.Models;
using OnePiece.App.Services;
using Prism.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePiece.App.ViewModels
{
    public class MangaReaderPageViewModel : BaseViewModel
    {
        private MangaBook _mangaBook = new MangaBook();
        public MangaBook MangaBook
        {
            get
            {
                return _mangaBook;
            }
            set
            {
                SetProperty(ref _mangaBook, value);
            }
        }

        public List<MangaPage> AllPages { get; set; } = new List<MangaPage>();

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<MangaPage> LoadMoreCommand { get; set; }

        private int _currentPageNum;
        public int CurrentPageNum
        {
            get
            {
                return _currentPageNum;
            }
            set
            {
                _currentPageNum = value;
                OnPropertyChanged(nameof(CurrentPageString));
            }
        }
        public int TotalPages
        {
            get
            {
                return AllPages.Count;
            }
        }
        public string CurrentPageString
        {
            get
            {
                return CurrentPageNum + "/" + TotalPages;
            }
        }

        public MangaReaderPageViewModel(IAppService appService) : base(appService)
        {
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<MangaPage>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public async Task NextChapter()
        {
            var newBook = new MangaBook();
            newBook.ChapterNum = $"Chapter {MangaBook.NextChapter}";
            newBook.Title = "Magi";
            newBook.PrevChapter = MangaBook.PrevChapter + 1;
            newBook.NextChapter = MangaBook.NextChapter + 1;
            MangaBook = newBook;

            await FetchAllPages();
            await LoadMorePages();
        }

        public async Task PrevChapter()
        {
            var newBook = new MangaBook();
            newBook.ChapterNum = $"Chapter {MangaBook.PrevChapter}";
            newBook.Title = "Magi";
            newBook.PrevChapter = MangaBook.PrevChapter - 1;
            newBook.NextChapter = MangaBook.NextChapter - 1;
            MangaBook = newBook;

            await FetchAllPages();
            await LoadMorePages();
        }

        public async Task FetchAllPages()
        {
            AllPages = new List<MangaPage>();

            for (int i = 0; i < 20; i++)
            {
                var page = new MangaPage
                {
                    Title = $"Page {i}",
                    ImageUrl = "http://i.imgur.com/mfKNPEF.jpg",
                    MinimumHeight = 200
                };
                AllPages.Add(page);
            }
        }

        public async Task LoadMorePages(int skip = 0)
        {
            var pageLoad = 4;

            if (MangaBook.Pages.Count > AllPages.Count - pageLoad)
            {
                return;
            }

            for (int i = skip; i < skip + pageLoad; i++)
            {
                MangaBook.Pages.Add(AllPages[i]);
            }
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            MangaBook.Pages.Clear();
            await LoadMorePages();

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(MangaPage item)
        {
            return IsNotBusy && (!MangaBook.Pages.Any() || !MangaBook.Pages[0].IsLoading);
        }

        public async Task ExecuteLoadMoreCommand(MangaPage item)
        {
            IsBusy = true;

            var skip = MangaBook.Pages.Count;
            await LoadMorePages(skip);

            IsBusy = false;
        }
    }
}
