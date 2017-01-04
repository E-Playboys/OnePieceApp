using OnePiece.App.Models;
using OnePiece.App.Services;
using OnePiece.App.Views.MainPageTabs;
using Prism.Commands;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnePiece.App.ViewModels
{
    class MangaPageViewModel : BaseViewModel
    {
        public object LastTappedItem { get; set; }

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

            var mangaReaderPage = new MangaReaderPage()
            {
                BindingContext = new MangaReaderPageViewModel(AppService)
                {
                    MangaBook = item
                },
                BackgroundColor = Color.White,
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
            for (int i = skip; i < skip + 20; i++)
            {
                var book = new MangaBook
                {
                    Title = $"Chapter {i}",
                    ImageUrl = "http://st.thichtruyentranh.com/images/icon/0048/one-piece1416866288.jpg"
                };
                MangaBooks.Add(book);
            }
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
