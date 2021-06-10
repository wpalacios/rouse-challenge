    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using Rouse.MarketAuctionChallenge.Core.Entities;
    using Rouse.MarketAuctionChallenge.Core.Interfaces.Services;

    namespace Rouse.MarketAuctionChallenge.Core.Services
    {
        public class MarketAuctionService : IMarketAuctionService
        {
            Dictionary<string, MarketAuction> allMarketAuctions;

            public MarketAuctionService()
            {
            }
            public MarketAuctionService(Dictionary<string, MarketAuction> allMarketAuctions)
            {
                this.allMarketAuctions = allMarketAuctions;
            }

            public Dictionary<string, MarketAuction> GetAllMarketAuctions()
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                string mockDataJson = File.ReadAllText("Mock/apiResponse.json");
                return JsonSerializer.Deserialize<Dictionary<string, MarketAuction>>(mockDataJson, options);
            }

            public KeyValuePair<string, MarketAuction> GetMarketAuctionItem(int id)
            {
                var marketAuctions = allMarketAuctions ?? this.GetAllMarketAuctions();
                return marketAuctions.FirstOrDefault(a => a.Key == id.ToString());
            }

            public KeyValuePair<string, Year> GetMarketAuctionYear(KeyValuePair<string, MarketAuction> marketAuction, int year)
            {
                return marketAuction.Value.Schedule.Years.FirstOrDefault(y => y.Key == year.ToString());
            }

            public MarketAuctionValue GetMarketActionValue(int id, int year)
            {
                /*
                    If we want to calculate the 'value' of a set and year,
                    we multiply the ratio for that year with the cost in saleDetails for that set.
                    MarketValue = {cost} * {marketRatio}
                    AuctionValue = {cost} * {auctionRatio}
                */
                var marketAuction = GetMarketAuctionItem(id);

                if (marketAuction.Equals(default(KeyValuePair<string, MarketAuction>)))
                {
                    return null;
                }

                var marketAuctionYear = GetMarketAuctionYear(marketAuction, year);

                if (marketAuctionYear.Equals(default(KeyValuePair<string, Year>)))
                {
                    return null;
                }

                var (cost, _, _) = marketAuction.Value.SaleDetails;
                var (marketRatio, auctionRatio) = marketAuctionYear.Value;

                return new MarketAuctionValue()
                {
                    MarketValue = cost * marketRatio,
                    AuctionValue = cost * auctionRatio,
                };
            }
        }
    }
