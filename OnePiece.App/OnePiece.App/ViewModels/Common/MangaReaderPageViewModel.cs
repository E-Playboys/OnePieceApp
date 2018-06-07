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
using OnePiece.App.LocalData;

namespace OnePiece.App.ViewModels
{
    public class MangaReaderPageViewModel : BaseViewModel
    {
        private readonly IAppDataStorage _appDataStorage;
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

        private int _totalPage;
        public int TotalPage {
            get => _totalPage;
            set => SetProperty(ref _totalPage, value);
        }

        public int CurrentMangaChapterNumber { get; set; }

        private int _currentIndex;
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set {
                CurrentPageNumber = value + 1;
                SetProperty(ref _currentIndex, value);
            }
        }

        private int _currentPageNumber;
        public int CurrentPageNumber {
            get => _currentPageNumber;
            set => SetProperty(ref _currentPageNumber, value);
        }

        private bool _isCardsViewVisible;
        public bool IsCardsViewVisible
        {
            get { return _isCardsViewVisible; }
            set { SetProperty(ref _isCardsViewVisible, value); }
        }

        private bool _isControlVisible;
        public bool IsControlVisible
        {
            get { return _isControlVisible; }
            set { SetProperty(ref _isControlVisible, value); }
        }

        public DelegateCommand ToggleControlCommand { get; set; }
        public DelegateCommand ToggleCardsViewCommand { get; set; }
        public DelegateCommand NextChapterCommand { get; set; }
        public DelegateCommand PrevChapterCommand { get; set; }

        public MangaReaderPageViewModel(IAppService appService, IAppDataStorage appDataStorage, IMangaApiService mangaService) : base(appService)
        {
            ToggleControlCommand = new DelegateCommand(ExecuteToggleControlCommand, CanExecuteCommand);
            ToggleCardsViewCommand = new DelegateCommand(ExecuteToggleCardsViewCommand, CanExecuteCommand);
            NextChapterCommand = new DelegateCommand(async () => await ExecuteNextChapterCommand(), CanExecuteCommand);
            PrevChapterCommand = new DelegateCommand(async () => await ExecutePrevChapterCommand(), CanExecuteCommand);

            _appDataStorage = appDataStorage;
            _mangaService = mangaService;
        }

        public async Task LoadManga(int next = 0, int previous = 0)
        {
            IsBusy = true;

            var manga = await _mangaService.GetByChapterNumberAsync(CurrentMangaChapterNumber, next, previous);
            if(manga != null)
            {
                _appDataStorage.SaveManga(manga);

                Manga = manga;
                CurrentMangaChapterNumber = manga.ChapterNumber;

                var pages = manga.PagesJson?.Split('|').Where(x => !string.IsNullOrEmpty(x));

                TotalPage = pages.Count();

                MangaPages.Clear();
                MangaPages.AddRange(pages.Select(x => new MangaPage { Url = x }));
            }

            IsBusy = false;
        }

        public bool CanExecuteCommand()
        {
            return IsNotBusy;
        }

        public void ExecuteToggleControlCommand()
        {
            IsControlVisible = !IsControlVisible;
        }

        public void ExecuteToggleCardsViewCommand()
        {
            IsCardsViewVisible = !IsCardsViewVisible;
        }

        public async Task ExecuteNextChapterCommand()
        {
            await LoadManga(next: 1);
        }

        public async Task ExecutePrevChapterCommand()
        {
            await LoadManga(previous: 1);
        }
    }
}
