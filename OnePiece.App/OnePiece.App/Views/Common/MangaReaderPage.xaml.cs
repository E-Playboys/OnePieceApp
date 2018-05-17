using OnePiece.App.Services;
using OnePiece.App.ViewModels;
using OnePiece.App.DataModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnePiece.App.Views
{
    public partial class MangaReaderPage : PopupPage
    {
        private readonly MangaReaderPageViewModel _context;

        private CancellationTokenSource _hideInfoCancelSource { get; set; } = new CancellationTokenSource();

        public MangaReaderPage(int chapterNumber)
        {
            InitializeComponent();

            _context = BindingContext as MangaReaderPageViewModel;
            if (_context != null)
            {
                _context.CurrentMangaChapterNumber = chapterNumber;
            }
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();

            // Show status bar
            DependencyService.Get<IStatusBar>().ShowStatusBar();
        }

        private void ShowHideControlBars(object sender, EventArgs e)
        {
            //TopBar.IsVisible = !TopBar.IsVisible;
            //BottomBar.IsVisible = !BottomBar.IsVisible;
        }

        private async Task OnPrevChapterClicked(object sender, EventArgs e)
        {
            var context = BindingContext as MangaReaderPageViewModel;
            if (context == null) return;
            await context.GoPrevChapter();
        }

        private async Task OnNextChapterClicked(object sender, EventArgs e)
        {
            var context = BindingContext as MangaReaderPageViewModel;
            if (context == null) return;
            await context.GoNextChapter();
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as MangaReaderPageViewModel;
            if (context != null)
            {
                await context.LoadManga();
            }
            base.OnAppearing();

            // Hide status bar
            //DependencyService.Get<IStatusBar>().HideStatusBar();

            //await HideInfoBars(_hideInfoCancelSource.Token);
        }

        private async Task HideInfoBars(CancellationToken token)
        {
            // Hide info
            await Task.Delay(2000);

            if (token.IsCancellationRequested) return;

            //TopBar.IsVisible = false;
            //BottomBar.IsVisible = false;
        }

        private async Task ShowInfoBars(CancellationTokenSource cancelSource)
        {
            cancelSource.Cancel();

            //TopBar.IsVisible = true;
            //BottomBar.IsVisible = true;
        }
    }
}
