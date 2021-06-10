using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rouse.MarketAuctionChallenge.Core.Entities;
using Rouse.MarketAuctionChallenge.Core.Interfaces.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Rouse.MarketAuctionChallenge.Web.Controllers
{
[ApiController]
[Route("[controller]")]
public class MarketAuctionController : ControllerBase
{
    private readonly ILogger<MarketAuctionController> _logger;
    private IMarketAuctionService _marketAuctionService;

    public MarketAuctionController(ILogger<MarketAuctionController> logger,
        IMarketAuctionService marketAuctionService
        )
    {
        _logger = logger;
        _marketAuctionService = marketAuctionService;
    }

    [HttpGet]
    public Dictionary<string, MarketAuction> GetAll()
    {
        return _marketAuctionService.GetAllMarketAuctions();
    }

    [HttpGet("marketAuctionValue")]
    public IActionResult GetMarketAuctionValue(int id, int year)
    {
        // We can create ViewModels,
        // but I'm just adding the data object to simplify the implementation
        MarketAuctionValue marketAuctionValue =
            _marketAuctionService.GetMarketActionValue(id, year);

        if (marketAuctionValue == null)
        {
            return NotFound("ID and/or year doesn't exist!");
        }

        return Ok(marketAuctionValue);
    }
}
}
