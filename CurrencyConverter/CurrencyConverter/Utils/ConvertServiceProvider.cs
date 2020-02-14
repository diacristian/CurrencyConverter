using DataRetriever;
using ExchangeRateAPI;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CurrencyConverter.Utils
{
    public class ConvertServiceProvider
    {
        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<double> ConvertFrom2ToAsync(Currency convertFrom, Currency convertTo, double convertFromValue)
        {
            if (convertFromValue == 0)
            {
                return 0;
            }

            if (new Currencies().IsCurrencyAvailable(convertFrom.Code))
            {
                var url = new UrlGenerator().GetUrlForCurrency(convertFrom.Code);
                var currencyRates = await new DataRetrieverManager().DownloadSerializedJsonDataAsync<CurrencyRates>(url);

                if (currencyRates.Rates != null)
                {
                    var currencyRate = currencyRates.Rates[convertTo.Code];
                    return Round(convertFromValue * currencyRate);
                }
            }

            return 0;
        }

        public async Task<double> ConvertTo2FromAsync(Currency convertFrom, Currency convertTo, double convertToValue)
        {
            if (convertToValue == 0)
            {
                return 0;
            }

            if (new Currencies().IsCurrencyAvailable(convertTo.Code))
            {
                var url = new UrlGenerator().GetUrlForCurrency(convertTo.Code);
                var currencyRates = await new DataRetrieverManager().DownloadSerializedJsonDataAsync<CurrencyRates>(url);

                if (currencyRates.Rates != null)
                {
                    var currencyRate = currencyRates.Rates[convertFrom.Code];
                    return Round(convertToValue * currencyRate);
                }
            }

            return 0;
        }

        public async Task<double> RetrieveConvertToCurrencyRateAsync(Currency convertFrom, Currency convertTo)
        {
            if (convertTo == null)
            {
                return 0;
            }

            if (new Currencies().IsCurrencyAvailable(convertFrom.Code))
            {
                var url = new UrlGenerator().GetUrlForCurrency(convertFrom.Code);
                var currencyRates = await new DataRetrieverManager().DownloadSerializedJsonDataAsync<CurrencyRates>(url);

                if (currencyRates.Rates != null)
                {
                    return Round(currencyRates.Rates[convertTo.Code]);
                }
            }

            return 0;
        }

        public async Task<double> RetrieveConvertFromCurrencyRateAsync(Currency convertFrom, Currency convertTo)
        {
            if (convertTo == null)
            {
                return 0;
            }

            if (new Currencies().IsCurrencyAvailable(convertTo.Code))
            {
                var url = new UrlGenerator().GetUrlForCurrency(convertTo.Code);
                var currencyRates = await new DataRetrieverManager().DownloadSerializedJsonDataAsync<CurrencyRates>(url);

                if (currencyRates.Rates != null)
                {
                    return Round(currencyRates.Rates[convertFrom.Code]);
                }
            }

            return 0;
        }

        private double Round(double value)
        {
            return Math.Round(value, 4);
        }
    }
}
