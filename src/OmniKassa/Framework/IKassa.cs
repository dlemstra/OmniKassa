// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NET35

using System.Collections.Specialized;
using System.Web;

namespace OmniKassa
{
    /// <content>
    /// Contains the .NET 3.5 implementation.
    /// </content>
    public partial interface IKassa
    {
        /// <summary>
        /// Returns the HTML that should be send to the customer that wants to start a payment.
        /// </summary>
        /// <param name="request">The payment request.</param>
        /// <returns>The HTML that should be send to the customer that wants to start a payment.</returns>
        string GetPaymentHtml(IPaymentRequest request);

        /// <summary>
        /// Returns the response from the payment provider for a payment request.
        /// </summary>
        /// <param name="request">The http request that contains the form with the response.</param>
        /// <returns>The response from the payment provider for a payment request.</returns>
        IPaymentResponse GetResponse(HttpRequest request);

        /// <summary>
        /// Returns the response from the payment provider for a payment request.
        /// </summary>
        /// <param name="responseData">The form data with the response.</param>
        /// <returns>The response from the payment provider for a payment request.</returns>
        IPaymentResponse GetResponse(NameValueCollection responseData);
    }
}

#endif