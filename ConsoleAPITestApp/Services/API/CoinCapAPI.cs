using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        private string BuildRequestUri(string endpoint, AssetRequestParameters parameters = null)
        {
            var builder = new UriBuilder(_baseUrl + endpoint);
            var query = HttpUtility.ParseQueryString(builder.Query);

            if (parameters != null)
            {
                if (parameters.Limit.HasValue)
                {
                    query["limit"] = parameters.Limit.Value.ToString();
                }
                
            }

            builder.Query = query.ToString();
            return builder.Uri.ToString();
        }
    }
}
