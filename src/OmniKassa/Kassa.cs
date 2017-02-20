// <copyright file="Kassa.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;
using System.Text;

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the communication with OmniKassa.
    /// </summary>
    public sealed partial class Kassa : IKassa
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Kassa"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Kassa(IKassaConfiguration configuration)
        {
            KassaConfigurationValidator.Validate(configuration);

            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IKassaConfiguration Configuration { get; }

        /// <summary>
        /// Returns the HTML that should be send to the customer that wants to start a payment.
        /// </summary>
        /// <param name="request">The payment request.</param>
        /// <param name="webHelper">The web helper.</param>
        /// <returns>The HTML that should be send to the customer that wants to start a payment.</returns>
        public string GetPaymentHtml(IPaymentRequest request, IWebHelper webHelper)
        {
            if (webHelper == null)
                throw new ArgumentNullException(nameof(webHelper));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            IPaymentPostData postData = CreatePostData(request);

            byte[] responseData = webHelper.PostData(Configuration.Url, postData);
            if (responseData == null)
                return null;

            return Encoding.UTF8.GetString(responseData);
        }

        /// <summary>
        /// Returns the payment response that was send to the specified http helper.
        /// </summary>
        /// <param name="webHelper">The web helper.</param>
        /// <returns>The payment response that was send to the specified http helper.</returns>
        public IPaymentResponse GetResponse(IWebHelper webHelper)
        {
            if (webHelper == null)
                throw new ArgumentNullException(nameof(webHelper));

            IPaymentPostData postData = webHelper.GetPostData();

            return GetResponse(postData);
        }

        private IPaymentPostData CreatePostData(IPaymentRequest request)
        {
            PaymentRequestValidator.Validate(request);

            string data = DataString.Create(Configuration, request);
            string seal = Seal.Create(data, Configuration.SecretKey);

            return new PaymentPostData()
            {
                Data = data,
                Seal = seal,
            };
        }

        private IPaymentResponse GetResponse(IPaymentPostData response)
        {
            PaymentPostDataValidator.Validate(Configuration, response);

            IPaymentResponse result = PaymentResponse.Create(response.Data);
            PaymentResponseValidator.Validate(Configuration, result);

            return result;
        }
    }
}
