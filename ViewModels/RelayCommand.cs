using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpf_gastosPessoais.ViewModels
{
    public class RelayCommand : ICommand
    {

        public RelayCommand(Action<object> action) : this(action, defaultCanExecute => true)
        {

        }

        public RelayCommand(Action<object> action, Predicate<object> predicate)
        {
            execute = action;
            canExecute = predicate;
        }

        private Action<object> execute;
        private Predicate<object> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute != null && canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
