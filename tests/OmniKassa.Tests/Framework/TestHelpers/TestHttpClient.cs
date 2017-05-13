// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NET45

using System;
using System.Text;

namespace OmniKassa.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class TestHttpClient : IHttpClient
    {
        private byte[] _response;

        public TestHttpClient(string response)
        {
            if (response != null)
                _response = Encoding.UTF8.GetBytes(response);
        }

        public IPaymentPostData PostedData { get; private set; }

        public Uri PostedUrl { get; private set; }

        public void Dispose()
        {
        }

        public byte[] PostData(Uri url, IPaymentPostData postData)
        {
            PostedUrl = url;
            PostedData = postData;

            return _response;
        }
    }
}

#endif