using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.NewsFeed
{
    public class NewsFeedApiService : INewsFeedApiService
    {
        private readonly IRequestProvider _requestProvider;

        public NewsFeedApiService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<List<DataModels.NewsFeed>> ListAsync(ListRequest rq)
        {
            var feeds = await _requestProvider.GetAsync<List<DataModels.NewsFeed>>($"{AppSettings.WEB_API_URL}/Feeds?skip={rq.Skip}&take={rq.Take}");

            return feeds;
        }

        public async Task<DataModels.NewsFeed> GetAsync(int id)
        {
            var feed = await _requestProvider.GetAsync<DataModels.NewsFeed>($"{AppSettings.WEB_API_URL}/Feeds/{id}");

            return feed;
        }
    }
}
