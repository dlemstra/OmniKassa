// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NETSTANDARD1_3

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OmniKassa
{
    internal static class HttpClientExtensions
    {
        public static async Task<byte[]> PostData(this HttpClient self, Uri uri, IPaymentPostData postData)
        {
            FormUrlEncodedContent content = ConvertPostData(postData);

            using (HttpResponseMessage response = await self.PostAsync(uri, content))
            {
                if (response.Content == null)
                    return await Task.FromResult<byte[]>(null);

                return await response.Content.ReadAsByteArrayAsync();
            }
        }

        internal static FormUrlEncodedContent ConvertPostData(IPaymentPostData postData)
        {
            KeyValuePair<string, string>[] nameValueCollection = new KeyValuePair<string, string>[3]
            {
                 new KeyValuePair<string, string>("Data", postData.Data),
                 new KeyValuePair<string, string>("InterfaceVersion", postData.InterfaceVersion),
                 new KeyValuePair<string, string>("Seal", postData.Seal),
            };

            return new FormUrlEncodedContent(nameValueCollection);
        }
    }
}

#endif