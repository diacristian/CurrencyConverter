using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ExchangeRateAPI
{
    public class Currencies
    {
        public Currencies()
        {
            PopulateAvailableApiCurrencies();
        }

        public List<Currency> AvailableApiCurrencies { get; private set; }

        private void PopulateAvailableApiCurrencies()
        {
            AvailableApiCurrencies = new List<Currency>
            {
                new Currency("AED", "UAE Dirham", "United Arab Emirates"),
                new Currency("ARS", "Argentine Peso", "Argentina"),
                new Currency("AUD", "Australian Dollar", "Australia"),
                new Currency("CHF", "Swiss Franc", "Switzerland"),
                new Currency("EUR", "Euro", "Spain"),
                new Currency("GBP", "Pound Sterling", "United Kingdom"),
                new Currency("RON", "Romanian Leu", "Romania"),
                new Currency("JPY", "Japanese Yen", "United States"),
                new Currency("USD", "US Dolar", "Japan"),
                new Currency("KRW", "South Korean Won", "Korea"),
                new Currency("MXN", "Mexican Peso", "Mexico"),
                new Currency("NOK", "Norwegian Krone", "Norway"),
                new Currency("TRY", "Turkish Lira", "Turkey"),
                new Currency("NZD", "New Zealand Dollar", "New Zealand"),
                new Currency("ZAR", "South African Rand", "South Africa"),
                new Currency("UAH", "Ukrainian Hryvnia", "Ukraine"),
                new Currency("SAR", "Saudi Riyal", "Saudi Arabia"),

            };
        }

        public Currency GetCurrencyAfterCode(string currencyCode)
        {
            if (IsCurrencyAvailable(currencyCode))
            {
                foreach (var availableCurrency in AvailableApiCurrencies.Where(availableCurrency => availableCurrency.Code == currencyCode).Select(availableCurrency => availableCurrency).AsParallel())
                {
                    return availableCurrency;
                }
            }

            return null;
        }

        public bool IsCurrencyAvailable(string currencyCode)
        {
            var currencyAvailable = false;

            Parallel.ForEach(AvailableApiCurrencies, _ =>
               {
                   if (_.Code.Equals(currencyCode))
                   {
                       currencyAvailable = true;
                   }
               });

            return currencyAvailable;
        }
    }
}
