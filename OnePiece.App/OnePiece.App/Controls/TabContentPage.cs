using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public class TabContentPage : ContentPage
    {
        public new void SendAppearing()
        {
            OnAppearing();
        }

        public new void SendDisappearing()
        {
            OnDisappearing();
        }
    }
}
