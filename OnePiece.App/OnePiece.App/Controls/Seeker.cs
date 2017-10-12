using System;
using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public class Seeker : Slider
    {
        // Events for external use (for example XAML)
        public event EventHandler TouchDown;
        public event EventHandler TouchUp;

        // Events called by renderers
        public EventHandler TouchDownEvent;
        public EventHandler TouchUpEvent;

        public Seeker()
        {
            TouchDownEvent = delegate
            {
                TouchDown?.Invoke(this, EventArgs.Empty);
            };
            TouchUpEvent = delegate
            {
                TouchUp?.Invoke(this, EventArgs.Empty);
            };
        }
    }
}
