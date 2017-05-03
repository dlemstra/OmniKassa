// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the data that is posted to OmniKassa or to the website of the merchant.
    /// </summary>
    public interface IPaymentPostData
    {
        /// <summary>
        /// Gets the information about the transaction.
        /// </summary>
        string Data { get; }

        /// <summary>
        /// Gets the version of the Rabo OmniKassa protocol.
        /// </summary>
        string InterfaceVersion { get; }

        /// <summary>
        /// Gets the seal of the data.
        /// </summary>
        string Seal { get; }
    }
}