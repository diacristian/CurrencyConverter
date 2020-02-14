using ExchangeRateAPI;

namespace CurrencyConverter.ViewModel
{
    public class PreferedCurrencyVm : ObservableVm
    {
        private bool _preferedByUser;

        public PreferedCurrencyVm(Currency currency, bool preferedByUser)
        {
            Currency = currency;
            PreferedByUser = preferedByUser;
        }

        public Currency Currency { get; set; }
        public bool PreferedByUser
        {
            get => _preferedByUser;
            set
            {
                _preferedByUser = value;
                OnPropertyChanged();
            }
        }
    }
}
