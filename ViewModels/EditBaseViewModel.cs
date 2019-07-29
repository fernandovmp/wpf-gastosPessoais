using System.Windows;
using System.Windows.Input;

namespace wpf_gastosPessoais.ViewModels
{
    public class EditBaseViewModel : ViewModelBase
    {
        protected   ICommand    confirm;
        protected   ICommand    cancel;
        protected   bool        isEditMode;
        public      ICommand    Confirm
        {
            get
            {
                if (confirm == null)
                    confirm = SelectCommand();
                return confirm;
            }
            set
            {
                confirm = value;
                OnPropertyChanged("Confirm");
            }
        }
        public      ICommand    Cancel
        {
            get
            {
                if (cancel == null)
                    cancel = new RelayCommand(CancelCommand);
                return cancel;
            }
            set
            {
                cancel = value;
                OnPropertyChanged("Cancel");
            }
        }
        public      Visibility  AddVisibility
        {
            get => !isEditMode ? Visibility.Visible : Visibility.Collapsed;
            
        }
        private     ICommand    SelectCommand()
        {
            if (isEditMode)
                return new RelayCommand(EditCommand);
            return new RelayCommand(AddCommand);
        }

        protected virtual void AddCommand(object parameter)
        {
            CancelCommand(parameter);
        }

        protected virtual void EditCommand(object parameter)
        {
            CancelCommand(parameter);
        }

        protected virtual void CancelCommand(object parameter)
        {
            ((Window)parameter).Close();
        }
    }
}
