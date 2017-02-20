// <copyright file="PaymentResponseValidator.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;

namespace OmniKassa
{
    internal sealed class PaymentResponseValidator : Validator
    {
        private readonly IKassaConfiguration _configuration;
        private readonly IPaymentResponse _response;

        private PaymentResponseValidator(IKassaConfiguration configuration, IPaymentResponse response)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (response == null)
                throw new ArgumentNullException(nameof(response));

            _configuration = configuration;
            _response = response;
        }

        public static void Validate(IKassaConfiguration configuration, IPaymentResponse response)
        {
            PaymentResponseValidator validator = new PaymentResponseValidator(configuration, response);
            validator.Validate();
        }

        private void Validate()
        {
            ValidateMerchantId();
        }

        private void ValidateMerchantId()
        {
            if (_configuration.MerchantId != _response.MerchantId)
                ThrowException("Invalid merchant id.", _configuration.MerchantId, _response.MerchantId);
        }
    }
}
