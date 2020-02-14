using System.Windows;
using System.Windows.Controls;

namespace CurrencyConverter.Controls.Utils
{
    public class TextBoxExtensions
    {
        public static string GetNonIntrusiveText(DependencyObject obj)
        {
            return (string)obj.GetValue(NonIntrusiveTextProperty);
        }

        public static void SetNonIntrusiveText(DependencyObject obj, string value)
        {
            obj.SetValue(NonIntrusiveTextProperty, value);
        }

        /// <summary>
        /// Using this AttachedProperty to avoid jumping effect of caret index to left followed by IsAsync property set to true
        /// More info: http://blogs.spencen.com/?p=566
        /// </summary>
        /// <returns></returns>
        public static readonly DependencyProperty NonIntrusiveTextProperty =  DependencyProperty.RegisterAttached("NonIntrusiveText", typeof(string), typeof(TextBoxExtensions), 
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, NonIntrusiveTextChanged));

        public static void NonIntrusiveTextChanged(
            object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            var caretIndex = textBox.CaretIndex;
            var selectionStart = textBox.SelectionStart;
            var selectionLength = textBox.SelectionLength;
            textBox.Text = (string)e.NewValue;
            textBox.CaretIndex = caretIndex;
            textBox.SelectionStart = selectionStart;
            textBox.SelectionLength = selectionLength;
        }
    }
}