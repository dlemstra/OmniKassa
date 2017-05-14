// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    internal sealed class KassaConfigurationValidator : Validator
    {
        private readonly IKassaConfiguration _configuration;

        private KassaConfigurationValidator(IKassaConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public static void Validate(IKassaConfiguration configuration)
        {
            KassaConfigurationValidator validator = new KassaConfigurationValidator(configuration);
            validator.Validate();
        }

        private void Validate()
        {
            ValidateKeyVersion();
            ValidateMerchantId();
            ValidateSecretKey();
            ValidateUrl();
        }

        private void ValidateKeyVersion()
        {
            IntValidator validator = new IntValidator(nameof(_configuration.KeyVersion), _configuration.KeyVersion);
            validator.IsHigherThan(0);
        }

        private void ValidateMerchantId()
        {
            StringValidator validator = new StringValidator(nameof(_configuration.MerchantId), _configuration.MerchantId);
            validator.IsNotNullOrWhiteSpace();
            validator.IsNotLongerThan(15);
            validator.IsAlphanumeric();
        }

        private void ValidateSecretKey()
        {
            StringValidator validator = new StringValidator(nameof(_configuration.SecretKey), _configuration.SecretKey);
            validator.IsNotNullOrWhiteSpace();
        }

        private void ValidateUrl()
        {
            StringValidator validator = new StringValidator(nameof(_configuration.Url), _configuration.Url);
            validator.IsNotNullOrWhiteSpace();
            validator.DoesNotContainSeparator();
        }
    }
}
