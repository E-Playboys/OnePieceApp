using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.Season
{
    public class SeasonApiService : ISeasonApiService
    {
        private readonly IRequestProvider _requestProvider;

        public SeasonApiService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<List<DataModels.Season>> ListAsync(ListRequest rq)
        {
            var seasons = await _requestProvider.GetAsync<List<DataModels.Season>>($"{AppSettings.WEB_API_URL}/Seasons?skip={rq.Skip}&take={rq.Take}");

            return seasons;
        }

        public async Task<DataModels.Season> GetAsync(int id)
        {
            var season = await _requestProvider.GetAsync<DataModels.Season>($"{AppSettings.WEB_API_URL}/Seasons/{id}");

            return season;
        }
    }
}
