using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.Season
{
    public interface ISeasonApiService
    {
        Task<List<DataModels.Season>> ListAsync(ListRequest rq);

        Task<DataModels.Season> GetAsync(int id);
    }
}
