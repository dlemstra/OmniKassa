// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    internal sealed class PaymentResponseValidator : Validator
    {
        private readonly IKassaConfiguration _configuration;
        private readonly IPaymentResponse _response;

        private PaymentResponseValidator(IKassaConfiguration configuration, IPaymentResponse response)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _response = response ?? throw new ArgumentNullException(nameof(response));
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
