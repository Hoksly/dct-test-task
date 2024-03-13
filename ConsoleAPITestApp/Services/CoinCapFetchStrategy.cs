namespace ConsoleAPITestApp.Services
{
    public class CoinCapFetchStrategy : IFetchStrategy
    {
        public Task<object> FetchRawData(string id)
        {
            // Logic to fetch data from CoinCap API
            return Task.FromResult(new object());
        }
    }
}
