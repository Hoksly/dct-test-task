namespace ConsoleAPITestApp.Models
{
    public class MarketRequestParameters : RequestParameters
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }

}
