// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NETCOREAPP1_1

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OmniKassa.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public TestHttpMessageHandler(string content)
        {
            _response = new HttpResponseMessage();

            if (content != null)
                _response.Content = new StringContent(content);
        }

        public HttpRequestMessage Request { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetPostedData();

            Request = request;

            return await Task.FromResult(_response);
        }
    }
}

#endif