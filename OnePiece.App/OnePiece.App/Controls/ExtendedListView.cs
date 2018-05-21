using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace OnePiece.App.Controls
{
    public class ExtendedListView : ListView
    {
        private bool _isLoadingMore;
        private bool _isItemAppearing;

        #region Bindable Properties

        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create(nameof(ItemClickCommand),
            typeof(ICommand), typeof(ExtendedListView));

        public static BindableProperty LoadMoreCommandProperty = BindableProperty.Create(nameof(LoadMoreCommand),
            typeof(ICommand), typeof(ExtendedListView));

        public static BindableProperty AllowSelectItemProperty = BindableProperty.Create(nameof(AllowSelectItem),
            typeof(bool), typeof(ExtendedListView), false);

        public static readonly BindableProperty CurrentIndexProperty = BindableProperty.Create(nameof(CurrentIndex), 
            typeof(int), typeof(ExtendedListView), -1, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = bindable as ExtendedListView;
                if (view.ItemsSource == null || view._isItemAppearing || !view.IsVisible)
                {
                    view._isItemAppearing = false;
                    return;
                } 
                if (oldValue != newValue && (int)newValue >= 0)
                {
                    var items = view.ItemsSource as IList;
                    var item = items[(int)newValue];
                    view.ScrollTo(item, ScrollToPosition.MakeVisible, true);
                }
        });

        #endregion

        #region Properties

        public ICommand ItemClickCommand
        {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public bool AllowSelectItem
        {
            get { return (bool)GetValue(AllowSelectItemProperty); }
            set { SetValue(AllowSelectItemProperty, value); }
        }

        public int CurrentIndex
        {
            get => (int)GetValue(CurrentIndexProperty);
            set => SetValue(CurrentIndexProperty, value);
        }

        #endregion

        public ExtendedListView()
        {
            RegisterEvents();
        }

        public ExtendedListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            ItemTapped -= OnItemTapped;
            ItemAppearing -= OnItemAppearing;

            ItemTapped += OnItemTapped;
            ItemAppearing += OnItemAppearing;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && ItemClickCommand != null && ItemClickCommand.CanExecute(e.Item))
            {
                ItemClickCommand.Execute(e.Item);
            }

            if (!AllowSelectItem)
            {
                SelectedItem = null;
            }
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            _isItemAppearing = true;

            var items = ItemsSource as IList;
            CurrentIndex = items.IndexOf(e.Item);

            if (_isLoadingMore || items == null || items.Count == 0 || LoadMoreCommand == null || !LoadMoreCommand.CanExecute(e.Item))
                return;

            // Hit the bottom
            if (e.Item == items[items.Count - 1])
            {
                _isLoadingMore = true;

                LoadMoreCommand.Execute(null);

                _isLoadingMore = false;
            }
        }
    }
}
