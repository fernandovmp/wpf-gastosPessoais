using Database;

namespace wpf_gastosPessoais.Models
{
    public enum EntryType { Credit = 1, Debit = -1}
    public class Entry
    {
        [DBOption(AutoIncrement = true, PrimaryKey = true)]
        public int Id { get; set; }
        [DBOption(ColumnName = "Name")]
        public string       Name { get; set; }
        [DBOption(ColumnName = "Type", DataType = "Int32")]
        public EntryType    EntryType { get; set; }
        [DBOption(ColumnName = "Value")]
        public decimal      Value { get; set; }
        [DBOption(ColumnName = "EntryGroup")]
        public string       Group { get; set; }
        [DBOption(ColumnName = "Editable")]
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
