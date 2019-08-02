namespace wpf_gastosPessoais.ViewModels
{
    public class DepositViewModel : EditBaseViewModel
    {
        public DepositViewModel(GoalControlViewModel goalControl)
        {
            this.goalControl = goalControl;
            isEditMode = true;
        }

        private GoalControlViewModel    goalControl;
        public  string                  DepositValue { get; set; }

        protected override void EditCommand(object parameter)
        {
            decimal.TryParse(DepositValue, out decimal value);
            goalControl.Goal.SavedValue += value;
            goalControl.SaveEdit();
            base.EditCommand(parameter);
        }
    }
}
