// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    internal interface IHttpClient : IDisposable
    {
        byte[] PostData(Uri uri, IPaymentPostData postData);

        IPaymentPostData GetPostData();
    }
}
