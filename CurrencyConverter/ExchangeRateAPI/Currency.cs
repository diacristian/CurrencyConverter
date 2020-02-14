namespace ExchangeRateAPI
{
    public class Currency
    {
        public Currency(string code, string name, string country)
        {
            Code = code;
            Name = name;
            Country = country;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
