using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using wpf_gastosPessoais.Models;

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
            isEditMode = true;
        }

        public EditEntryViewModel(EntriesViewModel entriesViewModel)
        {
            this.entriesViewModel = entriesViewModel;
            EntryName = "";
            EntryValue = "";
            EntryGroup = "";
            isCredit = true;
            isEditMode = false;
        }

        private ICommand                    checkbox;
        private bool                        isCredit;
        private EntriesViewModel            entriesViewModel;
        private EntryControlViewModel       entryControl;
        public  string      EntryName { get; set; }
        public  string      EntryValue { get; set; }
        public  string      EntryGroup { get; set; }
        public  bool        IsCredit
        {
            get => isCredit;
            set
            {

            }
        }
        public  bool        IsDebit
        {
            get => !isCredit;
            set
            {

            }
        }
        public  ICommand    Checkbox
        {
            get
            {
                if (checkbox == null)
                    checkbox = new RelayCommand(CheckIsCredit);
                return checkbox;
            }
            set => checkbox = value;
        }

        private void CheckIsCredit(object parameter)
        {
            isCredit = !isCredit;
            OnPropertyChanged("IsCredit", "IsDebit");
        }

        protected override void AddCommand(object parameter)
        {
            decimal.TryParse(EntryValue, out decimal value);
            Entry entry = new Entry
            {
                Name = EntryName,
                Group = EntryGroup,
                Value = value,
                EntryType = isCredit ? EntryType.Credit : EntryType.Debit
            };
            entriesViewModel.AllEntries.Add(entry);
            base.AddCommand(parameter);
        }

        protected override void EditCommand(object parameter)
        {
            decimal.TryParse(EntryValue, out decimal value);
            entryControl.Entry.Name = EntryName;
            entryControl.Entry.Group = EntryGroup;
            entryControl.Entry.Value = value;
            entryControl.SaveEdit();
            entryControl.NotifyEdit();
            base.EditCommand(parameter);
        }
    }
}
