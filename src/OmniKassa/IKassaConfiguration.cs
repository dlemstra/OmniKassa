// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the configuration of OmniKassa.
    /// </summary>
    public interface IKassaConfiguration
    {
        /// <summary>
        /// Gets the version number of the secret key. Can be found on the Rabo OmniKassa Downloadsite.
        /// </summary>
        int KeyVersion { get; }

        /// <summary>
        /// Gets the identifier of the merchant/webshop.
        /// </summary>
        string MerchantId { get; }

        /// <summary>
        /// Gets the secret key that will be used to create the seal.
        /// </summary>
        string SecretKey { get; }

        /// <summary>
        /// Gets the url of the Rabo OmniKassa.
        /// </summary>
        Uri Url { get; }
    }
}
