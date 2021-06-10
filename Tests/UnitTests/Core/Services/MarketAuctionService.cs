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
                                    DefaultAuctionRatio = 0.02M,
                                    DefaultMarketRatio = 0.002M,
                                    Years = new Dictionary<string, Year>()
                                    {
                                        {
                                            "2006", new Year()
                                            {
                                                AuctionRatio = 0.181383M,
                                                MarketRatio = 0.311276M,
                                            }
                                        },
                                        {
                                            "2007", new Year()
                                            {
                                                AuctionRatio = 0.185085M,
                                                MarketRatio = 0.317628M,
                                            }
                                        },
                                        {
                                            "2008", new Year()
                                            {
                                                AuctionRatio = 0.188862M,
                                                MarketRatio = 0.324111M,
                                            }
                                        },
                                        {
                                            "2009", new Year()
                                            {
                                                AuctionRatio = 0.192716M,
                                                MarketRatio = 0.330725M,
                                            }
                                        },
                                        {
                                            "2010", new Year()
                                            {
                                                AuctionRatio = 0.198498M,
                                                MarketRatio = 0.363179M,
                                            }
                                        },
                                        {
                                            "2011", new Year()
                                            {
                                                AuctionRatio = 0.206337M,
                                                MarketRatio = 0.374074M,
                                            }
                                        },
                                        {
                                            "2012", new Year()
                                            {
                                                AuctionRatio = 0.213178M,
                                                MarketRatio = 0.431321M,
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
                                    DefaultAuctionRatio = 0.06M,
                                    DefaultMarketRatio = 0.06M,
                                    Years = new Dictionary<string, Year>()
                                    {
                                        {
                                            "2016", new Year()
                                            {
                                                AuctionRatio = 0.417468M,
                                                MarketRatio = 0.613292M,
                                            }
                                        },
                                        {
                                            "2017", new Year()
                                            {
                                                AuctionRatio = 0.473205M,
                                                MarketRatio = 0.692965M,
                                            }
                                        },
                                        {
                                            "2018", new Year()
                                            {
                                                AuctionRatio = 0.684991M,
                                                MarketRatio = 0.980485M,
                                            }
                                        },
                                        {
                                            "2019", new Year()
                                            {
                                                AuctionRatio = 0.727636M,
                                                MarketRatio = 1.041526M,
                                            }
                                        },
                                        {
                                            "2020", new Year()
                                            {
                                                AuctionRatio = 1.106366M,
                                                MarketRatio = 0.772935M,
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

                System.Console.WriteLine($"Market Auction was not found: {itemWasNotFound.ToString()}");
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

                System.Console.WriteLine($"Market Auction Year was not found: {itemWasNotFound.ToString()}");
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
                    Assert.True(itemWasNotFound);
                }
                else
                {
                    KeyValuePair<string, Year> marketAuctionYear = marketAuctionService.GetMarketAuctionYear(marketAuction, year);
                    MarketAuctionValue marketAuctionValue = marketAuctionService.GetMarketActionValue(id, year);

                    System.Console.WriteLine($"MarketValue: {marketAuctionValue.MarketValue}");
                    System.Console.WriteLine($"AuctionValue: {marketAuctionValue.AuctionValue}");
                    Assert.True(marketAuctionValue.AuctionValue ==
                        marketAuction.Value.SaleDetails.Cost * marketAuctionYear.Value.AuctionRatio);
                    Assert.True(marketAuctionValue.MarketValue ==
                        marketAuction.Value.SaleDetails.Cost * marketAuctionYear.Value.MarketRatio);
                } 
            }

            [Theory]
            [InlineData(2021, 87964)]
            [InlineData(2020, 87964)]
            public void ShouldGetNothingForInvalidIdAndYear(int year, int id)
            {
                MarketAuctionValue marketAuctionValue = marketAuctionService.GetMarketActionValue(id, year);
                bool isNull = marketAuctionValue == null;

                System.Console.WriteLine($"marketAuctionValue is null: {isNull}");
                Assert.True(isNull);
            }
        }
    }