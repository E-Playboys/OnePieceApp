using Newtonsoft.Json;
using OnePiece.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnePiece.App.Services.Manga
{
    public interface IMangaService
    {
        Task<List<MangaChapter>> ListChaptersAsync(ListMangaChaptersRq rq);
        Task<List<MangaVolume>> ListVolumesAsync(ListVolumesRq rq);
        Task<MangaVolume> GetVolumeAsync(GetVolumeRq rq);
        Task<GetChapterRs> GetChapterAsync(GetChapterRq rq);
        Task<int> ListChapterCount();
    }

    public class MangaService : IMangaService
    {
        private HttpClient client = new HttpClient();

        private const string apiUrl = AppSettings.WEB_API_URL + "Manga/";

        //private static Account cloudinaryAcc = new Account("ewiki-io", "742665545865257", "S1PQu71FWZ8l8b-PjsLRzTtoeLg");

        public MangaService()
        {
        }

        public Task<int> ListChapterCount()
        {
            throw new NotImplementedException();
        }

        public async Task<List<MangaChapter>> ListChaptersAsync(ListMangaChaptersRq rq)
        {
            var chapters = new List<MangaChapter>();
            for(var i = 0; i <= 20; i++)
            {
                chapters.Add(new MangaChapter()
                {
                    Avatar = "http://static.hdonline.vn/i/resources/new/film/215x311/2016/07/20/one-piece-film-gold.jpg",
                    Name = "Luffy thành vua hải tặc",
                    Number = i,
                    Id = i
                });
            }
            return chapters;

            const string apiAction = "ListChapters";
            var requestUrl = $"{apiUrl}{apiAction}?skip={rq.Skip}&take={rq.Take}&volumeId={rq.VolumeId}";
            string response = string.Empty;

            try
            {
                response = await client.GetStringAsync(requestUrl);
            }
            catch (Exception ex)
            {

            }

            var items = JsonConvert.DeserializeObject<List<MangaChapter>>(response);

            return items;
        }

        public async Task<List<MangaVolume>> ListVolumesAsync(ListVolumesRq rq)
        {
            const string apiAction = "ListVolumes";
            var requestUrl = $"{apiUrl}{apiAction}?skip={rq.Skip}&take={rq.Take}";
            var response = await client.GetStringAsync(requestUrl);

            var items = JsonConvert.DeserializeObject<List<MangaVolume>>(response);

            return items;
        }

        public async Task<MangaVolume> GetVolumeAsync(GetVolumeRq rq)
        {
            const string apiAction = "GetVolume";
            var requestUrl = $"{apiUrl}{apiAction}?VolumeId={rq.VolumeId}";
            var response = await client.GetStringAsync(requestUrl);

            var items = JsonConvert.DeserializeObject<MangaVolume>(response);

            return items;
        }

        public async Task<GetChapterRs> GetChapterAsync(GetChapterRq rq)
        {
            const string apiAction = "GetChapter";
            var requestUrl = $"{apiUrl}{apiAction}?ChapterId={rq.ChapterId}";
            var response = await client.GetStringAsync(requestUrl);

            var items = JsonConvert.DeserializeObject<GetChapterRs>(response);

            return items;
        }
    }
}
