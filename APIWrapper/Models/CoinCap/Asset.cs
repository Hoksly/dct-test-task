namespace ConsoleAPITestApp.Models
{
    public class Asset
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Supply { get; set; }
        public decimal? MaxSupply { get; set; }
        public decimal MarketCapUsd { get; set; }
        public decimal VolumeUsd24Hr { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal ChangePercent24Hr { get; set; }
        public decimal Vwap24Hr { get; set; }
        
        public Currency ToCurrency()
        {
            return new Currency
            {
                Id = Id,
                Name = Name,
                Symbol = Symbol,
                MarketCapUsd = MarketCapUsd,
                PriceUsd = PriceUsd,
                VolumeUsd24Hr = VolumeUsd24Hr,
                ChangePercent24Hr = ChangePercent24Hr,
                Vwap24Hr = Vwap24Hr,
                Supply = Supply,
                MaxSupply = MaxSupply
            };
        }
    }
    


}
