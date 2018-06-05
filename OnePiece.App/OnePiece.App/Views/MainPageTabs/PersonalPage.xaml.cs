using OnePiece.App.Controls;
using OnePiece.App.ViewModels;

namespace OnePiece.App.Views
{
    public partial class PersonalPage : TabContentPage
    {
        private readonly PersonalPageViewModel _viewModel;

        public PersonalPage()
        {
            InitializeComponent();

            _viewModel = (PersonalPageViewModel) BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.LoadData();
        }
    }
}
