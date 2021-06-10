namespace Rouse.MarketAuctionChallenge.Core.Entities
{
    public class Year
    {
        public decimal MarketRatio { get; set; }
        public decimal AuctionRatio { get; set; }

        internal void Deconstruct (out decimal marketRatio, out decimal auctionRatio)
        {
            marketRatio = MarketRatio;
            auctionRatio = AuctionRatio;
        }
    }
}
