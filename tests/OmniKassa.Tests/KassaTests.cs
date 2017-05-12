// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    /// <content/>
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
    }
}
