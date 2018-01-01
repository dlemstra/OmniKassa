﻿// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NET35

using System;

namespace OmniKassa
{
    internal interface IHttpClient : IDisposable
    {
        byte[] PostData(Uri uri, IPaymentPostData postData);
    }
}

#endif
