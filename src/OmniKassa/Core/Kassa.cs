// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NETSTANDARD1_3

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OmniKassa
{
    /// <content>
    /// Contains the .NET Standard 1.3 implementation.
    /// </content>
    public sealed partial class Kassa : IKassa
    {
        /// <summary>
        /// Returns the HTML that should be send to the customer that wants to start a payment.
        /// </summary>
        /// <param name="request">The payment request.</param>
        /// <returns>The HTML that should be send to the customer that wants to start a payment.</returns>
        public async Task<string> GetPaymentHtml(IPaymentRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                return await GetPaymentHtml(client, request);
            }
        }

        /// <summary>
        /// Returns the response from the payment provider for a payment request.
        /// </summary>
        /// <param name="request">The http request that contains the form with the response.</param>
        /// <returns>The response from the payment provider for a payment request.</returns>
        public async Task<IPaymentResponse> GetResponse(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            IFormCollection responseData = await request.ReadFormAsync();
            return GetResponse(responseData);
        }

        /// <summary>
        /// Returns the response from the payment provider for a payment request.
        /// </summary>
        /// <param name="responseData">The form data with the response.</param>
        /// <returns>The response from the payment provider for a payment request.</returns>
        public IPaymentResponse GetResponse(IFormCollection responseData)
        {
            if (responseData == null)
                return null;

            PaymentPostData postData = new PaymentPostData()
            {
                Data = responseData["Data"],
                Seal = responseData["Seal"],
                InterfaceVersion = responseData["InterfaceVersion "],
            };

            return GetResponse(postData);
        }

        internal async Task<string> GetPaymentHtml(HttpClient client, IPaymentRequest request)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            IPaymentPostData postData = CreatePostData(request);

            byte[] responseData = await client.PostData(Configuration.Url, postData);
            if (responseData == null)
                return null;

            return Encoding.UTF8.GetString(responseData);
        }
    }
}

#endif