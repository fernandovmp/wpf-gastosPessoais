namespace wpf_gastosPessoais.Misc
{
    public class ItemChangedEventArgs<T>
    {
        public ItemChangedEventArgs(T item, string propertyName)
        {
            ItemChanged = item;
            PropertyName = propertyName;
        }

        public T ItemChanged { get; }
        public string PropertyName { get; }
    }
}