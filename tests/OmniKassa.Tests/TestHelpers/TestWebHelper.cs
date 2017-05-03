// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using System.Text;

namespace OmniKassa.Tests
{
    public sealed class TestWebHelper : IWebHelper
    {
        private IPaymentPostData _postData;
        private byte[] _response;

        public TestWebHelper(IPaymentPostData postData)
        {
            _postData = postData;
        }

        public TestWebHelper(string response)
        {
            if (response != null)
                _response = Encoding.UTF8.GetBytes(response);
        }

        public IPaymentPostData PostedData { get; private set; }

        public Uri PostedUrl { get; private set; }

        public void Dispose()
        {
        }

        public IPaymentPostData GetPostData()
        {
            return _postData;
        }

        public byte[] PostData(Uri url, IPaymentPostData postData)
        {
            PostedUrl = url;
            PostedData = postData;

            return _response;
        }
    }
}
