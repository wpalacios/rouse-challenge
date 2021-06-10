using System.Collections.Generic;
using Rouse.MarketAuctionChallenge.Core.Entities;

namespace Rouse.MarketAuctionChallenge.Core.Interfaces.Services
{
    public interface IMarketAuctionService
    {
        Dictionary<string, MarketAuction> GetAllMarketAuctions();
        KeyValuePair<string, MarketAuction> GetMarketAuctionItem(int id);
        KeyValuePair<string, Year> GetMarketAuctionYear(KeyValuePair<string, MarketAuction> marketAuction, int year);
        MarketAuctionValue GetMarketActionValue(int id, int year);
    }
}