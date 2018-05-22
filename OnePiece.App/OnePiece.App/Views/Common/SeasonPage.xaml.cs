using OnePiece.App.DataModels;
using OnePiece.App.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace OnePiece.App.Views
{
    public partial class SeasonPage : PopupPage
    {
        private readonly SeasonPageViewModel _viewModel;

        public SeasonPage(Season season)
        {
            InitializeComponent();

            _viewModel = (SeasonPageViewModel) BindingContext;
            _viewModel.Season = season;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            VideoCover.HeightRequest = (int)(width * 9 / 16);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.LoadAsync();
        }
    }
}
