using OnePiece.App.DataModels;
using OnePiece.App.Services;
using OnePiece.App.Services.Manga;
using Prism.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnePiece.App.DataServices.Manga;
using Plugin.DeviceInfo;
using OnePiece.App.Utilities;

namespace OnePiece.App.ViewModels
{
    public class MangaReaderPageViewModel : BaseViewModel
    {
        private readonly IMangaApiService _mangaService;

        private Manga _manga;
        public Manga Manga
        {
            get { return _manga; }
            set { SetProperty(ref _manga, value); }
        }

        private ObservableRangeCollection<MangaPage> _mangaPages;
        public ObservableRangeCollection<MangaPage> MangaPages
        {
            get { return _mangaPages ?? (_mangaPages = new ObservableRangeCollection<MangaPage>()); }
            set { SetProperty(ref _mangaPages, value); }
        }

        public int TotalPage => MangaPages.Count;

        public int CurrentMangaChapterNumber { get; set; }

        private int _currentPageNumber;
        public int CurrentPageNumber
        {
            get { return _currentPageNumber; }
            set { SetProperty(ref _currentPageNumber, value); }
        }

        private bool _isControlVisible;
        public bool IsControlVisible
        {
            get { return _isControlVisible; }
            set { SetProperty(ref _isControlVisible, value); }
        }

        public DelegateCommand ToggleControlCommand { get; set; }
        public DelegateCommand LoadMoreCommand { get; set; }

        public MangaReaderPageViewModel(IAppService appService, IMangaApiService mangaService) : base(appService)
        {
            ToggleControlCommand = new DelegateCommand(ExecuteToggleControlCommand, CanExecuteToggleControlCommand);
            LoadMoreCommand = new DelegateCommand(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);

            _mangaService = mangaService;
        }

        public async Task GoNextChapter()
        {
            await LoadManga(next: 1);
        }

        public async Task GoPrevChapter()
        {
            await LoadManga(previous: 1);
        }

        public async Task LoadManga(int next = 0, int previous = 0)
        {
            IsBusy = true;

            var manga = await _mangaService.GetByChapterNumberAsync(CurrentMangaChapterNumber, next, previous);
            if(manga != null)
            {
                Manga = manga;
                CurrentMangaChapterNumber = manga.ChapterNumber;

                MangaPages.Clear();
                MangaPages.AddRange(manga.MangaPages);
            }

            IsBusy = false;
        }

        public bool CanExecuteToggleControlCommand()
        {
            return IsNotBusy;
        }

        public void ExecuteToggleControlCommand()
        {
            IsBusy = true;

            IsControlVisible = !IsControlVisible;

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand()
        {
            return IsNotBusy;
        }

        public async void ExecuteLoadMoreCommand()
        {
            //IsBusy = true;

            //LoadManga();

            //IsBusy = false;
        }
    }
}
