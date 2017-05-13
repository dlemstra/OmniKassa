// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NETCOREAPP1_1

using System.Linq;
using System.Net.Http;

namespace OmniKassa.Tests
{
    [ExcludeFromCodeCoverage]
    public static class HttpRequestMessageExtensions
    {
        public static string GetPostedData(this HttpRequestMessage self)
        {
            return self.Headers.GetValues(nameof(HttpRequestMessageExtensions)).FirstOrDefault();
        }

        public static void SetPostedData(this HttpRequestMessage self)
        {
            if (self.Content is FormUrlEncodedContent content)
                self.Headers.Add(nameof(HttpRequestMessageExtensions), content.ReadAsStringAsync().Result);
        }
    }
}

#endif