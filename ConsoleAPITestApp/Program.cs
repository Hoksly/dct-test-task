// See https://aka.ms/new-console-template for more information

using 
var api = new CoinCapAPI(new HttpClient());
var candleParams = new CandleRequestParameters 
{
    Exchange = "poloniex",
    Interval = "h8", 
    BaseId = "ethereum",
    QuoteId = "bitcoin", 
};

var candles = await api.GetCandlesAsync(candleParams);

foreach (var candle in candles)
{
    Console.WriteLine($"Period: {candle.Period} - Open: {candle.Open} - Close: {candle.Close}");
}