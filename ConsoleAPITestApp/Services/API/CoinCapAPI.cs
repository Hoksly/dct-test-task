using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using ConsoleAPITestApp.Exceptions.ConsoleAPITestApp.Services.API;
using Newtonsoft.Json.Linq;
using ConsoleAPITestApp.Models; // Assuming your models are in this namespace

namespace ConsoleAPITestApp.Services.API
{
    public class RootObject
    {
        public Asset[] Data { get; set; }
        public long Timestamp { get; set; }
    }

    public class CoinCapAPI : ICryptoAPI
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "https://api.coincap.io/v2/";

        public CoinCapAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Currency>> GetTopCurrenciesAsync(int count)
        {
            var assets = await GetAssetsAsync(new AssetRequestParameters { Limit = count });
            return assets.Select(asset => asset.ToCurrency());
        }

        public async Task<bool> Ping()
        {
            try
            {
                // Choose a simple API endpoint known to exist
                var requestUri = BuildRequestUri("assets"); 

                // Make request with a short timeout
                using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(5)); 
                var response = await _httpClient.GetAsync(requestUri, timeoutCts.Token);

                // Consider a simple status code check for success
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException) 
            {
                // Basic network-level failure
                return false;
            }
            catch (TaskCanceledException) 
            {
                // Timeout occurred
                return false;
            }
        }
        
        public async Task<Asset> GetAssetAsync(string id)
        {
            var requestUri = BuildRequestUri($"assets/{id}"); 
            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound) // Adjust if needed
                {
                    throw new NoSuchCurrencyException($"Currency with id '{id}' not found.");
                }
                
                else 
                {
                    throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
                }
            }

            var jsonResponseString = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(jsonResponseString)["data"].ToObject<Asset>(); // Assuming the 'data' property exists 
            return data;
        }


        public Task<Currency> GetCurrencyAsync(string id)
        {
            try
            {

                Asset asset = GetAssetAsync(id).Result;
                return Task.FromResult(asset.ToCurrency());
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"API request failed with status code: {e.Message}");
            }
        }



        private async Task<IEnumerable<Asset>> GetAssetsAsync(AssetRequestParameters parameters = null)
        {
            var requestUri = BuildRequestUri("assets", parameters);
            var response = await _httpClient.GetAsync(requestUri);


            return JObject.Parse(await response.Content.ReadAsStringAsync())["data"].ToObject<IEnumerable<Asset>>();
        }

        private string BuildRequestUri(string endpoint, RequestParameters parameters = null)
        {
            var builder = new UriBuilder(_baseUrl + endpoint);
            var query = HttpUtility.ParseQueryString(builder.Query);

            if (parameters is AssetRequestParameters)
            {
                var assetParameters = parameters as AssetRequestParameters;
                if (assetParameters.Limit.HasValue && assetParameters.Limit.Value > 0)
                {
                    query["limit"] = assetParameters.Limit.Value.ToString();
                }
            }
            else if (parameters is MarketRequestParameters)
            {
                var marketParameters = parameters as MarketRequestParameters;
                if (marketParameters.Limit.HasValue && marketParameters.Limit.Value > 0)
                {
                    query["limit"] = marketParameters.Limit.Value.ToString();
                }
                if (marketParameters.Offset.HasValue) 
                {
                    query["offset"] = marketParameters.Offset.Value.ToString();
                }
            }

            builder.Query = query.ToString();
            return builder.Uri.ToString();
        }
        
        private async Task<IEnumerable<Market>> GetAssetMarketsAsync(string id, MarketRequestParameters parameters = null)
        {
            var requestUri = BuildRequestUri($"assets/{id}/markets", parameters);
            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                // Handle API errors (e.g., throw an exception)
                throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
            }
        
            var jsonResponseString = await response.Content.ReadAsStringAsync();
            try
            {
                var data = JObject.Parse(jsonResponseString)["data"].ToObject<IEnumerable<Market>>();
                return data;
            }
            catch (Exception e)
            {
                // Handle JSON parsing errors TODO
                
                throw;
            }
            
        }
        
        public async Task<IEnumerable<Market>> GetMarketsAsync(string id, int limit = 10)
        {
            var markets = await GetAssetMarketsAsync(id, new MarketRequestParameters { Limit = limit });
            return markets; 
        }

    }
}
