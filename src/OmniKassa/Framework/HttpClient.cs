// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;

namespace OmniKassa
{
    internal sealed class HttpClient : WebClient, IHttpClient
    {
        private readonly IPaymentPostData _postData;

        public HttpClient()
        {
        }

        public HttpClient(IPaymentPostData postData)
        {
            Debug.Assert(postData != null);

            _postData = postData;
        }

        public IPaymentPostData GetPostData()
        {
            return _postData;
        }

        public byte[] PostData(Uri uri, IPaymentPostData postData)
        {
            NameValueCollection data = ConvertPostData(postData);

            return UploadValues(uri, data);
        }

        internal static NameValueCollection ConvertPostData(IPaymentPostData postData)
        {
            Debug.Assert(postData != null);

            return new NameValueCollection
            {
                { "Data", postData.Data },
                { "Seal", postData.Seal },
                { "InterfaceVersion", postData.InterfaceVersion }
            };
        }
    }
}