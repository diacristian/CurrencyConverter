using CurrencyConverter.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;
using CurrencyConverter.Controls.Utils;
using CurrencyConverter.ViewModel.Contracts;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ContentControlHolder_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // When window is not loaded we don't want the animation as we don't have all the measurements to do it
            if (UtilExtensions.GetAnimationAllowed(MainStackPanel) && IsLoaded)
            {
                var sb = (Storyboard)FindResource("DataContextChanged");
                sb.Begin();
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (DataContext is IClosing context)
            {
                e.Cancel = !context.OnClosing();
            }
        }
    }
}
