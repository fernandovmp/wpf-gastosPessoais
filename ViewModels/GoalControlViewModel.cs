using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_gastosPessoais.Models;
using wpf_gastosPessoais.Misc;
using System.Windows.Input;
using System.Windows;
using Database;

namespace wpf_gastosPessoais.ViewModels
{
    public class GoalControlViewModel : ViewModelBase
    {

        public GoalControlViewModel(TrulyObservableCollection<Goal> goals, Goal goal)
        {
            this.goals = goals;
            Goal = goal;
            SetProgressText();
        }

        private TrulyObservableCollection<Goal> goals;
        private ICommand            editGoal;
        private ICommand            deleteGoal;
        private ICommand            deposit;
        private ICommand            done;
        private string              progressText;
        public  Goal                Goal { get; set; }
        public  string              ProgressText
        {
            get => progressText;
            set
            {
                progressText = value;
                OnPropertyChanged("ProgressText");
            }
        }
        public  ICommand            EditGoal
        {
            get
            {
                if (editGoal == null)
                    editGoal = new RelayCommand(EditCommand);
                return editGoal;
            }
            set
            {
                editGoal = value;
                OnPropertyChanged("EditGoal");
            }
        }
        public  ICommand            DeleteGoal
        {
            get
            {
                if (deleteGoal == null)
                    deleteGoal = new RelayCommand(DeleteCommand);
                return deleteGoal;
            }
            set
            {
                deleteGoal = value;
                OnPropertyChanged("DeleteGoal");
            }
        }
        public  ICommand            Deposit
        {
            get
            {
                if (deposit == null)
                    deposit = new RelayCommand(DepositCommand);
                return deposit;
            }
            set
            {
                deposit = value;
                OnPropertyChanged("Deposit");
            }
        }
        public  ICommand            Done
        {
            get
            {
                if (done == null)
                    done = new RelayCommand(DoneCommand);
                return done;
            }
            set
            {
                done = value;
                OnPropertyChanged("Done");
            }
        }
        public  Visibility          NotCompletedVisibility
        {
            get => !Goal.Completed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void EditCommand(object parameter)
        {
            WindowHostView window = new WindowHostView
            {
                DataContext = new EditGoalViewModel(this)
            };
            window.ShowDialog();
        }

        private void DeleteCommand(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Tem certeza que deseja apagar esse objetivo?",
                $"Excluir {Goal.Name}", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                goals.Remove(Goal);
            }
        }

        private void DepositCommand(object parameter)
        {
            WindowHostView window = new WindowHostView
            {
                DataContext = new DepositViewModel(this)
            };
            window.ShowDialog();
            DatabaseManager.Update(Goal, $"Id = {Goal.Id}");
        }

        private void DoneCommand(object parameter)
        {
            Goal.Completed = true;
            OnPropertyChanged("NotCompletedVisibility");
            DatabaseManager.Update(Goal, $"Id = {Goal.Id}");
        }

        public void SaveEdit()
        {
            DatabaseManager.Update(Goal, $"Id = {Goal.Id}");
        }

        public void SetProgressText()
        {
            if (Goal.Completed)
            {
                ProgressText = "Alcançado";
                return;
            }
            ProgressText = $"{Goal.SavedValue.ToString("F2")}/{Goal.Value.ToString("F2")}";
        }
    }
}
