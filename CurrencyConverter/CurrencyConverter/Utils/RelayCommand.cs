using System;
using System.Windows.Input;

namespace CurrencyConverter.Utils
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _executeCommand;

        public RelayCommand(Action<object> executeCommand)
            : this(executeCommand, null)
        {
        }

        public RelayCommand(Action<object> executeCommand, Predicate<object> canExecute)
        {
            if (executeCommand == null)
            {
                throw new ArgumentNullException("executeCommand");
            }
            _executeCommand = executeCommand;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }


        public void Execute(object parameter)
        {
            _executeCommand(parameter);
        }
    }
}
