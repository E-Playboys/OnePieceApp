using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.Manga
{
    public class MangaApiService : IMangaApiService
    {
        private readonly IRequestProvider _requestProvider;

        public MangaApiService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<List<DataModels.Manga>> ListAsync(ListRequest rq)
        {
            var mangas = await _requestProvider.GetAsync<List<DataModels.Manga>>($"{AppSettings.WEB_API_URL}/Mangas/List?skip={rq.Skip}&take={rq.Take}");

            return mangas;
        }

        public async Task<DataModels.Manga> GetAsync(int id)
        {
            var manga = await _requestProvider.GetAsync<DataModels.Manga>($"{AppSettings.WEB_API_URL}/Mangas/Get?id={id}");

            return manga;
        }
    }
}
