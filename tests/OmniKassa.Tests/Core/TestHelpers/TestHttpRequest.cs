// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NETCOREAPP1_1

using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace OmniKassa.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestHttpRequest : HttpRequest
    {
        public TestHttpRequest()
        {
        }

        public override Stream Body { get; set; }

        public override long? ContentLength { get; set; }

        public override string ContentType { get; set; }

        public override IRequestCookieCollection Cookies { get; set; }

        public override IFormCollection Form { get; set; }

        public override bool HasFormContentType => false;

        public override IHeaderDictionary Headers { get; }

        public override HostString Host { get; set; }

        public override HttpContext HttpContext { get; }

        public override bool IsHttps { get; set; }

        public override string Method { get; set; }

        public override PathString Path { get; set; }

        public override PathString PathBase { get; set; }

        public override string Protocol { get; set; }

        public override IQueryCollection Query { get; set; }

        public override QueryString QueryString { get; set; }

        public override string Scheme { get; set; }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken)
        {
            IFormCollection collection = new FormCollection(new Dictionary<string, StringValues>
            {
                { "Test", new StringValues("Test") }
            });

            return Task.FromResult(collection);
        }
    }
}

#endif