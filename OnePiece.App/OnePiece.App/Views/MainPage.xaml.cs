using FormsPlugin.Iconize;

namespace OnePiece.App.Views
{
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
