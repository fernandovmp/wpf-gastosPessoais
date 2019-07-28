using Database;
using wpf_gastosPessoais.Misc;

namespace wpf_gastosPessoais.Models
{
    public class Goal : NotifyPropertyChanged
    {
        private string name;
        private decimal value;
        private decimal savedValue;
        private bool completed;
        [DBOption(AutoIncrement = true, PrimaryKey = true)]
        public int      Id { get; set; }
        [DBOption()]
        public string   Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        [DBOption()]
        public decimal  Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged("Value", "Progress", "IntProgress");
            }
        }
        [DBOption()]
        public decimal  SavedValue
        {
            get => savedValue;
            set
            {
                savedValue = value;
                OnPropertyChanged("SavedValue", "Progress", "IntProgress");
            }
        }
        public float    Progress { get => (float)(SavedValue / Value); }
        public int      IntProgress { get => (int)(Progress * 100); }
        [DBOption()]
        public bool     Completed
        {
            get => completed;
            set
            {
                completed = value;
                OnPropertyChanged("Completed");
            }
        }
    }
}
