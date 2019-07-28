using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_gastosPessoais.Views
{
    /// <summary>
    /// Interação lógica para EditGoalView.xam
    /// </summary>
    public partial class EditGoalView : UserControl
    {
        public EditGoalView()
        {
            InitializeComponent();
        }

        public string GoalName
        {
            get => (string)GetValue(GoalNameProperty);
            set => SetValue(GoalNameProperty, value);
        }
        public string GoalValue
        {
            get => (string)GetValue(GoalValueProperty);
            set => SetValue(GoalValueProperty, value);
        }
        public string GoalSavedValue
        {
            get => (string)GetValue(GoalSavedValueProperty);
            set => SetValue(GoalSavedValueProperty, value);
        }
        public ICommand Cancel
        {
            get => (ICommand)GetValue(CancelProperty);
            set => SetValue(CancelProperty, value);
        }
        public ICommand Confirm
        {
            get => (ICommand)GetValue(ConfirmProperty);
            set => SetValue(ConfirmProperty, value);
        }
        public Visibility AddVisibility
        {
            get => (Visibility)GetValue(AddVisibilityProperty);
            set => SetValue(AddVisibilityProperty, value);
        }

        public static DependencyProperty GoalNameProperty = DependencyProperty.Register(
            "GoalName", typeof(string), typeof(EditGoalView)
            );
        public static DependencyProperty GoalValueProperty = DependencyProperty.Register(
            "GoalValue", typeof(string), typeof(EditGoalView)
            );
        public static DependencyProperty GoalSavedValueProperty = DependencyProperty.Register(
            "GoalSavedValue", typeof(string), typeof(EditGoalView)
            );
        public static DependencyProperty CancelProperty = DependencyProperty.Register(
            "Cancel", typeof(ICommand), typeof(EditGoalView)
            );
        public static DependencyProperty ConfirmProperty = DependencyProperty.Register(
            "Confirm", typeof(ICommand), typeof(EditGoalView)
            );
        public static DependencyProperty AddVisibilityProperty = DependencyProperty.Register(
            "AddVisibility", typeof(Visibility), typeof(EditGoalView)
            );

    }
}
