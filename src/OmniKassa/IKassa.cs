// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the communication with OmniKassa.
    /// </summary>
    public partial interface IKassa
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        IKassaConfiguration Configuration { get; }

        /// <summary>
        /// Returns the HTML that should be send to the customer that wants to start a payment.
        /// </summary>
        /// <param name="request">The payment request.</param>
        /// <param name="httpHelper">The http helper.</param>
        /// <returns>The HTML that should be send to the customer that wants to start a payment.</returns>
        string GetPaymentHtml(IPaymentRequest request, IWebHelper httpHelper);

        /// <summary>
        /// Returns the response from the payment provider for a payment request.
        /// </summary>
        /// <param name="httpHelper">The http helper.</param>
        /// <returns>The response from the payment provider for a payment request.</returns>
        IPaymentResponse GetResponse(IWebHelper httpHelper);
    }
}
