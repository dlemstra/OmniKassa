// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

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

        internal IPaymentResponse GetResponse(IPaymentPostData response)
        {
            PaymentPostDataValidator.Validate(Configuration, response);

            IPaymentResponse result = PaymentResponse.Create(response.Data);
            PaymentResponseValidator.Validate(Configuration, result);

            return result;
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
    }
}
