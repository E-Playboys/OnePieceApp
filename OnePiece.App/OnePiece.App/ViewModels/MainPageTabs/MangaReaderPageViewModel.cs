using System.Threading.Tasks;
using OnePiece.App.Models;
using OnePiece.App.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OnePiece.App.ViewModels
{
    public class MangaReaderPageViewModel : BaseViewModel
    {
        public MangaBook MangaBook { get; set; } = new MangaBook();

        public MangaReaderPageViewModel(IAppService appService) : base(appService)
        {
        }

        public async Task LoadMangaPages(int skip = 0)
        {
            MangaBook.Pages.Clear();

            for (int i = skip; i < skip + 20; i++)
            {
                var book = new MangaPage
                {
                    Title = $"Page {i}",
                    ImageUrl = "http://i.imgur.com/mfKNPEF.jpg"
                };
                MangaBook.Pages.Add(book);
            }
        }
    }
}
