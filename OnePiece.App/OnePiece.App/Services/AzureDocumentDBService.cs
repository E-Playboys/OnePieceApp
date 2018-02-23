
using OnePiece.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnePiece.App.Services
{
    public interface IAzureDocumentDBService
    {
        Task<List<NewsFeed>> GetNewsFeeds();
    }
}
