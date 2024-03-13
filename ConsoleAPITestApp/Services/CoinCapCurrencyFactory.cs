using ConsoleAPITestApp.Models;

namespace ConsoleAPITestApp.Services
{
    public class CoinCapCurrencyFactory : ICurrencyDataFactory
    {
        public Currency CreateCurrency(object rawData)
        {
            // Logic to parse CoinCap JSON format and create a Currency object
            return new Currency();  
        }
    }
}
