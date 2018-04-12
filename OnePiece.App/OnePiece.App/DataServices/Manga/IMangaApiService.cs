using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.Manga
{
    public interface IMangaApiService
    {
        Task<List<DataModels.Manga>> ListAsync(ListRequest rq);

        Task<DataModels.Manga> GetAsync(int id);
    }
}
