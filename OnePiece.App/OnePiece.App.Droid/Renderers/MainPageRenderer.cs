using System.Collections.Generic;
using System.Linq;
using Android.Views;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;
using OnePiece.App.Controls;
using OnePiece.App.Droid.Renderers;
using OnePiece.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Plugin.Iconize;
using Plugin.Iconize.Droid.Controls;

[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageRenderer))]
namespace OnePiece.App.Droid.Renderers
{
    /// <summary>
    /// This renderer is for replacing the tab with Xamarin Android bottom bar (https://github.com/pocheshire/BottomNavigationBar)
    /// To integrate into Xamarin Forms https://asyncawait.wordpress.com/2016/06/16/bottom-menu-for-xamarin-forms-android/
    /// The page structure of the sample is NavigationPage -> TabbedPage
    /// Our page structure is different which is to support navigation within each tab TabbedPage -> NavigationPage
    /// So it causes some miscalculating in the layout, so there are some TRICKY codes in OnLayout which needs to be fixed if possible
    /// </summary>
    internal class MainPageRenderer : VisualElementRenderer<MainPage>, IOnTabClickListener
    {
        private BottomBar _bottomBar;

        private Page _currentPage;

        private int _lastSelectedTabIndex = -1;

        public MainPageRenderer()
        {
            // Required to say packager to not to add child pages automatically
            AutoPackage = false;
        }

        public void OnTabSelected(int position)
        {
            LoadPageContent(position);
        }

        public void OnTabReSelected(int position)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MainPage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                ClearElement(e.OldElement);
            }

            if (e.NewElement != null)
            {
                InitializeElement(e.NewElement);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearElement(Element);
            }

            base.Dispose(disposing);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            if (Element == null)
            {
                return;
            }

            var statusBarHeight = 0;

            //int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            //if (resourceId > 0)
            //{
            //    statusBarHeight = Resources.GetDimensionPixelSize(resourceId);
            //}

            b -= statusBarHeight;

            int width = r - l;
            int height = b - t;

            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.AtMost));

            // We need to call measure one more time with measured sizes 
            // in order to layout the bottom bar properly
            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(_bottomBar.ItemContainer.MeasuredHeight, MeasureSpecMode.Exactly));

            int barHeight = _bottomBar.ItemContainer.MeasuredHeight;

            _bottomBar.Layout(0, b - barHeight, width, b);

            float density = Android.Content.Res.Resources.System.DisplayMetrics.Density;

            double contentWidthConstraint = width / density;
            double contentHeightConstraint = (height - barHeight + statusBarHeight) / density;

            if (_currentPage != null)
            {
                var renderer = Platform.GetRenderer(_currentPage);

                renderer.Element.Measure(contentWidthConstraint, contentHeightConstraint);
                renderer.Element.Layout(new Rectangle(0, 0, contentWidthConstraint, contentHeightConstraint));

                renderer.UpdateLayout();
            }
        }

        private void InitializeElement(MainPage element)
        {
            element.CurrentPageChanged += (sender, args) =>
            {
                _bottomBar.SelectTabAtPosition(element.Children.IndexOf(element.CurrentPage), true);
            };

            PopulateChildren(element);
        }

        private void PopulateChildren(MainPage element)
        {
            // Unfortunately bottom bar can not be reused so we have to
            // remove it and create the new instance
            _bottomBar?.RemoveFromParent();

            _bottomBar = CreateBottomBar(element.Children);
            AddView(_bottomBar);

            LoadPageContent(0);
        }

        private void ClearElement(MainPage element)
        {
            if (_currentPage != null)
            {
                IVisualElementRenderer renderer = Platform.GetRenderer(_currentPage);

                if (renderer != null)
                {
                    renderer.ViewGroup.RemoveFromParent();
                    renderer.ViewGroup.Dispose();
                    renderer.Dispose();

                    _currentPage = null;
                }

                if (_bottomBar != null)
                {
                    _bottomBar.RemoveFromParent();
                    _bottomBar.Dispose();
                    _bottomBar = null;
                }
            }
        }

        private BottomBar CreateBottomBar(IEnumerable<Page> pageIntents)
        {
            var bar = new BottomBar(Context);

            // TODO: Configure the bottom bar here according to your needs
            bar.SetOnTabClickListener(this);
            //bar.UseFixedMode();
            //bar.UseDarkTheme();
            PopulateBottomBarItems(bar, pageIntents);
            //bar.ItemContainer.SetBackgroundColor(Color.White.ToAndroid());
            bar.SetActiveTabColor(Color.FromHex("#b71c1c").ToAndroid());
            bar.SetTextAppearance(Resource.Style.BottomBarTitle);

            return bar;
        }

        private void PopulateBottomBarItems(BottomBar bar, IEnumerable<Page> pages)
        {
            var barItems = pages.Select(x => new BottomBarTab(new IconDrawable(Context, x.Icon).SizeDp(25), x.Title));

            bar.SetItems(barItems.ToArray());
        }

        private void LoadPageContent(int position)
        {
            ShowPage(position);
        }

        private void ShowPage(int position)
        {
            if (position != _lastSelectedTabIndex)
            {
                Element.CurrentPage = Element.Children[position];

                if (Element.CurrentPage != null)
                {
                    LoadPageContent(Element.CurrentPage);
                }
            }

            _lastSelectedTabIndex = position;
        }

        private void LoadPageContent(Page page)
        {
            UnloadCurrentPage();

            _currentPage = page;

            LoadCurrentPage();

            Element.CurrentPage = _currentPage;
        }

        private void LoadCurrentPage()
        {
            var renderer = Platform.GetRenderer(_currentPage);

            if (renderer == null)
            {
                renderer = Platform.CreateRenderer(_currentPage);
                Platform.SetRenderer(_currentPage, renderer);

                AddView(renderer.ViewGroup);
            }
            else
            {
                // As we show and hide pages manually OnAppearing and OnDisappearing
                // workflow methods won't be called by the framework. Calling them manually...
                var basePage = _currentPage as TabContentPage;
                basePage?.SendAppearing();
            }

            renderer.ViewGroup.Visibility = ViewStates.Visible;
        }

        private void UnloadCurrentPage()
        {
            if (_currentPage != null)
            {
                var basePage = _currentPage as TabContentPage;
                basePage?.SendDisappearing();

                var renderer = Platform.GetRenderer(_currentPage);

                if (renderer != null)
                {
                    renderer.ViewGroup.Visibility = ViewStates.Invisible;
                }
            }
        }
    }
}