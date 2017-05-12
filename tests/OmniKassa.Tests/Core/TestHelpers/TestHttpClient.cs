// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NETCOREAPP1_1

using System;
using System.Net;
using System.Net.Http;

namespace OmniKassa.Tests
{
    public sealed class TestHttpClient : HttpClient
    {
        private readonly TestHttpMessageHandler _handler;

        public TestHttpClient(string response)
            : this(new TestHttpMessageHandler(response))
        {
        }

        private TestHttpClient(TestHttpMessageHandler handler)
            : base(handler)
        {
            _handler = handler;
        }

        public IPaymentPostData PostedData => CreatePostedData(_handler.Request);

        public Uri PostedUrl => _handler.Request.RequestUri;

        private IPaymentPostData CreatePostedData(HttpRequestMessage request)
        {
            string postedData = request.GetPostedData();
            if (postedData == null)
                return null;

            PaymentPostData result = new PaymentPostData();

            foreach (string keyValue in postedData.Split('&'))
            {
                if (keyValue.StartsWith("Data="))
                    result.Data = WebUtility.UrlDecode(keyValue.Substring(5));
                if (keyValue.StartsWith("InterfaceVersion="))
                    result.InterfaceVersion = WebUtility.UrlDecode(keyValue.Substring(17));
                if (keyValue.StartsWith("Seal="))
                    result.Seal = WebUtility.UrlDecode(keyValue.Substring(5));
            }

            return result;
        }
    }
}

#endif