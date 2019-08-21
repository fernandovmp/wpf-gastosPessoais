using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using wpf_gastosPessoais.Models;
using wpf_gastosPessoais.Data;
using System.Threading.Tasks;

namespace wpf_gastosPessoais.ViewModels
{
    public class EditEntryViewModel : EditBaseViewModel
    {

        public EditEntryViewModel(EntryControlViewModel entryControl)
        {
            this.entryControl = entryControl;
            Entry entry = entryControl.Entry;
            EntryName = entry.Name;
            EntryValue = entry.Value.ToString("F2");
            EntryGroup = entry.Group;
            isCredit = entryControl.Entry.EntryType == EntryType.Credit ? true : false;
            isEditMode = true;
            GetGroupsAsync();
        }

        private async void GetGroupsAsync()
        {
            groups = await GetEntryGroups();
            UpdateGroupSource();
        }

        public EditEntryViewModel(EntriesViewModel entriesViewModel)
        {
            this.entriesViewModel = entriesViewModel;
            isCredit = true;
            isEditMode = false;
            GetGroupsAsync();
        }

        private ICommand                            checkbox;
        private bool                                isCredit;
        private EntriesViewModel                    entriesViewModel;
        private EntryControlViewModel               entryControl;
        private ICollection<EntryGroup>             groups;
        private ObservableCollection<EntryGroup>    groupSource;
        public  string                              EntryName { get; set; }
        public  string                              EntryValue { get; set; }
        public  string                              EntryGroup { get; set; }
        public  bool                                IsCredit
        {
            get => isCredit;
            set
            {

            }
        }
        public  bool                                IsDebit
        {
            get => !isCredit;
            set
            {

            }
        }
        public  ICommand                            Checkbox
        {
            get
            {
                if (checkbox == null)
                    checkbox = new RelayCommand(CheckIsCredit);
                return checkbox;
            }
            set => checkbox = value;
        }
        public  ObservableCollection<EntryGroup>    GroupSource
        {
            get => groupSource;
            set
            {
                groupSource = value;
                OnPropertyChanged("GroupSource");
            }
        }

        private void CheckIsCredit(object parameter)
        {
            isCredit = !isCredit;
            UpdateGroupSource();
            OnPropertyChanged("IsCredit", "IsDebit");
        }

        private async Task<ICollection<EntryGroup>> GetEntryGroups()
        {
            return await new EntryGroupRepository().GetAll();
        }

        private void UpdateGroupSource()
        {
            var source = from serie in groups
                         where serie.Type == (IsCredit ? 1 : -1)
                         select serie;
            GroupSource = new ObservableCollection<EntryGroup>(source);
            UpdateSelectedGroup();
        }

        private void UpdateSelectedGroup()
        {
            if (!GroupSource.ToList().Exists(x => x.Name == EntryGroup))
            {
                EntryGroup = GroupSource[0].Name;
                OnPropertyChanged("EntryGroup");
            }
        }

        protected override void AddCommand(object parameter)
        {
            decimal.TryParse(EntryValue, out decimal value);
            EntryRepository repository = new EntryRepository();
            Entry entry = new Entry
            {
                Id = repository.NextId++,
                Name = EntryName,
                Group = EntryGroup,
                Value = value,
                EntryType = isCredit ? EntryType.Credit : EntryType.Debit
            };
            entriesViewModel.AllEntries.Add(entry);
            SaveEntryGroup();
            base.AddCommand(parameter);
        }

        protected override void EditCommand(object parameter)
        {
            decimal.TryParse(EntryValue, out decimal value);
            entryControl.Entry.Name = EntryName;
            entryControl.Entry.Group = EntryGroup;
            entryControl.Entry.Value = value;
            entryControl.SaveEdit();
            SaveEntryGroup();
            base.EditCommand(parameter);
        }

        private void SaveEntryGroup()
        {
            if (!GroupSource.ToList().Exists(x => x.Name == EntryGroup))
            {
                new EntryGroupRepository().Save(new EntryGroup
                {
                    Name = EntryGroup,
                    Type = IsCredit ? 1 : -1
                });
            }
        }

    }
}
