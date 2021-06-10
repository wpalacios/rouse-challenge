    using Rouse.MarketAuctionChallenge.Core.Interfaces.Services;
    using Xunit;
    using Moq;
    using System.Collections.Generic;
    using Rouse.MarketAuctionChallenge.Core.Entities;
    using Rouse.MarketAuctionChallenge.Core.Services;
    using System.Linq;

    namespace Tests.Core.Services
    {
        public class MarketAuctionServiceTests
        {
            private Mock<IMarketAuctionService> _serviceMock;
            private MarketAuctionService marketAuctionService;

            public MarketAuctionServiceTests()
            {
                _serviceMock = new Mock<IMarketAuctionService>();
                _serviceMock.Setup(s => s.GetAllMarketAuctions()).Returns(
                    new Dictionary<string, MarketAuction>()
                    {
                        {
                            "67352", new MarketAuction()
                            {
                                Schedule = new Schedule()
                                {
                                    DefaultAuctionRatio = 0.02m,
                                    DefaultMarketRatio = 0.002m,
                                    Years = new Dictionary<string, Year>()
                                    {
                                        {
                                            "2006", new Year()
                                            {
                                                AuctionRatio = 0.181383m,
                                                MarketRatio = 0.311276m,
                                            }
                                        },
                                        {
                                            "2007", new Year()
                                            {
                                                AuctionRatio = 0.185085m,
                                                MarketRatio = 0.317628m,
                                            }
                                        },
                                        {
                                            "2008", new Year()
                                            {
                                                AuctionRatio = 0.188862m,
                                                MarketRatio = 0.324111m,
                                            }
                                        },
                                        {
                                            "2009", new Year()
                                            {
                                                AuctionRatio = 0.192716m,
                                                MarketRatio = 0.330725m,
                                            }
                                        },
                                        {
                                            "2010", new Year()
                                            {
                                                AuctionRatio = 0.198498m,
                                                MarketRatio = 0.363179m,
                                            }
                                        },
                                        {
                                            "2011", new Year()
                                            {
                                                AuctionRatio = 0.206337m,
                                                MarketRatio = 0.374074m,
                                            }
                                        },
                                        {
                                            "2012", new Year()
                                            {
                                                AuctionRatio = 0.213178m,
                                                MarketRatio = 0.431321m,
                                            }
                                        }
                                    }
                                },
                                SaleDetails = new SaleDetail()
                                {
                                    AuctionSaleCount= 17,
                                    Cost = 681252,
                                    RetailSaleCount = 122
                                }
                            }
                        },
                        {
                            "87390", new MarketAuction()
                            {
                                Schedule = new Schedule()
                                {
                                    DefaultAuctionRatio = 0.06m,
                                    DefaultMarketRatio = 0.06m,
                                    Years = new Dictionary<string, Year>()
                                    {
                                        {
                                            "2016", new Year()
                                            {
                                                AuctionRatio = 0.417468m,
                                                MarketRatio = 0.613292m,
                                            }
                                        },
                                        {
                                            "2017", new Year()
                                            {
                                                AuctionRatio = 0.473205m,
                                                MarketRatio = 0.692965m,
                                            }
                                        },
                                        {
                                            "2018", new Year()
                                            {
                                                AuctionRatio = 0.684991m,
                                                MarketRatio = 0.980485m,
                                            }
                                        },
                                        {
                                            "2019", new Year()
                                            {
                                                AuctionRatio = 0.727636m,
                                                MarketRatio = 1.041526m,
                                            }
                                        },
                                        {
                                            "2020", new Year()
                                            {
                                                AuctionRatio = 1.106366m,
                                                MarketRatio = 0.772935m,
                                            }
                                        }
                                    }
                                },
                                SaleDetails = new SaleDetail()
                                {
                                    AuctionSaleCount= 127,
                                    Cost = 48929,
                                    RetailSaleCount = 12
                                }
                            }
                        }
                    }
                );
                marketAuctionService = new MarketAuctionService(allMarketAuctions: _serviceMock.Object.GetAllMarketAuctions());
            }

            [Fact]
            public void ShouldReturnAllMarketAuctionItems()
            {
                var items = _serviceMock.Object.GetAllMarketAuctions();
                System.Console.WriteLine($"Total Market Auction Records: {items.Count}");
                Assert.True(items.Count == 2);
            }

            [Theory]
            [InlineData(67352)]
            [InlineData(87390)]
            public void ShouldGetMarketAuctionItem(int id)
            {
                KeyValuePair<string, MarketAuction> marketAuction =
                    marketAuctionService.GetMarketAuctionItem(id);

                bool itemWasNotFound = marketAuction.Equals(default(KeyValuePair<string, MarketAuction>));
                
                System.Console.WriteLine($"Market Auction found for ID: {id}");
                Assert.False(itemWasNotFound);
            }

            [Theory]
            [InlineData(2006)]
            [InlineData(2007)]
            [InlineData(2008)]
            [InlineData(2009)]
            [InlineData(2010)]
            public void ShouldGetMarketAuctionYear(int year)
            {
                int validItemId = 67352;
                KeyValuePair<string, MarketAuction> marketAuction =
                    marketAuctionService.GetMarketAuctionItem(validItemId);

                KeyValuePair<string, Year> marketAuctionYear =
                    marketAuctionService.GetMarketAuctionYear(marketAuction, year);
                bool itemWasNotFound = marketAuction.Equals(default(KeyValuePair<string, Year>));

                System.Console.WriteLine($"Market Auction information found for year: {year}");
                Assert.False(itemWasNotFound);
            }

            [Theory]
            [InlineData(2007, 67352)]
            [InlineData(2011, 87964)]
            public void ShouldGetCorrectAuctionValue(int year, int id)
            {
                KeyValuePair<string, MarketAuction> marketAuction = marketAuctionService.GetMarketAuctionItem(id);
                bool itemWasNotFound = marketAuction.Equals(default(KeyValuePair<string, MarketAuction>));

                if(itemWasNotFound)
                {
                    System.Console.WriteLine($"No information found for year {year} and ID {id}");
                    Assert.True(itemWasNotFound);
                }
                else
                {
                    KeyValuePair<string, Year> marketAuctionYear = marketAuctionService.GetMarketAuctionYear(marketAuction, year);
                    MarketAuctionValue marketAuctionValue = marketAuctionService.GetMarketActionValue(id, year);

                    System.Console.WriteLine($"Information found for year {year} and ID {id}");
                    System.Console.WriteLine($"MarketValue: {marketAuctionValue.MarketValue}");
                    System.Console.WriteLine($"AuctionValue: {marketAuctionValue.AuctionValue}");
                    Assert.True(marketAuctionValue.AuctionValue ==
                        marketAuction.Value.SaleDetails.Cost * marketAuctionYear.Value.AuctionRatio);
                    Assert.True(marketAuctionValue.MarketValue ==
                        marketAuction.Value.SaleDetails.Cost * marketAuctionYear.Value.MarketRatio);
                } 
            }
        }
    }