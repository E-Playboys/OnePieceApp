using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnePiece.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeftMenu : MasterDetailPage, IMasterDetailPageOptions
    {
        public LeftMenu()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;
    }
}
