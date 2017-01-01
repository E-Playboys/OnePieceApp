using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePiece.App.Models;
using OnePiece.App.Services;
using Prism.Commands;

namespace OnePiece.App.ViewModels
{
    public class VideoPageViewModel : BaseViewModel
    {
        private ObservableCollection<Video> _videos;
        public ObservableCollection<Video> Videos
        {
            get { return _videos ?? (_videos = new ObservableCollection<Video>()); }
            set { SetProperty(ref _videos, value); }
        }

        public DelegateCommand LoadVideosCommand { get; set; }
        public DelegateCommand<Video> ViewVideoCommand { get; set; }

        public VideoPageViewModel(IAppService appService) : base(appService)
        {
            LoadVideosCommand = new DelegateCommand(async () => await ExecuteLoadVideosCommandAsync());
            ViewVideoCommand = new DelegateCommand<Video>(async (video) => await ExecuteViewVideoCommandAsync(video));
        }

        public async Task ExecuteLoadVideosCommandAsync()
        {
            IsBusy = true;

            var videos = await LoadVideosAsync();
            foreach (var video in videos)
            {
                Videos.Add(video);
            }

            IsBusy = false;
        }

        private async Task ExecuteViewVideoCommandAsync(Video video)
        {
            
        }

        private async Task<List<Video>>  LoadVideosAsync()
        {
            var fakeData = new List<Video>
            {
                new Video { Title = "AAA"},
                new Video { Title = "AAA"},
                new Video { Title = "AAA"},
                new Video { Title = "AAA"},
                new Video { Title = "AAA"},
                new Video { Title = "AAA"}
            };

            return fakeData;
        }
    }
}
