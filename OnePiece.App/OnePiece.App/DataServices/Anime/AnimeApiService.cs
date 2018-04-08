using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.App.DataServices.Anime
{
    public class AnimeApiService
    {
        private readonly IRequestProvider _requestProvider;

        public AnimeApiService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }


    }
}
