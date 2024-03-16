using ConsoleAPITestApp.Models;

namespace ConsoleAPITestApp.Services
{
    public class CoinGeckoCurrencyFactory : ICurrencyDataFactory
    {
        public Currency CreateCurrency(object rawData)
        {
            return new Currency();
        }
    }
}
