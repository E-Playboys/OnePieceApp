using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public partial class BindableGrid : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(BindableGrid), null,
                                        propertyChanged: (bindable, oldvalue, newvalue) => ((BindableGrid)bindable).OnSizeChanged());

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindableGrid), null,
                                        propertyChanged: (bindable, oldvalue, newvalue) => ((BindableGrid)bindable).OnItemsSourceChanged(oldvalue as IEnumerable, newvalue as IEnumerable));

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

        public BindableGrid()
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

        private void Coll_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null)
                return;

            BindItems(e.NewItems);
        }

        private void BindItems(IEnumerable items)
        {
            Grid.Children.Clear();
            Grid.RowDefinitions.Clear();
            int i = 0;
            foreach (object item in items)
            {
                var child = ItemTemplate.CreateContent() as View;
                if (child == null)
                    return;

                Grid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = GridLength.Star
                });

                Grid.RowSpacing = 5;

                child.BindingContext = item;
                Grid.Children.Add(child, 0, i);
                i++;
            }
        }
    }
}
