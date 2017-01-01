using OnePiece.App.Controls;
using OnePiece.App.ViewModels;

namespace OnePiece.App.Views
{
    public partial class VideoPage : TabContentPage
    {
        private readonly VideoPageViewModel _viewModel;

        public VideoPage()
        {
            InitializeComponent();

            _viewModel = (VideoPageViewModel) BindingContext;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.ExecuteLoadVideosCommandAsync();
        }
    }
}
