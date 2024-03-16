namespace ConsoleAPITestApp.Services.API
{
    public class CryptoAPIFactory
    {
        public ICryptoAPI CreateAPI(string providerName)
        {
            if (providerName == "CoinCap") 
            {
                return new CoinCapAPI(new HttpClient());
            } 
            // ... other providers
            else throw new ArgumentException("Unsupported API provider");
        }
    }
}
