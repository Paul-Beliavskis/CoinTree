using System;
using System.Net;

namespace CoinTree.Api.ViewModels
{
    public class ErrorModel
    {
        public string Message { get; set; }

        public static ErrorModel Create(string message)
        {
            return new ErrorModel
            {
                Message = message
            };
        }
    }
}
