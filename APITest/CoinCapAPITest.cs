using ConsoleAPITestApp.Models;
using ConsoleAPITestApp.Services.API;

namespace APITest
{
    public class CoinCapAPITest
    {
        [SetUp]
        public void Setup()
        {
            CoinCapAPI api = new CoinCapAPI(new HttpClient());
        }

        [Test]
        public void TestPing()
        {
            CoinCapAPI api = new CoinCapAPI(new HttpClient());
            bool result = api.Ping().Result;
            Assert.IsTrue(result);
        }
        
        [Test] 
        public void TestGetTopCurrenciesAsync()
        {
            CoinCapAPI api = new CoinCapAPI(new HttpClient());
            IEnumerable<Currency> result = api.GetTopCurrenciesAsync(10).Result;
            Assert.IsNotNull(result);
            // check if the result has 10 items
            Assert.That(result.Count(), Is.EqualTo(10));
        }
        
        [Test]
        public void TestGetCurrencyAsync()
        {
            CoinCapAPI api = new CoinCapAPI(new HttpClient());
            Currency result = api.GetCurrencyAsync("bitcoin").Result;
            
            Assert.IsNotNull(result);
            // check if the result has the correct id
            Assert.That(result.Id, Is.EqualTo("bitcoin"));
        }
        
    }
}
