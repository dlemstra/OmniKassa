// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the response status.
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// Awaiting status report.
        /// </summary>
        AwaitingStatusReport,

        /// <summary>
        /// Cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// Expired.
        /// </summary>
        Expired,

        /// <summary>
        /// Referral.
        /// </summary>
        Referral,

        /// <summary>
        /// Refused.
        /// </summary>
        Refused,

        /// <summary>
        /// Successful.
        /// </summary>
        Successful,
    }
}
