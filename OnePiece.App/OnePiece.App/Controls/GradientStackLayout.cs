using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public class GradientStackLayout : StackLayout
    {
        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public bool IsHorizontal { get; set; }
    }
}
