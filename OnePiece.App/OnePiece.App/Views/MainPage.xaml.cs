using FormsPlugin.Iconize;
using Xamarin.Forms.Xaml;

namespace OnePiece.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : IconTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Title = CurrentPage?.Title;
        }
    }
}
