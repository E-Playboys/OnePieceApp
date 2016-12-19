using System.Linq;
using OnePiece.App.Controls;
using OnePiece.App.ViewModels;

namespace OnePiece.App.Views
{
    public partial class NewsFeedPage : TabContentPage
    {
        public NewsFeedPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as NewsFeedPageViewModel;
            if (context != null && !context.NewsFeeds.Any()) await context.LoadNewsFeeds(0);
            base.OnAppearing();
        }
    }
}
