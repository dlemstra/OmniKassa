// <copyright file="Kassa.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;
using System.Collections.Specialized;
using System.Web;

namespace OmniKassa
{
    /// <content>
    /// Contains the .NET 3.5 implementation.
    /// </content>
    public sealed partial class Kassa : IKassa
    {
        /// <summary>
        /// Returns the HTML that should be send to the customer that wants to start a payment.
        /// </summary>
        /// <param name="request">The payment request.</param>
        /// <returns>The HTML that should be send to the customer that wants to start a payment.</returns>
        public string GetPaymentHtml(IPaymentRequest request)
        {
            using (WebHelper webHelper = new WebHelper())
            {
                return GetPaymentHtml(request, webHelper);
            }
        }

        /// <summary>
        /// Returns the response from the payment provider for a payment request.
        /// </summary>
        /// <param name="request">The http request that contains the form with the response.</param>
        /// <returns>The response from the payment provider for a payment request.</returns>
        public IPaymentResponse GetResponse(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return GetResponse(request.Form);
        }

        /// <summary>
        /// Returns the response from the payment provider for a payment request.
        /// </summary>
        /// <param name="responseData">The form data with the response.</param>
        /// <returns>The response from the payment provider for a payment request.</returns>
        public IPaymentResponse GetResponse(NameValueCollection responseData)
        {
            if (responseData == null)
                return null;

            PaymentPostData postData = new PaymentPostData()
            {
                Data = responseData["Data"],
                InterfaceVersion = responseData["InterfaceVersion"],
                Seal = responseData["Seal"],
            };

            using (WebHelper httpHelper = new WebHelper(postData))
            {
                return GetResponse(httpHelper);
            }
        }
    }
}