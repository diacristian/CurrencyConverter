using System;
using System.Collections.Generic;

namespace ExchangeRateAPI
{
    public class CurrencyRates
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public int Time_last_updated { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
}
