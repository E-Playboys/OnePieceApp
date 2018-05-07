using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OnePiece.App.DataModels;
using OnePiece.App.DataServices.NewsFeed;
using OnePiece.App.Services;
using Prism.Commands;

namespace OnePiece.App.ViewModels
{
    public class NewsFeedPageViewModel : BaseViewModel
    {
        private readonly INewsFeedApiService _newsFeedApiService;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<NewsFeed> LoadMoreCommand { get; set; }

        private ObservableCollection<NewsFeed> _newsFeeds = new ObservableCollection<NewsFeed>();
        public ObservableCollection<NewsFeed> NewsFeeds
        {
            get { return _newsFeeds; }
            set { SetProperty(ref _newsFeeds, value); }
        }

        public NewsFeedPageViewModel(IAppService appService, INewsFeedApiService newsFeedApiService) : base(appService)
        {
            _newsFeedApiService = newsFeedApiService;
            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = new DelegateCommand<NewsFeed>(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async void ExecuteRefreshCommand()
        {
            IsBusy = true;

            NewsFeeds = new ObservableCollection<NewsFeed>();
            await LoadNewsFeeds(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(NewsFeed item)
        {
            return IsNotBusy;
        }

        public async void ExecuteLoadMoreCommand(NewsFeed item)
        {
            IsBusy = true;

            var skip = NewsFeeds.Count;
            await LoadNewsFeeds(skip);

            IsBusy = false;
        }

        public async Task LoadNewsFeeds(int skip)
        {
            var feeds = await _newsFeedApiService.ListAsync(new DataServices.ListRequest {Skip = skip});

            foreach (var newsFeed in feeds)
            {
                NewsFeeds.Add(newsFeed);
            }
        }
    }
}
