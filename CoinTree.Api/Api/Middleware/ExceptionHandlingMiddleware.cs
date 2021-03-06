using System;
using System.Net;
using System.Threading.Tasks;
using CoinTree.Api.ViewModels;
using CoinTree.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoinTree.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string response = null;

            if (exception is OperationCanceledException)
            {
                _logger.LogDebug(exception, "Request was cancelled.");
                return;
            }
            if (exception is CoinNotExistsException)
            { 
                _logger.LogError(exception, "Coin does not exist");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = JsonConvert.SerializeObject(ErrorModel.Create(exception.Message));
            }
            else
            {
                _logger.LogError(exception, "Something really bad occurred. We shouldn't ever return 500.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = JsonConvert.SerializeObject(ErrorModel.Create("Internal server error"));
            }

            await context.Response.WriteAsync(response);
        }
    }
}
