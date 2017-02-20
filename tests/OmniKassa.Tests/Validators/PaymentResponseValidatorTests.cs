// <copyright file="PaymentResponseValidatorTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class PaymentResponseValidatorTests
    {
        private readonly KassaConfiguration _configuration = new KassaConfiguration()
        {
            KeyVersion = 1,
            MerchantId = "123456789012345",
            SecretKey = "secret",
            Url = new Uri("https://www.github.com")
        };

        [TestMethod]
        public void Validate_ConfigurationIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
            {
                PaymentResponseValidator.Validate(null, new PaymentResponse());
            });
        }

        [TestMethod]
        public void Validate_ResponseIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("response", () =>
            {
                PaymentResponseValidator.Validate(new KassaConfiguration(), null);
            });
        }

        [TestMethod]
        public void Validate_MerchantIdIsInvalid_ThrowwsException()
        {
            PaymentResponse response = new PaymentResponse()
            {
                MerchantId = "test"
            };

            ExceptionAssert.Throws<InvalidOperationException>($"Invalid merchant id.{Environment.NewLine}Expected value: {_configuration.MerchantId}.", () =>
            {
                PaymentResponseValidator.Validate(_configuration, response);
            });
        }

        [TestMethod]
        public void Validate_IsValid_NoException()
        {
            PaymentResponse response = new PaymentResponse()
            {
                MerchantId = "123456789012345"
            };

            PaymentResponseValidator.Validate(_configuration, response);
        }
    }
}
