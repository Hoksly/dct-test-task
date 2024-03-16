using ConsoleAPITestApp.Services.API;
using ConsoleAPITestApp.Models;
using System.Runtime.InteropServices.JavaScript;

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
        public void TestAssetBuildRequestUri()
        {
            var parameters = new AssetRequestParameters
            {
                Limit = 10,
                Offset = 0
            };
            var uri = api.BuildRequestUri("assets", parameters);
            Assert.That(uri, Is.EqualTo("https://api.coincap.io/v2/assets?limit=10&offset=0"));
        }
        [Test]
        public void TestMarketBuildRequestUri()
        {
            var parameters = new MarketRequestParameters
            {
                Limit = 10,
                Offset = 0
            };
            var uri = api.BuildRequestUri("assets/bitcoin/markets", parameters);
            Assert.That(uri, Is.EqualTo("https://api.coincap.io/v2/assets/bitcoin/markets?limit=10&offset=0"));
        }

        [Test]
        public void TestCandleBuildRequestUri()
        {
            var candleParams = new CandleRequestParameters
            {
                Exchange = "poloniex",
                Interval = "h8",
                BaseId = "ethereum",
                QuoteId = "bitcoin",
            };
            var uri = api.BuildRequestUri("candles", candleParams);
            Assert.That(uri, Is.EqualTo("https://api.coincap.io/v2/candles?exchange=poloniex&interval=h8&baseId=ethereum&quoteId=bitcoin"));
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
