using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace wpf_gastosPessoais.Misc
{
    public class TrulyObservableCollection<T> : ObservableCollection<T>, ICollectionItemPropertyChanged<T>
        where T : INotifyPropertyChanged
    {
        public TrulyObservableCollection()
        {
            CollectionChanged += SetPropertyChanged;
        }

        public TrulyObservableCollection(ICollection<T> collection) : this()
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public event EventHandler<ItemChangedEventArgs<T>> ItemChanged;

        private void SetPropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged += Item_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged -= Item_PropertyChanged;
                }
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var args = new ItemChangedEventArgs<T>((T)sender, e.PropertyName);
            ItemChanged?.Invoke(this, args);
        }
    }
}
