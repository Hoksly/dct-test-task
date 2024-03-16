namespace ConsoleAPITestApp.Models
{
    public class CandleRequestParameters : RequestParameters
    {
        public string Exchange { get; set; }
        public string Interval { get; set; }
        public string BaseId { get; set; }
        public string QuoteId { get; set; }
        public long? Start { get; set; }
        public long? End { get; set; }
    }
}
