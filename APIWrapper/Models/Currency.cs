namespace ConsoleAPITestApp.Models
{
        public class Currency
        {
            public string Id { get; set; }
            public int Rank { get; set; }
            public string Symbol { get; set; }
            public string Name { get; set; }
            public decimal? Supply { get; set; } // Supply might be null if not provided by the API
            public decimal? MaxSupply { get; set; } // MaxSupply m  ight be null if not provided by the API
            public decimal MarketCapUsd { get; set; }
            public decimal PriceUsd { get; set; }
            public decimal VolumeUsd24Hr { get; set; }
            public decimal? Quantity24Hr { get; set; } // Quantity24Hr might be null if not provided by the API
            public decimal ChangePercent24Hr { get; set; }
            public decimal Vwap24Hr { get; set; }
        }

    
}
