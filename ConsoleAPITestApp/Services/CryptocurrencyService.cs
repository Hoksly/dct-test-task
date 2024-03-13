using ConsoleAPITestApp.Models;

namespace ConsoleAPITestApp.Services
{
    public class CryptocurrencyService
    {
        private readonly ICurrencyDataFactory _factory;

        public CryptocurrencyService(ICurrencyDataFactory factory)
        {
            _factory = factory;
        }

        public async Task<Currency> GetCurrencyDetails(string id, string apiSource)
        {
            // Select specific API based on apiSource
            object rawData = await FetchDataFromApi(id, apiSource);  
            return _factory.CreateCurrency(rawData);
        }
        
        public async Task<Currency> FetchDataFromApi(string id, string apiSource)
        {
            IFetchStrategy strategy = GetFetchStrategy(apiSource); // Helper method to select strategy

            object rawData = await strategy.FetchRawData(id);
            return _factory.CreateCurrency(rawData);
        }

        private IFetchStrategy GetFetchStrategy(string apiSource) 
        {
            // Example using a simple switch statement
            switch(apiSource.ToLowerInvariant())
            {
                case "coincap": return new CoinCapFetchStrategy();
                case "coingecko": return new CoinGeckoFetchStrategy();
                default: throw new ArgumentException("Unsupported API");
            }
        }

    }

}
