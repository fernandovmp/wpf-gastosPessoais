using wpf_gastosPessoais.Data;
using wpf_gastosPessoais.Models;

namespace wpf_gastosPessoais.ViewModels
{
    public class EditGoalViewModel : EditBaseViewModel
    {
        public EditGoalViewModel(GoalControlViewModel goalControl)
        {
            this.goalControl = goalControl;
            Goal goal = goalControl.Goal;
            GoalName = goal.Name;
            GoalValue = goal.Value.ToString("F2");
            isEditMode = true;
        }

        public EditGoalViewModel(GoalsViewModel goalsViewModel)
        {
            this.goalsViewModel = goalsViewModel;
            GoalName = "";
            GoalValue = "";
            GoalSavedValue = "";
            isEditMode = false;
        }

        private GoalControlViewModel    goalControl;
        private GoalsViewModel          goalsViewModel;
        public  string                  GoalName { get; set; }
        public  string                  GoalValue { get; set; }
        public  string                  GoalSavedValue { get; set; }

        protected override void AddCommand(object parameter)
        {
            decimal.TryParse(GoalValue, out decimal value);
            decimal.TryParse(GoalSavedValue, out decimal savedValue);
            GoalRepository repository = new GoalRepository();
            Goal goal = new Goal
            {
                Id = repository.NextId++,
                Name = GoalName,
                Value = value,
                SavedValue = savedValue
            };
            goalsViewModel.AllGoals.Add(goal);
            base.AddCommand(parameter);
        }

        protected override void EditCommand(object parameter)
        {
            decimal.TryParse(GoalValue, out decimal value);
            goalControl.Goal.Name = GoalName;
            goalControl.Goal.Value = value;
            goalControl.SaveEdit();
            goalControl.SetProgressText();
            base.EditCommand(parameter);
        }
    }
}
