using CurrencyConverter.ViewModel;

namespace CurrencyConverter.Commands
{
    class SwitchCurrencyCommand
    {
        private readonly CurrencyConverterVm _vm;

        public SwitchCurrencyCommand(CurrencyConverterVm vm)
        {
            _vm = vm;
        }

        public bool CanExecute()
        {
            if (_vm.ConvertFromSelectedItem == null)
            {
                return false;
            }

            if (_vm.ConvertFromSelectedItem.Equals(_vm.ConvertToSelectedItem))
            {
                return false;
            }

            return true;
        }

        public void Execute()
        {
            var oldConvertFromSelectedItem = _vm.ConvertFromSelectedItem;
            _vm.ConvertFromSelectedItem = _vm.ConvertToSelectedItem;
            _vm.ConvertToSelectedItem = oldConvertFromSelectedItem;
        }
    }
}
