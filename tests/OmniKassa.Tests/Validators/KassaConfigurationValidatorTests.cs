// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class KassaConfigurationValidatorTests
    {
        [TestMethod]
        public void Validate_ConfigurationIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
            {
                KassaConfigurationValidator.Validate(null);
            });
        }

        [TestMethod]
        public void Validate_IsValid_NoException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = "123456789012345",
                SecretKey = "secret",
                Url = new Uri("https://www.github.com")
            };

            KassaConfigurationValidator.Validate(configuration);
        }

        [TestMethod]
        public void Validate_KeyVersionLessThan1_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 0,
                MerchantId = "merchant",
                SecretKey = "secret",
                Url = new Uri("https://www.github.com")
            };

            ExceptionAssert.ThrowsHigherThanValidationException(nameof(configuration.KeyVersion), 0, () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_MerchantIdIsNull_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = null,
                SecretKey = "secret",
                Url = new Uri("https://www.github.com")
            };

            ExceptionAssert.ThrowsNullValidationException(nameof(configuration.MerchantId), () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_MerchantIdIsEmpty_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = string.Empty,
                SecretKey = "secret",
                Url = new Uri("https://www.github.com")
            };

            ExceptionAssert.ThrowsEmptyValidationException(nameof(configuration.MerchantId), () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_MerchantIdIsWhitespace_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = "   ",
                SecretKey = "secret",
                Url = new Uri("https://www.github.com")
            };

            ExceptionAssert.ThrowsWhitespaceValidationException(nameof(configuration.MerchantId), () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_MerchantIdIsLongerThanMaxLength_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = "1234567890123456",
                SecretKey = "secret",
                Url = new Uri("https://www.github.com")
            };

            ExceptionAssert.ThrowsLengthValidationException(nameof(configuration.MerchantId), 15, () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_MerchantIdHasNonAlphanumericCharacter_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = "%",
                SecretKey = "secret",
                Url = new Uri("https://www.github.com")
            };

            ExceptionAssert.ThrowsAlphanumericValidationException(nameof(configuration.MerchantId), () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_SecretKeyIsNull_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = "merchant",
                SecretKey = null,
                Url = new Uri("https://www.github.com")
            };

            ExceptionAssert.ThrowsNullValidationException(nameof(configuration.SecretKey), () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_SecretKeyIsEmpty_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = "merchant",
                SecretKey = string.Empty,
                Url = new Uri("https://www.github.com")
            };

            ExceptionAssert.ThrowsEmptyValidationException(nameof(configuration.SecretKey), () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_SecretKeyIsWhitespace_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = "merchant",
                SecretKey = "   ",
                Url = new Uri("https://www.github.com")
            };

            ExceptionAssert.ThrowsWhitespaceValidationException(nameof(configuration.SecretKey), () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_UrlIsNull_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = "merchant",
                SecretKey = "secret",
                Url = null
            };

            ExceptionAssert.ThrowsNullValidationException(nameof(configuration.Url), () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }

        [TestMethod]
        public void Validate_UrlContainsSeparator_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration()
            {
                KeyVersion = 1,
                MerchantId = "merchant",
                SecretKey = "secret",
                Url = new Uri("https://github.com/?a=|")
            };

            ExceptionAssert.ThrowsSeparatorValidationException(nameof(configuration.Url), () =>
            {
                KassaConfigurationValidator.Validate(configuration);
            });
        }
    }
}
