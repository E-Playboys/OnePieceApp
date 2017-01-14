using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OnePiece.App.Utilities
{
    public interface IObservableCollectionEx : INotifyCollectionChanged, INotifyPropertyChanged, IList
    {
        void Add(int index, object item);
        void Move(int oldIndex, int newIndex);
    }

    public class ObservableCollectionEx<T> : ObservableCollection<T>, IObservableCollectionEx
    {
        public void Add(int index, object item)
        {
            if (Items.IsReadOnly)
                throw new NotSupportedException("NotSupported_ReadOnlyCollection");

            InsertItem(index, (T)item);
        }
    }
}
