using OnePiece.App.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;

namespace OnePiece.App.Views.MainPageTabs
{
    public partial class MangaReaderPage
    {
        public MangaReaderPage()
        {
            InitializeComponent();
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as MangaReaderPageViewModel;
            if (context != null && !context.MangaBook.Pages.Any())
            {
                await context.LoadMangaPages();
            }
            base.OnAppearing();
        }
    }
}
