﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_gastosPessoais.Models;
using wpf_gastosPessoais.Misc;
using System.Windows;
using System.Collections.Specialized;
using Database;

namespace wpf_gastosPessoais.ViewModels
{
    public class GoalsViewModel : ViewModelBase
    {
        public GoalsViewModel()
        {
            goals = new TrulyObservableCollection<Goal>(DatabaseManager.ReadAll<Goal>());
            goals.CollectionChanged += Goals_CollectionChanged;
            goalControls = new TrulyObservableCollection<GoalControlViewModel>();
            foreach (var item in goals)
            {
                AddGoalControlVM(item);
            }
        }

        private ICommand addGoal;
        private TrulyObservableCollection<Goal> goals;
        private TrulyObservableCollection<GoalControlViewModel> goalControls;
        public TrulyObservableCollection<Goal> AllGoals
        {
            get => goals;
            set
            {
                goals = value;
                OnPropertyChanged("AllGoals");
            }
        }
        public TrulyObservableCollection<GoalControlViewModel> AllGoalsControls
        {
            get => goalControls;
            set
            {
                goalControls = value;
                OnPropertyChanged("AllGoalsControls");
            }
        }
        public Visibility GoalGridVisibility
        {
            get
            {
                return goals.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility FirstGoalVisibility
        {
            get
            {
                return goals.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public ICommand AddGoal
        {
            get
            {
                if (addGoal == null)
                    addGoal = new RelayCommand(AddGoalCommand);
                return addGoal;
            }
            set
            {
                addGoal = value;
                OnPropertyChanged("AddGoal");
            }
        }

        private void Goals_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                bool removed;
                foreach (var item in e.OldItems)
                {
                    Goal goal = (Goal)item;
                    removed = !goals.Contains(goal);
                    if (removed)
                    {
                        goalControls.Remove(goalControls.FirstOrDefault(x => x.Goal == goal));
                        DatabaseManager.Delete<Goal>($"Id = {goal.Id}");
                    }
                }
            }
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    AddGoalControlVM((Goal)item);
                    DatabaseManager.Save((Goal)item);
                }
            }
            OnPropertyChanged("FirstGoalVisibility", "GoalGridVisibility", "AllGoals");
        }

        private void AddGoalControlVM(Goal item)
        {
            goalControls.Add(new GoalControlViewModel(goals, item));
        }

        private void AddGoalCommand(object parameter)
        {
            WindowHostView window = new WindowHostView
            {
                DataContext = new EditGoalViewModel(this)
            };
            window.ShowDialog();
        }

    }
}