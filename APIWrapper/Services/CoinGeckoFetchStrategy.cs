namespace ConsoleAPITestApp.Services
{
    public class CoinGeckoFetchStrategy : IFetchStrategy
    {
        public Task<object> FetchRawData(string id)
        {
            // Logic to fetch data from CoinGecko API
            return Task.FromResult(new object());
        }
    }
}
