using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTree.Api.Unit.Tests.TestFactories
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private HttpResponseMessage _response;

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        public static HttpMessageHandler GetHttpMessageHandler(string content, HttpStatusCode httpStatusCode)
        {
            var memStream = new MemoryStream();

            var streamWriter = new StreamWriter(memStream);
            streamWriter.Write(content);
            streamWriter.Flush();
            memStream.Position = 0;

            var httpContent = new StreamContent(memStream);

            var response = new HttpResponseMessage()
            {
                StatusCode = httpStatusCode,
                Content = httpContent
            };

            var messageHandler = new FakeHttpMessageHandler(response);

            return messageHandler;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<HttpResponseMessage>();

            tcs.SetResult(_response);

            return tcs.Task;
        }
    }
}
