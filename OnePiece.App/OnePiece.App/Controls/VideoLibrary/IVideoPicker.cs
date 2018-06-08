using System;
using System.Threading.Tasks;

namespace OnePiece.App.Controls.VideoLibrary
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();
    }
}
