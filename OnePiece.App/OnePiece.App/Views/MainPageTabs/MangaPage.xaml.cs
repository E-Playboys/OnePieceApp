using OnePiece.App.Controls;
using OnePiece.App.Models;
using OnePiece.App.ViewModels;
using System.Linq;

namespace OnePiece.App.Views
{
    public partial class MangaPage : TabContentPage
    {
        public MangaPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as MangaPageViewModel;
            if (context != null && !context.MangaBooks.Any()) await context.LoadMangaBooks();
            base.OnAppearing();
        }
    }
}
