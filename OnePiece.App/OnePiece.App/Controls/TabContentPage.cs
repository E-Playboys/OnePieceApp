using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public class TabContentPage : ContentPage
    {
        public void SendAppearing()
        {
            OnAppearing();
        }

        public void SendDisappearing()
        {
            OnDisappearing();
        }
    }
}
