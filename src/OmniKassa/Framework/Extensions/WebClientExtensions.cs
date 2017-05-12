// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NET35

using System;
using System.Collections.Specialized;
using System.Net;

namespace OmniKassa
{
    internal static class WebClientExtensions
    {
        public static byte[] PostData(this WebClient self, Uri uri, IPaymentPostData postData)
        {
            NameValueCollection data = ConvertPostData(postData);

            return self.UploadValues(uri, data);
        }

        internal static NameValueCollection ConvertPostData(IPaymentPostData postData)
        {
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