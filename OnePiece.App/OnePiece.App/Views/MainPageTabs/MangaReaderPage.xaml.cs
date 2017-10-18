using OnePiece.App.Services;
using OnePiece.App.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnePiece.App.Views.MainPageTabs
{
    public partial class MangaReaderPage : PopupPage
    {
        private CancellationTokenSource _hideInfoCancelSource { get; set; } = new CancellationTokenSource();

        public MangaReaderPage()
        {
            InitializeComponent();
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();

            // Show status bar
            DependencyService.Get<IStatusBar>().ShowStatusBar();
        }

        private async Task OnItemTapped(object sender, EventArgs e)
        {
            await ShowInfoBars(_hideInfoCancelSource);
            _hideInfoCancelSource = new CancellationTokenSource();
            await HideInfoBars(_hideInfoCancelSource.Token);
        }

        private async Task OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var context = BindingContext as MangaReaderPageViewModel;
            var currentPage = e.Item as Models.MangaImage;

            if (currentPage == null || context == null) return;

            var index = context.MangaChapter.MangaImages.IndexOf(currentPage);
            context.CurrentPageNum = index + 1;

            await ShowInfoBars(_hideInfoCancelSource);
            _hideInfoCancelSource = new CancellationTokenSource();
            await HideInfoBars(_hideInfoCancelSource.Token);
        }

        private async Task OnPrevChapterClicked(object sender, EventArgs e)
        {
            var context = BindingContext as MangaReaderPageViewModel;
            if (context == null) return;
            context.AllPages.Clear();
            context.MangaChapter.MangaImages.Clear();
            await context.GoPrevChapter();
        }

        private async Task OnNextChapterClicked(object sender, EventArgs e)
        {
            var context = BindingContext as MangaReaderPageViewModel;
            if (context == null) return;
            context.AllPages.Clear();
            context.MangaChapter.MangaImages.Clear();
            await context.GoNextChapter();
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as MangaReaderPageViewModel;
            if (context != null)
            {
                context.MangaChapter.MangaImages.Clear();
                await context.InitializeChapter();
                await context.LoadMorePages();
            }
            base.OnAppearing();

            // Hide status bar
            DependencyService.Get<IStatusBar>().HideStatusBar();

            await HideInfoBars(_hideInfoCancelSource.Token);
        }

        private async Task HideInfoBars(CancellationToken token)
        {
            // Hide info
            await Task.Delay(2000);

            if (token.IsCancellationRequested) return;

            TopBar.IsVisible = false;
            BottomBar.IsVisible = false;
        }

        private async Task ShowInfoBars(CancellationTokenSource cancelSource)
        {
            cancelSource.Cancel();

            TopBar.IsVisible = true;
            BottomBar.IsVisible = true;
        }
    }
}
