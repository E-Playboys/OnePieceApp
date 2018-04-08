using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.NewsFeed
{
    public interface INewsFeedApiService
    {
        Task<List<DataModels.NewsFeed>> ListAsync(ListRequest rq);

        Task<DataModels.NewsFeed> GetAsync(int id);
    }
}
