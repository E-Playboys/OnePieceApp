using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.Anime
{
    public interface IAnimeApiService
    {
        Task<DataModels.Anime> GetAsync(int id);

        Task<DataModels.Anime> GetLatestEpisodeAsync();
        Task<DataModels.Anime> GetLatestTvSpecialAsync();
        Task<DataModels.Anime> GetLatestMovieAsync();
        Task<List<DataModels.Anime>> ListEpisodeBySeasonAsync(ListEpisodeBySeasonRequest rq);
        Task<List<DataModels.Anime>> ListTvSpecialsAsync(ListRequest rq);
        Task<List<DataModels.Anime>> ListMoviesAsync(ListRequest rq);
    }
}
