using FormsPlugin.Iconize;
using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public class TabNavigationPage : IconNavigationPage
    {
        private readonly TabContentPage _root;

        public TabNavigationPage(Page root) : base(root)
        {
            _root = root as TabContentPage;
        }

        public void SendAppearing()
        {
            _root?.SendAppearing();
        }

        public void SendDisappearing()
        {
            _root?.SendDisappearing();
        }
    }
}
