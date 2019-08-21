using Database;
using wpf_gastosPessoais.Misc;

namespace wpf_gastosPessoais.Models
{
    public enum EntryType { Credit = 1, Debit = -1}
    public class Entry : NotifyPropertyChanged
    {
        private string      name;
        private decimal     value;
        private string      group;
        public int Id { get; set; }
        public string       Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public EntryType    EntryType { get; set; }
        public decimal      Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }
        public string       Group
        {
            get => group;
            set
            {
                group = value;
                OnPropertyChanged("Group");
            }
        }
        public bool         Editable { get; set; }
        public decimal      SignedValue
        {
            get => Value * (int)EntryType;
        }

        public Entry()
        {
            Name = "";
            EntryType = EntryType.Credit;
            Value = 0;
            Group = "";
            Editable = true;
        }

        public Entry(string name, EntryType type, decimal value, string group, bool editable = true)
        {
            Name = name;
            EntryType = type;
            Value = value * (int)type;
            Group = group;
            Editable = editable;
        }
    }
    
}
