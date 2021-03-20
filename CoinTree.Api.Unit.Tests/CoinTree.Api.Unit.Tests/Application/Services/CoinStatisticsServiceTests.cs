using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoinTree.Api.Unit.Tests.TestFactories;
using CoinTree.Application.Enums;
using CoinTree.Application.Services;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CoinTree.Api.Unit.Tests
{
    public class CoinStatisticsServiceTests
    {
        private readonly ILogger<CoinStatisticsService> _logger;

        private HttpClient _httpClient;

        private CoinStatisticsService _systemUnderTest;

        public CoinStatisticsServiceTests()
        {
            _logger = A.Fake<ILogger<CoinStatisticsService>>();
        }

        [Fact]
        public async Task GetCoinStaticstics_IsStatsReturned_CoinStatsReturned()
        {
            var httpMessageHandler = FakeHttpMessageHandler.GetHttpMessageHandler("{\"sell\":\"AUD\",\"buy\":\"BTC\",\"ask\":76710.6110,\"bid\":74710.8252,\"rate\":0.00001304,\"spotRate\":75710.7181,\"market\":\"AUD\",\"timestamp\":\"2021-03-20T07:40:02.1859046+00:00\",\"rateType\":\"Ask\",\"rateSteps\":null}", HttpStatusCode.OK);

            _httpClient = new HttpClient(httpMessageHandler);
            _httpClient.BaseAddress = new Uri("https://trade.cointree.com/");

            _systemUnderTest = new CoinStatisticsService(_httpClient, _logger);

            var stats = await _systemUnderTest.GetCoinStaticsticsAsync(CoinType.BTC, CancellationToken.None);

            Assert.Equal(stats.Buy, CoinType.BTC.ToString());
        }

        [Fact]
        public async Task GetCoinStaticstics_IsStatsReturned_WrongCoinReturned()
        {
            var httpMessageHandler = FakeHttpMessageHandler.GetHttpMessageHandler("{\"sell\":\"AUD\",\"buy\":\"BTC\",\"ask\":76710.6110,\"bid\":74710.8252,\"rate\":0.00001304,\"spotRate\":75710.7181,\"market\":\"AUD\",\"timestamp\":\"2021-03-20T07:40:02.1859046+00:00\",\"rateType\":\"Ask\",\"rateSteps\":null}", HttpStatusCode.OK);

            _httpClient = new HttpClient(httpMessageHandler);
            _httpClient.BaseAddress = new Uri("https://trade.cointree.com/");

            _systemUnderTest = new CoinStatisticsService(_httpClient, _logger);

            var stats = await _systemUnderTest.GetCoinStaticsticsAsync(CoinType.BTC, CancellationToken.None);

            Assert.NotEqual(stats.Buy, CoinType.ETH.ToString());
        }
    }
}
