using CurrencyConverter.Commands;
using CurrencyConverter.Utils;
using ExchangeRateAPI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CurrencyConverter.ViewModel.Contracts;

namespace CurrencyConverter.ViewModel
{
    public class CurrencyConverterVm : ObservableVm, IVm, IClosing
    {
        private IVm _currentVm;
        private bool _isConvertFromBlocked;
        private bool _isConvertToBlocked;
        private readonly ConvertServiceProvider _convertServiceProvider;
        private double _convertFromValue;
        private double _convertToValue;
        private Currency _convertFromSelectedItem;
        private Currency _convertToSelectedItem;
        private double _convertFromSelectedItemCurrency;
        private double _convertToSelectedItemCurrency;

        public CurrencyConverterVm()
        {
            _convertServiceProvider = new ConvertServiceProvider();
            PreferencesCurrencyVm = new PreferencesCurrencyVm(new RelayCommand(e => new CurrencyConverterCommand(this).Execute(), ce => new CurrencyConverterCommand(this).CanExecute()));
            PopulateAvailableCurrencies();

            SwitchCurrenciesCommand = new RelayCommand(e => new SwitchCurrencyCommand(this).Execute(), ce => new SwitchCurrencyCommand(this).CanExecute());
            PreferencesCommand = new RelayCommand(e => new PreferencesCommand(this).Execute(), ce => new PreferencesCommand(this).CanExecute());

            CurrentVm = this;
        }

        public IVm CurrentVm
        {
            get => _currentVm;
            set
            {
                _currentVm = value;
                OnPropertyChanged();
            }
        }

        public PreferencesCurrencyVm PreferencesCurrencyVm { get; set; }

        public ObservableCollection<Currency> AvailableCurrencies { get; set; }

        public double ConvertFromValue
        {
            get => _convertFromValue;
            set
            {
                if (_convertFromValue == value) return;

                _convertFromValue = value;

                if (!_isConvertToBlocked)
                {
                    _isConvertFromBlocked = true;
                    var newValue = Task.Run(() => _convertServiceProvider.ConvertFrom2ToAsync(ConvertFromSelectedItem, _convertToSelectedItem, _convertFromValue)).Result;
                    if (ConvertToValue != newValue)
                    {
                        ConvertToValue = newValue;
                    }
                }
                _isConvertToBlocked = false;


                OnPropertyChanged();
            }
        }

        public double ConvertToValue
        {
            get => _convertToValue;
            set
            {
                if (_convertToValue == value) return;

                _convertToValue = value;

                if (!_isConvertFromBlocked)
                {
                    var newValue = Task.Run(() => _convertServiceProvider.ConvertTo2FromAsync(ConvertFromSelectedItem, _convertToSelectedItem, _convertToValue)).Result;
                    if (ConvertFromValue != newValue)
                    {
                        _isConvertToBlocked = true;
                        ConvertFromValue = newValue;
                    }


                }
                _isConvertFromBlocked = false;
                OnPropertyChanged();
            }
        }

        public Currency ConvertFromSelectedItem
        {
            get
            {
                if (_convertFromSelectedItem == null)
                {
                    _convertFromSelectedItem = AvailableCurrencies.FirstOrDefault();

                    if (_convertServiceProvider.CheckForInternetConnection())
                    {
                        RefreshSelectedItemsCurrencies();
                    }
                }
                return _convertFromSelectedItem;
            }
            set
            {
                if (_convertFromSelectedItem == value) return;

                _convertFromSelectedItem = value;
                _isConvertFromBlocked = true;
                RefreshSelectedItemsCurrencies();
                ConvertToValue = Task.Run(() => _convertServiceProvider.ConvertFrom2ToAsync(ConvertFromSelectedItem, _convertToSelectedItem, _convertFromValue)).Result;
                OnPropertyChanged();
            }
        }

        public Currency ConvertToSelectedItem
        {
            get
            {
                if (_convertToSelectedItem == null)
                {
                    _convertToSelectedItem = AvailableCurrencies.LastOrDefault();

                    if (_convertServiceProvider.CheckForInternetConnection())
                    {
                        RefreshSelectedItemsCurrencies();
                    }
                }
                return _convertToSelectedItem;
            }
            set
            {
                if (_convertToSelectedItem == value) return;

                _convertToSelectedItem = value;
                _isConvertFromBlocked = true;
                RefreshSelectedItemsCurrencies();
                ConvertToValue = Task.Run(() => _convertServiceProvider.ConvertFrom2ToAsync(ConvertFromSelectedItem, _convertToSelectedItem, _convertFromValue)).Result;
                OnPropertyChanged();
            }
        }

        public double ConvertFromSelectedItemCurrency
        {
            get
            {
                return _convertFromSelectedItemCurrency;
            }
            set
            {
                _convertFromSelectedItemCurrency = value;
                OnPropertyChanged();
            }
        }
        public double ConvertToSelectedItemCurrency
        {
            get
            {
                return _convertToSelectedItemCurrency;
            }
            set
            {
                _convertToSelectedItemCurrency = value;
                OnPropertyChanged();
            }
        }

        public ICommand SwitchCurrenciesCommand { get; set; }
        public ICommand PreferencesCommand { get; set; }

        private void RefreshSelectedItemsCurrencies()
        {
            ConvertFromSelectedItemCurrency = Task.Run(() => _convertServiceProvider.RetrieveConvertFromCurrencyRateAsync(ConvertFromSelectedItem, _convertToSelectedItem)).Result;
            ConvertToSelectedItemCurrency = Task.Run(() => _convertServiceProvider.RetrieveConvertToCurrencyRateAsync(ConvertFromSelectedItem, _convertToSelectedItem)).Result;
        }

        public void PopulateAvailableCurrencies()
        {
            if (AvailableCurrencies == null)
            {
                AvailableCurrencies = new ObservableCollection<Currency>();
            }

            AvailableCurrencies.Clear();

            foreach (var preferedCurrency in PreferencesCurrencyVm.PreferedCurrencies)
            {
                if (preferedCurrency == null)
                {
                    continue;
                }

                if (preferedCurrency.PreferedByUser)
                {
                    AvailableCurrencies.Add(preferedCurrency.Currency);
                }
            }
        }

        public void CheckSelectedElement()
        {
            if (!AvailableCurrencies.Contains(ConvertFromSelectedItem))
            {
                ConvertFromSelectedItem = AvailableCurrencies.FirstOrDefault();
            }

            if (!AvailableCurrencies.Contains(ConvertToSelectedItem))
            {
                ConvertToSelectedItem = AvailableCurrencies.LastOrDefault();
            }
        }

        public bool OnClosing()
        {
            SaveChanges();
            return true;
        }

        private void SaveChanges()
        {
            var defaultSettings = Properties.Settings.Default;
            defaultSettings.AnimateApp = PreferencesCurrencyVm.IsAnimationAllowed;
            defaultSettings.PreferedCurrencyCodes = new System.Collections.Specialized.StringCollection();

            foreach (var availableCurrency in PreferencesCurrencyVm.PreferedCurrencies)
            {
                if (availableCurrency.PreferedByUser)
                {
                    defaultSettings.PreferedCurrencyCodes.Add(availableCurrency.Currency.Code);
                }
            }

            Properties.Settings.Default.Save();
        }

    }
}
