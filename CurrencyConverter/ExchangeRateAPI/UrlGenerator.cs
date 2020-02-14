namespace ExchangeRateAPI
{
    public class UrlGenerator
    {
        private const string BaseApiUrl = "https://api.exchangerate-api.com/v4/latest/";

        public string GetUrlForCurrency(string currencyCode)
        {
            return BaseApiUrl + currencyCode;
        }
    }
}
