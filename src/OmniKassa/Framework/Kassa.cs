// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NET35

using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace OmniKassa
{
    /// <content>
    /// Contains the .NET 3.5 implementation.
    /// </content>
    public sealed partial class Kassa : IKassa
    {
        private readonly object _lock = new object();
        private HttpClient _client;

        /// <summary>
        /// Returns the HTML that should be send to the customer that wants to start a payment.
        /// </summary>
        /// <param name="request">The payment request.</param>
        /// <returns>The HTML that should be send to the customer that wants to start a payment.</returns>
        public string GetPaymentHtml(IPaymentRequest request)
        {
            CreateClient();

            return GetPaymentHtml(_client, request);
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

            return GetResponse(postData);
        }

        internal string GetPaymentHtml(IHttpClient client, IPaymentRequest request)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            IPaymentPostData postData = CreatePostData(request);

            byte[] responseData = client.PostData(Configuration.Url, postData);
            if (responseData == null)
                return null;

            return Encoding.UTF8.GetString(responseData);
        }

        private void CreateClient()
        {
            if (_client != null)
                return;

            lock (_lock)
            {
                if (_client == null)
                    _client = new HttpClient();
            }
        }
    }
}

#endif