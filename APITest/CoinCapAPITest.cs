using ConsoleAPITestApp.Models;
using ConsoleAPITestApp.Services.API;

namespace APITest
{
    public class CoinCapAPITest
    {
        private CoinCapAPI api;
        [SetUp]
        public void Setup()
        {
            api = new CoinCapAPI(new HttpClient());
        }

        [Test]
        public void TestPing()
        {
            bool result = api.Ping().Result;
            Assert.IsTrue(result);
        }
        
        [Test] 
        public void TestGetTopCurrenciesAsync()
        {
            IEnumerable<Currency> result = api.GetTopCurrenciesAsync(10).Result;
            Assert.IsNotNull(result);
            // check if the result has 10 items
            Assert.That(result.Count(), Is.EqualTo(10));
        }
        
        [Test]
        public void TestGetCurrencyAsync()
        {
            Currency result = api.GetCurrencyAsync("bitcoin").Result;
            Assert.IsNotNull(result);
            // check if the result has the correct id
            Assert.That(result.Id, Is.EqualTo("bitcoin"));
        }
        
        [Test]
        public void TestGetAssetMarketsAsync()
        {
            IEnumerable<Market> result = api.GetMarketsAsync("bitcoin", 10).Result;
            Assert.IsNotNull(result);
            // check if the result has 10 items or less
            Assert.That(result.Count(), Is.LessThanOrEqualTo(10));
            
            // check if the result has the correct base id
            Assert.That(result.All(market => market.BaseId == "bitcoin"));
        }

        
    }
}
