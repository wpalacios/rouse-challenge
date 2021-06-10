namespace Rouse.MarketAuctionChallenge.Core.Entities
{
    public class Schedule
    {
        public decimal DefaultMarketRatio { get; set; }
        public decimal DefaultAuctionRatio { get; set; }
        public System.Collections.Generic.Dictionary<string, Year> Years { get; set; } = new System.Collections.Generic.Dictionary<string, Year>();
    }
}
