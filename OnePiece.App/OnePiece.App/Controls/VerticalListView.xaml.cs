using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public partial class VerticalListView : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(VerticalListView), null,
                                        propertyChanged: (bindable, oldvalue, newvalue) => ((VerticalListView)bindable).OnSizeChanged());

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(VerticalListView), null,
                                        propertyChanged: (bindable, oldvalue, newvalue) => ((VerticalListView)bindable).OnItemsSourceChanged(oldvalue as IEnumerable, newvalue as IEnumerable));

        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(VerticalListView), StackOrientation.Horizontal,
                                        propertyChanged: (bindable, oldvalue, newvalue) => ((VerticalListView)bindable).OnOrientationChanged(oldvalue, newvalue));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public StackOrientation Orientation
        {
            get { return (StackOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public VerticalListView()
        {
            InitializeComponent();
        }

        private void OnSizeChanged()
        {
            ForceLayout();
        }

        private void OnItemsSourceChanged(IEnumerable oldItems, IEnumerable newItems)
        {
            if (oldItems != null)
            {
                var coll = (INotifyCollectionChanged)oldItems;
                coll.CollectionChanged -= Coll_CollectionChanged;
            }

            if (newItems != null)
            {
                var coll = (INotifyCollectionChanged)newItems;
                coll.CollectionChanged += Coll_CollectionChanged;
                BindItems(newItems);
            }
        }

        private void OnOrientationChanged(object oldValue, object newValue)
        {
            StackItems.Orientation = (StackOrientation)newValue;
        }

        private void Coll_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null)
                return;

            BindItems(e.NewItems);
        }

        private void BindItems(IEnumerable items)
        {
            StackItems.Children.Clear();
            foreach (object item in items)
            {
                var child = ItemTemplate.CreateContent() as View;
                if (child == null)
                    return;

                child.BindingContext = item;
                StackItems.Children.Add(child);
            }
        }
    }
}
