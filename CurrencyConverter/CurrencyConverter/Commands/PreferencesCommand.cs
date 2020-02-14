using CurrencyConverter.ViewModel;

namespace CurrencyConverter.Commands
{
    class PreferencesCommand
    {
        private readonly CurrencyConverterVm _vm;

        public PreferencesCommand(CurrencyConverterVm vm)
        {
            _vm = vm;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            _vm.CurrentVm = _vm.PreferencesCurrencyVm;
        }
    }
}
