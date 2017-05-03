// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the response code.
    /// </summary>
    [Flags]
    public enum ResponseCode
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// Awaiting status report = 60.
        /// </summary>
        AwaitingStatusReport = 2,

        /// <summary>
        /// Cancelled.
        /// </summary>
        Cancelled = Cancelled_17 | Cancelled_90,

        /// <summary>
        /// Cancelled = 17.
        /// </summary>
        Cancelled_17 = 4,

        /// <summary>
        /// Cancelled = 90.
        /// </summary>
        Cancelled_90 = 8,

        /// <summary>
        /// Expired = 97.
        /// </summary>
        Expired = 16,

        /// <summary>
        /// Referral = 02.
        /// </summary>
        Referral = 32,

        /// <summary>
        /// Refused.
        /// </summary>
        Refused = Refused_03 | Refused_05 | Refused_12 | Refused_14 | Refused_25 | Refused_30 | Refused_75 | Refused_89,

        /// <summary>
        /// Refused = 03.
        /// </summary>
        Refused_03 = 64,

        /// <summary>
        /// Refused = 05.
        /// </summary>
        Refused_05 = 128,

        /// <summary>
        /// Refused = 12.
        /// </summary>
        Refused_12 = 256,

        /// <summary>
        /// Refused = 14.
        /// </summary>
        Refused_14 = 512,

        /// <summary>
        /// Refused = 25.
        /// </summary>
        Refused_25 = 1024,

        /// <summary>
        /// Refused = 30.
        /// </summary>
        Refused_30 = 2048,

        /// <summary>
        /// Refused = 75.
        /// </summary>
        Refused_75 = 4096,

        /// <summary>
        /// Refused = 89.
        /// </summary>
        Refused_89 = 8192,

        /// <summary>
        /// Successful = 00.
        /// </summary>
        Successful = 16384,
    }
}
