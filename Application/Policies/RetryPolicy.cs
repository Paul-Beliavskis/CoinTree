using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoinTree.Api.Constants;
using Polly;
using Polly.Extensions.Http;

namespace CoinTree.Application.Services
{
    public static class RetryPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
             var randomJitter = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync(ApiConstants.Http.PollyRetryCount,    // exponential back-off plus some jitter
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                    + TimeSpan.FromMilliseconds(randomJitter.Next(0, 100))
                );
        }
    }
}
