using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;

namespace wpf_gastosPessoais.Behaviors
{
    public enum NumericInputMode { None, Decimal, Digit }
    public class NumericTextBoxBehavior : Behavior<TextBox>
    {
        const NumberStyles validNumberStyles = NumberStyles.AllowDecimalPoint |
                                               NumberStyles.AllowLeadingSign;

        public NumericTextBoxBehavior()
        {
            InputMode = NumericInputMode.None;
            OnlyPositiveNumbers = false;
        }

        public NumericInputMode InputMode { get; set; }
        public bool             OnlyPositiveNumbers
        {
            get => (bool)GetValue(OnlyPositiveNumbersProperty);
            set => SetValue(OnlyPositiveNumbersProperty, value);
        }

        public static DependencyProperty OnlyPositiveNumbersProperty = DependencyProperty.Register(
            "OnlyPositiveNumbers", typeof(bool), typeof(NumericTextBoxBehavior), new PropertyMetadata(false)
            );

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown += AssociatedObjectPreviewKeyDown;
            DataObject.AddPastingHandler(AssociatedObject, Pasting);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown -= AssociatedObjectPreviewKeyDown;
            DataObject.RemovePastingHandler(AssociatedObject, Pasting);
        }

        private void AssociatedObjectPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsValidInput(GetText(e.Text)))
            {
                e.Handled = true;
            }
        }

        private void AssociatedObjectPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                if (!IsValidInput(GetText(" ")))
                {
                    e.Handled = true;
                }
            }
        }

        private void Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if(!IsValidInput(GetText(text)))
                {
                    e.CancelCommand();
                }
                return;
            }
            e.CancelCommand();
        }

        private string GetText(string input)
        {
            TextBox textBox = AssociatedObject;

            int selectionStart = textBox.SelectionStart;
            if (textBox.Text.Length < selectionStart)
                selectionStart = textBox.Text.Length;
            int selectionLenght = textBox.SelectionLength;
            if (textBox.Text.Length < selectionStart + selectionLenght)
                selectionLenght = textBox.Text.Length - selectionStart;
            string realText = textBox.Text.Remove(selectionStart, selectionLenght);

            int caretIndex = textBox.CaretIndex;
            if(realText.Length < caretIndex)
            {
                caretIndex = realText.Length;
            }

            string text = realText.Insert(caretIndex, input);
            return text;
        }

        private bool IsValidInput(string input)
        {
            switch (InputMode)
            {
                case NumericInputMode.None:
                    return true;
                case NumericInputMode.Decimal:
                    return CheckIsDecimal(input);
                case NumericInputMode.Digit:
                    return CheckIsDigit(input);
                default:
                    throw new ArgumentException("Unknow NumericInputMode");
            }
        }

        private bool CheckIsDigit(string input)
        {
            return input.All(char.IsDigit);
        }

        private bool CheckIsDecimal(string input)
        {
            char decimalSeparator = Convert.ToChar(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
            char thousandSeparator = Convert.ToChar(NumberFormatInfo.CurrentInfo.NumberGroupSeparator);
            if(input.Contains(thousandSeparator))
            {
                return false;
            }
            if(input.ToCharArray().Where(c => c == decimalSeparator).Count() > 1)
            {
                return false;
            }

            if(input.Contains("-"))
            {
                if (OnlyPositiveNumbers)
                    return false;
                if (input.IndexOf("-", StringComparison.Ordinal) > 0)
                    return false;
                if (input.Length == 1)
                    return true;
            }
            return decimal.TryParse(input, validNumberStyles, CultureInfo.CurrentCulture, out decimal d);
        }
    }
}
