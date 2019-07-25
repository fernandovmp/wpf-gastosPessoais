using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace wpf_gastosPessoais.Views
{
    /// <summary>
    /// Interação lógica para EntryControl.xam
    /// </summary>
    public partial class EntryControl : UserControl
    {
        public EntryControl()
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
        public string TypeOfEntry
        {
            get => (string)GetValue(TypeOfEntryProperty);
            set => SetValue(TypeOfEntryProperty, value);
        }
        public Brush  ValueForeground
        {
            get => (Brush)GetValue(ValueForegroundProperty);
            set => SetValue(ValueForegroundProperty, value);
        }
        public ICommand DeleteEntry
        {
            get => (ICommand)GetValue(DeleteEntryProperty);
            set => SetValue(DeleteEntryProperty, value);
        }
        public ICommand EditEntry
        {
            get => (ICommand)GetValue(EditEntryProperty);
            set => SetValue(EditEntryProperty, value);
        }

        public static DependencyProperty EntryNameProperty = DependencyProperty.Register(
            "EntryName", typeof(string), typeof(EntryControl)
            );
        public static DependencyProperty EntryValueProperty = DependencyProperty.Register(
            "EntryValue", typeof(string), typeof(EntryControl)
            );
        public static DependencyProperty EntryGroupProperty = DependencyProperty.Register(
            "EntryGroup", typeof(string), typeof(EntryControl)
            );
        public static DependencyProperty TypeOfEntryProperty = DependencyProperty.Register(
            "TypeOfEntry", typeof(string), typeof(EntryControl)
            );
        public static DependencyProperty ValueForegroundProperty = DependencyProperty.Register(
            "ValueForeground", typeof(Brush), typeof(EntryControl)
            );
        public static DependencyProperty DeleteEntryProperty = DependencyProperty.Register(
            "DeleteEntry", typeof(ICommand), typeof(EntryControl)
            );
        public static DependencyProperty EditEntryProperty = DependencyProperty.Register(
            "EditEntry", typeof(ICommand), typeof(EntryControl)
            );
    }
}
