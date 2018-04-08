using System.IO;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        Task PostAsync(string uri, object data, string token = "", string header = "");

        Task<TResult> PostAsync<TData, TResult>(string uri, TData data, string token = "", string header = "");

        Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);

        Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task DeleteAsync(string uri, string token = "");
        Task<TResult> UploadAsync<TResult>(string uri, Stream data, string fileName, string token = "", string header = "");
    }
}