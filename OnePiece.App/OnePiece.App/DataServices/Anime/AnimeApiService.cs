using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.Anime
{
    public class AnimeApiService : IAnimeApiService
    {
        private readonly IRequestProvider _requestProvider;

        public AnimeApiService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<List<DataModels.Anime>> ListEpisodeBySeasonAsync(ListEpisodeBySeasonRequest rq)
        {
            var animes = await _requestProvider.GetAsync<List<DataModels.Anime>>($"{AppSettings.WEB_API_URL}/Episodes/ListBySeason?seasonId={rq.SeasonId}&skip={rq.Skip}&take={rq.Take}");

            return animes;
        }

        public async Task<List<DataModels.Anime>> ListStoriesAsync(ListRequest rq)
        {
            var animes = await _requestProvider.GetAsync<List<DataModels.Anime>>($"{AppSettings.WEB_API_URL}/Episodes/ListStories?skip={rq.Skip}&take={rq.Take}");

            return animes;
        }

        public async Task<List<DataModels.Anime>> ListTvSpecialsAsync(ListRequest rq)
        {
            var animes = await _requestProvider.GetAsync<List<DataModels.Anime>>($"{AppSettings.WEB_API_URL}/Episodes/ListTvSpecials?skip={rq.Skip}&take={rq.Take}");

            return animes;
        }

        public async Task<List<DataModels.Anime>> ListMoviesAsync(ListRequest rq)
        {
            var animes = await _requestProvider.GetAsync<List<DataModels.Anime>>($"{AppSettings.WEB_API_URL}/Episodes/ListMovies?skip={rq.Skip}&take={rq.Take}");

            return animes;
        }

        public async Task<DataModels.Anime> GetAsync(int id)
        {
            var anime = await _requestProvider.GetAsync<DataModels.Anime>($"{AppSettings.WEB_API_URL}/Episodes/Get?id={id}");

            return anime;
        }

        public async Task<DataModels.Anime> GetLatestEpisodeAsync()
        {
            var anime = await _requestProvider.GetAsync<DataModels.Anime>($"{AppSettings.WEB_API_URL}/Episodes/GetLatestEpisode");

            return anime;
        }

        public async Task<DataModels.Anime> GetLatestTvSpecialAsync()
        {
            var anime = await _requestProvider.GetAsync<DataModels.Anime>($"{AppSettings.WEB_API_URL}/Episodes/GetLatestTvSpecial");

            return anime;
        }

        public async Task<DataModels.Anime> GetLatestMovieAsync()
        {
            var anime = await _requestProvider.GetAsync<DataModels.Anime>($"{AppSettings.WEB_API_URL}/Episodes/GetLatestMovie");

            return anime;
        }
    }
}
