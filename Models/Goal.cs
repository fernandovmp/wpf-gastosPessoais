using Database;
using wpf_gastosPessoais.Misc;

namespace wpf_gastosPessoais.Models
{
    public class Goal : NotifyPropertyChanged
    {
        private string   name;
        private decimal  value;
        private decimal  savedValue;
        private bool     completed;
        public  int      Id { get; set; }
        public  string   Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public  decimal  Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged("Value", "Progress", "IntProgress");
            }
        }
        public  decimal  SavedValue
        {
            get => savedValue;
            set
            {
                savedValue = value;
                OnPropertyChanged("SavedValue", "Progress", "IntProgress");
            }
        }
        public  float    Progress
        {
            get
            {
                if (Completed) return 1;
                return (float)(SavedValue / Value);
            }
        }
        public  int      IntProgress { get => (int)(Progress * 100); }
        public  bool     Completed
        {
            get => completed;
            set
            {
                completed = value;
                OnPropertyChanged("Completed", "Progress", "IntProgress");
            }
        }
    }
}
