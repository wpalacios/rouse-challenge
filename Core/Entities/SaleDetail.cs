using System;

namespace Rouse.MarketAuctionChallenge.Core.Entities
{
    public class SaleDetail
    {
        public int Cost { get; set; }
        public int RetailSaleCount { get; set; }
        public int AuctionSaleCount { get; set; }

        internal void Deconstruct(out int cost, out int retailSaleCount, out int auctionSaleCount)
        {
            cost = this.Cost;
            retailSaleCount = this.RetailSaleCount;
            auctionSaleCount = this.AuctionSaleCount;
        }
    }
}
