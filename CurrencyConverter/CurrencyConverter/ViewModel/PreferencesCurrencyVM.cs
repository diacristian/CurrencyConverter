using ExchangeRateAPI;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CurrencyConverter.ViewModel.Contracts;

namespace CurrencyConverter.ViewModel
{
    public class PreferencesCurrencyVm : ObservableVm, IVm
    {
        private bool _isAnimationAllowed;

        public PreferencesCurrencyVm(ICommand backCommand)
        {
            PopulateAvailableCurrencies();
            PopulatePreferedCurrencies();
            IsAnimationAllowed = Properties.Settings.Default.AnimateApp;
            CurrencyConverterCommand = backCommand;
        }

        public ICommand CurrencyConverterCommand { get; set; }

        public List<Currency> AllCurrencies { get; set; }
        public List<PreferedCurrencyVm> PreferedCurrencies { get; set; }

        public bool IsAnimationAllowed
        {
            get => _isAnimationAllowed;
            set
            {
                _isAnimationAllowed = value;
                OnPropertyChanged();
            }
        }

        private void PopulateAvailableCurrencies()
        {
            if (AllCurrencies == null)
            {
                AllCurrencies = new List<Currency>();
            }
            Parallel.ForEach(new Currencies().AvailableApiCurrencies, _ =>
            {
                AllCurrencies.Add(_);
            });
        }

        private void PopulatePreferedCurrencies()
        {
            if (PreferedCurrencies == null)
            {
                PreferedCurrencies = new List<PreferedCurrencyVm>();
            }

            if (Properties.Settings.Default.PreferedCurrencyCodes != null)
            {
                Parallel.ForEach(AllCurrencies, _ =>
                {
                    var isPrefered = Properties.Settings.Default.PreferedCurrencyCodes.Contains(_.Code);
                    PreferedCurrencies.Add(new PreferedCurrencyVm(_, isPrefered));
                });
            }
            else
            {
                Parallel.ForEach(AllCurrencies, _ =>
                {
                    PreferedCurrencies.Add(new PreferedCurrencyVm(_, true));
                });
            }
        }

    }
}
