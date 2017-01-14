using OnePiece.App.Controls;
using OnePiece.App.ViewModels;
using Xamarin.Forms;

namespace OnePiece.App.Views
{
    public partial class AnimePage : TabbedPage
    {
        private readonly AnimePageViewModel _viewModel;

        public AnimePage()
        {
            InitializeComponent();

            //_viewModel = (AnimePageViewModel) BindingContext;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //await _viewModel.ExecuteLoadAnimesCommandAsync();
        }
    }
}
