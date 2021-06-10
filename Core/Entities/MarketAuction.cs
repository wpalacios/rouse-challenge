namespace Rouse.MarketAuctionChallenge.Core.Entities
{
    public class MarketAuction
    {
        public Schedule Schedule { get; set; }
        public SaleDetail SaleDetails { get; set; }
        public Classification Classification { get; set; }
    }
}
