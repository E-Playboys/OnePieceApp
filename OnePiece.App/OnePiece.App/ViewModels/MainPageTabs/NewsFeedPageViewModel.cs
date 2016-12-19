using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OnePiece.App.Models;
using OnePiece.App.Services;
using Prism.Commands;

namespace OnePiece.App.ViewModels
{
    public class NewsFeedPageViewModel : BaseViewModel
    {
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<NewsFeed> LoadMoreCommand { get; set; }

        private ObservableCollection<NewsFeed> _newsFeeds = new ObservableCollection<NewsFeed>();
        public ObservableCollection<NewsFeed> NewsFeeds
        {
            get { return _newsFeeds; }
            set { SetProperty(ref _newsFeeds, value); }
        }

        public NewsFeedPageViewModel(IAppService appService) : base(appService)
        {
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<NewsFeed>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
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

        public async Task ExecuteLoadMoreCommand(NewsFeed item)
        {
            IsBusy = true;

            var skip = NewsFeeds.Count;
            await LoadNewsFeeds(skip);

            IsBusy = false;
        }

        public async Task LoadNewsFeeds(int skip)
        {
            var newsFeeds = new ObservableCollection<NewsFeed>()
            {
                new NewsFeed()
                {
                    Gif = "http://res.cloudinary.com/bongvl/image/upload/v1471705758/ccrrzjhmrgnlfxez65wy.mp4",
                    Image = "http://res.cloudinary.com/bongvl/image/upload/v1471705758/ccrrzjhmrgnlfxez65wy.png",
                    Width = 639,
                    Height = 387
                },
                new NewsFeed()
                {
                    Gif = "http://res.cloudinary.com/bongvl/image/upload/v1469802320/joo4zhusnhe9ygthpqsn.mp4",
                    Image = "http://res.cloudinary.com/bongvl/image/upload/v1469802320/joo4zhusnhe9ygthpqsn.png",
                    Width = 639,
                    Height = 431
                },
                new NewsFeed()
                {
                    Gif = "http://res.cloudinary.com/bongvl/image/upload/v1467095733/mfnyjj3mh2u3hapalyeh.mp4",
                    Image = "http://res.cloudinary.com/bongvl/image/upload/v1467095733/mfnyjj3mh2u3hapalyeh.png",
                    Width = 639,
                    Height = 357
                },
                new NewsFeed()
                {
                    Gif = "https://media.giphy.com/media/PjYfyarIEsNGM/giphy.mp4",
                    Image = "https://media.giphy.com/media/PjYfyarIEsNGM/giphy.gif",
                    Width = 480,
                    Height = 364
                },
            };

            foreach (var newsFeed in newsFeeds)
            {
                NewsFeeds.Add(newsFeed);
            }
        }
    }
}
