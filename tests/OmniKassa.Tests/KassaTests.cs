// <copyright file="KassaTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

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

        [TestMethod]
        public void GetPaymentHtml_RequestIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("request", () =>
            {
                kassa.GetPaymentHtml(null, new WebHelper());
            });
        }

        [TestMethod]
        public void GetPaymentHtml_WebHelperIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("webHelper", () =>
            {
                kassa.GetPaymentHtml(new PaymentRequest(), null);
            });
        }

        [TestMethod]
        public void GetPaymentHtml_RequestIsInvalid_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);
            PaymentRequest request = new PaymentRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The value for Amount should be higher than 0.", () =>
            {
                kassa.GetPaymentHtml(request, new WebHelper());
            });
        }

        [TestMethod]
        public void GetPaymentHtml_WebHelperReturnsResponse_ReturnsResponse()
        {
            Kassa kassa = new Kassa(_configuration);
            TestWebHelper webHelper = new TestWebHelper("TestWebHelper");

            string result = kassa.GetPaymentHtml(_request, webHelper);

            Assert.AreEqual("TestWebHelper", result);
            Assert.AreEqual("HP_1.0", webHelper.PostedData.InterfaceVersion);
            Assert.AreEqual("merchantId=123456789012345|keyVersion=1|amount=100|currencyCode=208|normalReturnUrl=https://www.github.com/|transactionReference=1234", webHelper.PostedData.Data);
            Assert.AreEqual("903c94c085a024859f4e30322c87f705f509d2ba44446ddeafc1c18a336a82a9", webHelper.PostedData.Seal);
            Assert.AreEqual(new Uri("https://www.github.com"), webHelper.PostedUrl);
        }

        [TestMethod]
        public void GetPaymentHtml_WebHelperReturnsNull_ReturnsNull()
        {
            Kassa kassa = new Kassa(_configuration);
            TestWebHelper webHelper = new TestWebHelper((string)null);

            string result = kassa.GetPaymentHtml(_request, webHelper);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPaymentHtml_WebHelperReturnsEmptyArray_ReturnsNull()
        {
            Kassa kassa = new Kassa(_configuration);
            TestWebHelper webHelper = new TestWebHelper(string.Empty);

            string result = kassa.GetPaymentHtml(_request, webHelper);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetResponse_WebHelperIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("webHelper", () =>
            {
                kassa.GetResponse((IWebHelper)null);
            });
        }

        [TestMethod]
        public void GetResponse_PostedDataIsInvalid_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);
            PaymentPostData postData = new PaymentPostData();
            TestWebHelper webHelper = new TestWebHelper(postData);

            ExceptionAssert.Throws<InvalidOperationException>("The value for Data should not be null.", () =>
            {
                kassa.GetResponse(webHelper);
            });
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
            TestWebHelper webHelper = new TestWebHelper(postData);

            ExceptionAssert.Throws<InvalidOperationException>($"The seal is invalid.{Environment.NewLine}Expected value: f5db08a8cfdc7245247e8ad08d88e12e96435ce1bd11c781ab170c80d6a4668d.", () =>
            {
                kassa.GetResponse(webHelper);
            });
        }

        [TestMethod]
        public void GetResponse_ResponseIsInvalid_ReturnsResult()
        {
            Kassa kassa = new Kassa(_configuration);
            PaymentPostData postData = new PaymentPostData()
            {
                Data = $"merchantId={_configuration.MerchantId}|amount=4200",
                Seal = "0e14ed66182e64d1eed8623d946032648dafa6043f87fc7e4fb8eb2e40469781",
            };
            TestWebHelper webHelper = new TestWebHelper(postData);

            IPaymentResponse response = kassa.GetResponse(webHelper);

            Assert.IsNotNull(response);
            Assert.AreEqual(42.0m, response.Amount);
        }
    }
}
