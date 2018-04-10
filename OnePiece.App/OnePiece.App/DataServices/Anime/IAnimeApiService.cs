using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.Anime
{
    public interface IAnimeApiService
    {
        Task<List<DataModels.Anime>> ListEpisodeBySeasonAsync(ListEpisodeBySeasonRequest rq);

        Task<DataModels.Anime> GetAsync(int id);
    }
}
