// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the data that is posted to OmniKassa or to the website of the merchant.
    /// </summary>
    public sealed class PaymentPostData : IPaymentPostData
    {
        /// <summary>
        /// Gets or sets the information about the transaction.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the version of the Rabo OmniKassa protocol.
        /// </summary>
        public string InterfaceVersion { get; set; } = KassaConfiguration.InterfaceVersion;

        /// <summary>
        /// Gets or sets the seal of the data.
        /// </summary>
        public string Seal { get; set; }
    }
}
