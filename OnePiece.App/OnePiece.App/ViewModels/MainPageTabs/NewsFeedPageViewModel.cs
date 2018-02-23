using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OnePiece.App.Models;
using OnePiece.App.Services;
using Prism.Commands;
using Xamarin.Forms;

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
            var nfs = await DependencyService.Get<IAzureDocumentDBService>().GetNewsFeeds();
            foreach (var nf in nfs)
            {
                NewsFeeds.Add(nf);
            }

            var newsFeeds = new ObservableCollection<NewsFeed>()
            {
                new NewsFeed()
                {
                    Gifs = new ObservableCollection<Gif>(){
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/fd8a4da6212583efe07c32be813dcd40/tumblr_oiqpc4Ktd71t1yvcko1_500.gif",
                            Width = 500,
                            Height = 500
                        }
                    }
                },
                new NewsFeed()
                {
                    Gifs = new ObservableCollection<Gif>()
                    {
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/dd2d46bfe7ce8659acb419fc5bd31075/tumblr_oinzd6QdMM1usyygio1_540.gif",
                            Width = 540,
                            Height = 304
                        },
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/8e854222867e41d572d041b4a6710e36/tumblr_oinzd6QdMM1usyygio2_540.gif",
                            Width = 540,
                            Height = 304
                        }
                    },
                    Title = "Nico Robin",
                    Description = "Sweet smile ;)."
                },
                new NewsFeed()
                {
                    Gifs = new ObservableCollection<Gif>()
                    {
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/3e2913c0c5035552db72db9a814266b4/tumblr_oik9hsWrgi1v6xsm2o2_540.gif",
                            Width = 540,
                            Height = 304
                        },
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/dda0ca8d51ff2a7ceacacaa9a530bbf4/tumblr_oik9hsWrgi1v6xsm2o1_540.gif",
                            Width = 540,
                            Height = 304
                        },
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/ae87f92f2bfcf1d02ff3a175f7ed3e61/tumblr_oik9hsWrgi1v6xsm2o4_540.gif",
                            Width = 540,
                            Height = 304
                        },
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/09dd472ed41138fd587ac56103a32048/tumblr_oik9hsWrgi1v6xsm2o6_540.gif",
                            Width = 540,
                            Height = 304
                        },
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/fb92bb2da1e0d959e4cfe23261483aa4/tumblr_oik9hsWrgi1v6xsm2o3_540.gif",
                            Width = 540,
                            Height = 304
                        },
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/11458265ca6144f92924c0c391a4a2de/tumblr_oik9hsWrgi1v6xsm2o5_540.gif",
                            Width = 540,
                            Height = 304
                        }
                    },
                    Title = "This is a real title"
                },
                new NewsFeed()
                {
                    Gifs = new ObservableCollection<Gif>()
                    {
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/3c563608501ef46c4a1410ae7974d39c/tumblr_oigawa1j5t1sg8uefo4_540.gif",
                            Width = 540,
                            Height = 300
                        },
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/78e870fa2629da2c7daed71810303820/tumblr_oigawa1j5t1sg8uefo1_540.gif",
                            Width = 540,
                            Height = 300
                        },
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/9011deefab11c3294957d9829c8390a7/tumblr_oigawa1j5t1sg8uefo3_540.gif",
                            Width = 540,
                            Height = 300
                        },
                        new Gif()
                        {
                            Url = "https://68.media.tumblr.com/8ffabf156de3853deb49819fb4be790f/tumblr_oigawa1j5t1sg8uefo2_540.gif",
                            Width = 540,
                            Height = 300
                        }
                    },
                    Title = "This is a real title",
                    Description = "This is a real description. Sure, it's real. It's not fake description. Because it's real data."
                },
            };

            foreach (var newsFeed in newsFeeds)
            {
                NewsFeeds.Add(newsFeed);
            }
        }
    }
}
