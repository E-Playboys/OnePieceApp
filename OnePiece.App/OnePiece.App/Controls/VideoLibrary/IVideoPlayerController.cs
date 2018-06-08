using System;

namespace OnePiece.App.Controls.VideoLibrary
{ 
    public interface IVideoPlayerController
    {
        VideoStatus Status { set; get; }

        TimeSpan Duration { set; get; }
    }
}
