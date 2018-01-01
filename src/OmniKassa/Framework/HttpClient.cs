// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NET35

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;

namespace OmniKassa
{
    internal sealed class HttpClient : WebClient, IHttpClient
    {
        public HttpClient()
        {
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

#endif