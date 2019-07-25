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
using System.Windows.Shapes;

namespace wpf_gastosPessoais.Views
{
    /// <summary>
    /// Lógica interna para EditEntryView.xaml
    /// </summary>
    public partial class EditEntryView : UserControl
    {
        public EditEntryView()
        {
            InitializeComponent();
        }

        public string EntryName
        {
            get => (string)GetValue(EntryNameProperty);
            set => SetValue(EntryNameProperty, value);
        }
        public string EntryValue
        {
            get => (string)GetValue(EntryValueProperty);
            set => SetValue(EntryValueProperty, value);
        }
        public string EntryGroup
        {
            get => (string)GetValue(EntryGroupProperty);
            set => SetValue(EntryGroupProperty, value);
        }
        public bool IsCredit
        {
            get => (bool)GetValue(IsCreditProperty);
            set => SetValue(IsCreditProperty, value);
        }
        public bool IsDebit
        {
            get => (bool)GetValue(IsDebitProperty);
            set => SetValue(IsDebitProperty, value);
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
        public ICommand Checkbox
        {
            get => (ICommand)GetValue(CheckboxProperty);
            set => SetValue(CheckboxProperty, value);
        }
        public Visibility CheckboxVisibility
        {
            get => (Visibility)GetValue(CheckboxVisibilityProperty);
            set => SetValue(CheckboxVisibilityProperty, value);
        }

        public static DependencyProperty EntryNameProperty = DependencyProperty.Register(
            "EntryName", typeof(string), typeof(EditEntryView)
            );
        public static DependencyProperty EntryValueProperty = DependencyProperty.Register(
            "EntryValue", typeof(string), typeof(EditEntryView)
            );
        public static DependencyProperty EntryGroupProperty = DependencyProperty.Register(
            "EntryGroup", typeof(string), typeof(EditEntryView)
            );
        public static DependencyProperty IsCreditProperty = DependencyProperty.Register(
            "IsCredit", typeof(bool), typeof(EditEntryView)
            );
        public static DependencyProperty IsDebitProperty = DependencyProperty.Register(
            "IsDebit", typeof(bool), typeof(EditEntryView)
            );
        public static DependencyProperty CancelProperty = DependencyProperty.Register(
            "Cancel", typeof(ICommand), typeof(EditEntryView)
            );
        public static DependencyProperty ConfirmProperty = DependencyProperty.Register(
            "Confirm", typeof(ICommand), typeof(EditEntryView)
            );
        public static DependencyProperty CheckboxProperty = DependencyProperty.Register(
            "Checkbox", typeof(ICommand), typeof(EditEntryView)
            );
        public static DependencyProperty CheckboxVisibilityProperty = DependencyProperty.Register(
            "CheckboxVisibility", typeof(Visibility), typeof(EditEntryView)
            );
    }
}
