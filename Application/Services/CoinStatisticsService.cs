using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoinTree.Application.Dtos;
using CoinTree.Application.Enums;
using CoinTree.Application.Exceptions;
using CoinTree.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoinTree.Application.Services
{
    public class CoinStatisticsService : ICoinStatisticsService
    {
       private readonly HttpClient _httpClient;

        private readonly ILogger<CoinStatisticsService> _logger;

        public CoinStatisticsService(HttpClient httpClient, ILogger<CoinStatisticsService> logger)
        {
            _httpClient = httpClient;

            _logger = logger; 
        }

        public async Task<CoinStatsDto> GetCoinStaticsticsAsync(CoinType coinType, CancellationToken cancelationToken)
        {
            var resultStr = string.Empty;

            switch (coinType)
            {
                case CoinType.BTC:
                case CoinType.ETH:
                case CoinType.XRP:
                    resultStr = await _httpClient.GetStringAsync(CreateRequestUri(coinType), cancelationToken);
                    break;
                default:
                    throw new CoinNotExistsException();
            }

            return JsonConvert.DeserializeObject<CoinStatsDto>(resultStr);
        }

        private string CreateRequestUri(CoinType coinType)
        {
            return $"/api/prices/aud/{coinType}";
        }
    }
}
