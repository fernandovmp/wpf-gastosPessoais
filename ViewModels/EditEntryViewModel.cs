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
    public class EditEntryViewModel : ViewModelBase
    {

        public EditEntryViewModel(EntryControlViewModel entryControl)
        {
            this.entryControl = entryControl;
            Entry entry = entryControl.Entry;
            confirm = new RelayCommand(EditEntryCommand);
            EntryName = entry.Name;
            EntryValue = entry.Value.ToString("F2");
            EntryGroup = entry.Group;
            isEditMode = true;
        }

        public EditEntryViewModel(EntriesViewModel entriesViewModel)
        {
            this.entriesViewModel = entriesViewModel;
            confirm = new RelayCommand(AddEntryCommand);
            EntryName = "";
            EntryValue = "";
            EntryGroup = "";
            isCredit = true;
            isEditMode = false;
        }

        private ICommand                    confirm;
        private ICommand                    checkbox;
        private ICommand                    cancel;
        private bool                        isCredit;
        private bool                        isEditMode;
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
        public  ICommand    Confirm
        {
            get
            {
                if (confirm == null)
                    confirm = new RelayCommand(AddEntryCommand);
                return confirm;
            }
            set => confirm = value;
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
        public  ICommand    Cancel
        {
            get
            {
                if (cancel == null)
                    cancel = new RelayCommand(CloseWindow);
                return cancel;
            }
            set => cancel = value;
        }
        public  Visibility  CheckboxVisibility
        {
            get
            {
                return isEditMode ? Visibility.Collapsed : Visibility.Visible;
            }
            set
            {
                isEditMode = value == Visibility.Visible ? false : true;
            }
        }

        private void CheckIsCredit(object parameter)
        {
            isCredit = !isCredit;
            OnPropertyChanged("IsCredit", "IsDebit");
        }

        private void AddEntryCommand(object parameter)
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
            CloseWindow(parameter);
        }

        private void EditEntryCommand(object parameter)
        {
            decimal.TryParse(EntryValue, out decimal value);
            entryControl.Entry.Name = EntryName;
            entryControl.Entry.Group = EntryGroup;
            entryControl.Entry.Value = value;
            entryControl.SaveEdit();
            entryControl.NotifyEdit();
            CloseWindow(parameter);
        }

        private void CloseWindow(object parameter)
        {
            ((Window)parameter).Close();
        }
    }
}
