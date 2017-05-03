// <copyright file="PaymentPostDataValidator.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;

namespace OmniKassa
{
    internal sealed class PaymentPostDataValidator : Validator
    {
        private readonly IKassaConfiguration _configuration;
        private readonly IPaymentPostData _postData;

        private PaymentPostDataValidator(IKassaConfiguration configuration, IPaymentPostData postData)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (postData == null)
                throw new ArgumentNullException(nameof(postData));

            _configuration = configuration;
            _postData = postData;
        }

        public static void Validate(IKassaConfiguration configuration, IPaymentPostData postData)
        {
            PaymentPostDataValidator validator = new PaymentPostDataValidator(configuration, postData);
            validator.Validate();
        }

        private void Validate()
        {
            ValidateData();
            ValidateSeal();
        }

        private void ValidateData()
        {
            StringValidator validator = new StringValidator(nameof(_postData.Data), _postData.Data);
            validator.IsNotNullOrWhiteSpace();
        }

        private void ValidateSeal()
        {
            string seal = Seal.Create(_postData.Data, _configuration.SecretKey);
            if (seal != _postData.Seal)
                ThrowException("The seal is invalid.", seal, _postData.Seal);
        }
    }
}
