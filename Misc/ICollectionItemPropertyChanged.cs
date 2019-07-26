using System;

namespace wpf_gastosPessoais.Misc
{
    internal interface ICollectionItemPropertyChanged<T>
    {
        event EventHandler<ItemChangedEventArgs<T>> ItemChanged;
    }
}