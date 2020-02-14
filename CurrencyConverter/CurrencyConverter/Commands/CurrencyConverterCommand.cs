using CurrencyConverter.ViewModel;

namespace CurrencyConverter.Commands
{
    class CurrencyConverterCommand
    {
        private readonly CurrencyConverterVm _vm;

        public CurrencyConverterCommand(CurrencyConverterVm vm)
        {
            _vm = vm;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            _vm.PopulateAvailableCurrencies();
            _vm.CheckSelectedElement();
            _vm.CurrentVm = _vm;
        }
    }
}
