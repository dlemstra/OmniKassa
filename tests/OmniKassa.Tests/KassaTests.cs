// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public partial class KassaTests
    {
        private readonly KassaConfiguration _configuration = new KassaConfiguration()
        {
            KeyVersion = 1,
            MerchantId = "123456789012345",
            SecretKey = "secret",
            Url = new Uri("https://www.github.com")
        };

        private readonly PaymentRequest _request = new PaymentRequest()
        {
            Amount = 1,
            CurrencyCode = CurrencyCode.DanishCrown,
            ReturnUrl = new Uri("https://www.github.com"),
            TransactionReference = "1234",
        };

        [TestMethod]
        public void Constructor_ConfigurationIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
            {
                new Kassa(null);
            });
        }

        [TestMethod]
        public void Constructor_ConfigurationIsInvalid_ThrowsException()
        {
            KassaConfiguration configuration = new KassaConfiguration();

            ExceptionAssert.Throws<InvalidOperationException>("The value for MerchantId should not be null.", () =>
            {
                new Kassa(configuration);
            });
        }

        [TestMethod]
        public void Constructor_WithConfiguration_ConfigurationIsSet()
        {
            Kassa kassa = new Kassa(_configuration);

            Assert.AreEqual(_configuration, kassa.Configuration);
        }

        [TestMethod]
        public void GetResponse_ResponseValid_ReturnsResult()
        {
            Kassa kassa = new Kassa(_configuration);
            PaymentPostData postData = new PaymentPostData()
            {
                Data = $"merchantId={_configuration.MerchantId}|amount=4200",
                Seal = "0e14ed66182e64d1eed8623d946032648dafa6043f87fc7e4fb8eb2e40469781",
            };

            IPaymentResponse response = kassa.GetResponse(postData);

            Assert.IsNotNull(response);
            Assert.AreEqual(42.0m, response.Amount);
        }

        [TestMethod]
        public void GetResponse_ResponseIsInvalid_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);
            PaymentPostData postData = new PaymentPostData()
            {
                Data = "test=test",
                Seal = "seal",
            };

            ExceptionAssert.Throws<InvalidOperationException>($"The seal is invalid.{Environment.NewLine}Expected value: f5db08a8cfdc7245247e8ad08d88e12e96435ce1bd11c781ab170c80d6a4668d.", () =>
            {
                kassa.GetResponse(postData);
            });
        }
    }
}
