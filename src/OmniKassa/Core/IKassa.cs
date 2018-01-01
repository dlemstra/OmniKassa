// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NETSTANDARD1_3

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OmniKassa
{
    /// <content>
    /// Contains the .NET Standard 1.3 implementation.
    /// </content>
    public partial interface IKassa
    {
        /// <summary>
        /// Returns the HTML that should be send to the customer that wants to start a payment.
        /// </summary>
        /// <param name="request">The payment request.</param>
        /// <returns>The HTML that should be send to the customer that wants to start a payment.</returns>
        Task<string> GetPaymentHtml(IPaymentRequest request);

        /// <summary>
        /// Returns the response from the payment provider for a payment request.
        /// </summary>
        /// <param name="request">The http request that contains the form with the response.</param>
        /// <returns>The response from the payment provider for a payment request.</returns>
        Task<IPaymentResponse> GetResponse(HttpRequest request);

        /// <summary>
        /// Returns the response from the payment provider for a payment request.
        /// </summary>
        /// <param name="responseData">The form data with the response.</param>
        /// <returns>The response from the payment provider for a payment request.</returns>
        IPaymentResponse GetResponse(IFormCollection responseData);
    }
}

#endif