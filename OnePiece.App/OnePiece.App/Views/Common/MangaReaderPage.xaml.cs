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

            CloseWhenBackgroundIsClicked = true;

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

        protected override async void OnAppearing()
        {
            var context = BindingContext as MangaReaderPageViewModel;
            if (context != null)
            {
                await context.LoadManga();
            }
            base.OnAppearing();

            // Hide status bar
            DependencyService.Get<IStatusBar>().HideStatusBar();
        }
    }
}
