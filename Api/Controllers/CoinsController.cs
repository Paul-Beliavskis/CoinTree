using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoinTree.Application.Dtos;
using CoinTree.Application.Enums;
using CoinTree.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoinTree.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly ILogger<CoinsController> _logger;

        private readonly ICoinStatisticsService _coinStatisticsService;

        public CoinsController(ILogger<CoinsController> logger, ICoinStatisticsService coinStatisticsService)
        {
            _logger = logger;

            _coinStatisticsService = coinStatisticsService;
        }

        [HttpGet("{coinType}")]
        public async Task<IActionResult> Get(CoinType coinType, CancellationToken cancelationToken)
        {
            var result = await _coinStatisticsService.GetCoinStaticsticsAsync(coinType, cancelationToken);

            return Ok(result);
        }
    }
}