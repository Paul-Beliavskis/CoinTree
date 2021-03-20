using System;
using System.Net;
using System.Net.Mime;
using CoinTree.Application.Interfaces;
using CoinTree.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinTree.Application.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICoinStatisticsService, CoinStatisticsService>(client =>
            {
                client.DefaultRequestHeaders.Add(HttpRequestHeader.Accept.ToString(), MediaTypeNames.Application.Json);
                client.BaseAddress = new Uri(configuration["Apis:CoinExchange:Url"]);
            })
            .AddPolicyHandler(RetryPolicy.GetRetryPolicy());
        }
    }
}
