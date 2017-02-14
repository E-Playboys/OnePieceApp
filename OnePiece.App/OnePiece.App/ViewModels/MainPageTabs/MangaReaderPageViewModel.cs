using OnePiece.App.Models;
using OnePiece.App.Services;
using OnePiece.App.Services.Manga;
using Prism.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePiece.App.ViewModels
{
    public class MangaReaderPageViewModel : BaseViewModel
    {
        private MangaChapter _mangaChapter = new MangaChapter();
        public MangaChapter MangaChapter
        {
            get
            {
                return _mangaChapter;
            }
            set
            {
                SetProperty(ref _mangaChapter, value);
            }
        }

        public int MangaChapterId { get; set; }
        public int? PrevChapterId { get; set; }
        public int? NextChapterId { get; set; }

        public Queue<MangaImage> AllPages { get; set; } = new Queue<MangaImage>();
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<MangaImage> LoadMoreCommand { get; set; }

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

        public int TotalPageCount { get; set; }

        public string CurrentPageString
        {
            get
            {
                return CurrentPageNum + "/" + TotalPageCount;
            }
        }

        private readonly IMangaService _mangaService;

        public MangaReaderPageViewModel(IAppService appService, IMangaService mangaService) : base(appService)
        {
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<MangaImage>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);

            _mangaService = mangaService;
        }

        public async Task GoNextChapter()
        {
            if (!NextChapterId.HasValue) return;

            MangaChapterId = NextChapterId.Value;
            await InitializeChapter();
            await LoadMorePages();
        }

        public async Task GoPrevChapter()
        {
            if (!PrevChapterId.HasValue) return;

            MangaChapterId = PrevChapterId.Value;
            await InitializeChapter();
            await LoadMorePages();
        }

        public async Task InitializeChapter()
        {
            if (MangaChapterId > 0)
            {
                var rs = await _mangaService.GetChapterAsync(new GetChapterRq
                {
                    ChapterId = MangaChapterId
                });

                if (rs.Chapter == null) return;

                NextChapterId = rs.NextChapterId;
                PrevChapterId = rs.PrevChapterId;

                TotalPageCount = rs.Chapter.MangaImages.Count;
                AllPages = new Queue<MangaImage>(rs.Chapter.MangaImages);
                rs.Chapter.MangaImages.Clear();
                MangaChapter = rs.Chapter;
            }
        }

        public async Task LoadMorePages(int skip = 0)
        {
            var pageLoad = 4;
            var popCount = 0;

            while (AllPages.Count > 0)
            {
                if (popCount >= pageLoad) break;

                var nextPage = AllPages.Dequeue();
                nextPage.ImageWidth = App.ScreenWidth;
                MangaChapter.MangaImages.Add(nextPage);
                popCount++;
            }
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            await InitializeChapter();
            await LoadMorePages();

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(MangaImage item)
        {
            return IsNotBusy && (!MangaChapter.MangaImages.Any() || !MangaChapter.MangaImages[0].IsLoading);
        }

        public async Task ExecuteLoadMoreCommand(MangaImage item)
        {
            IsBusy = true;

            var skip = MangaChapter.MangaImages.Count;
            await LoadMorePages(skip);

            IsBusy = false;
        }
    }
}
