using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CurrencyConverter.Controls.UserControls
{
    /// <summary>
    /// Interaction logic for MainCurrencyConverter.xaml
    /// </summary>
    public partial class MainCurrencyConverter : UserControl
    {
        public MainCurrencyConverter()
        {
            InitializeComponent();
        }

        private void ConvertFromValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                e.Handled = !IsTextAllowed(textBox.Text);
            }
    
        }

        private bool IsTextAllowed(string text)
        {
            var regex = new Regex("[^0-9.-]+");
            return !regex.IsMatch(text);
        }
    }
}
