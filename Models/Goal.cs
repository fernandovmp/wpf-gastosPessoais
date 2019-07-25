using Database;

namespace wpf_gastosPessoais.Models
{
    public class Goal
    {
        [DBOption(AutoIncrement = true, PrimaryKey = true)]
        public int      Id { get; set; }
        [DBOption()]
        public string   Name { get; set; }
        [DBOption()]
        public decimal  Value { get; set; }
        [DBOption()]
        public decimal  SavedValue { get; set; }
        public float    Progress { get => (float)(SavedValue / Value); }
        [DBOption()]
        public bool     Completd { get; set; }
    }
}
