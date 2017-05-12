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

        [TestMethod]
        public void GetPaymentHtml_RequestIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("request", () =>
            {
                kassa.GetPaymentHtml(null, new HttpClient());
            });
        }

        [TestMethod]
        public void GetPaymentHtml_HttpClientIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("client", () =>
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
                kassa.GetPaymentHtml(request, new HttpClient());
            });
        }

        [TestMethod]
        public void GetPaymentHtml_HttpClientReturnsResponse_ReturnsResponse()
        {
            Kassa kassa = new Kassa(_configuration);
            TestHttpClient client = new TestHttpClient("TestHttpClient");

            string result = kassa.GetPaymentHtml(_request, client);

            Assert.AreEqual("TestHttpClient", result);
            Assert.AreEqual("HP_1.0", client.PostedData.InterfaceVersion);
            Assert.AreEqual("merchantId=123456789012345|keyVersion=1|amount=100|currencyCode=208|normalReturnUrl=https://www.github.com/|transactionReference=1234", client.PostedData.Data);
            Assert.AreEqual("903c94c085a024859f4e30322c87f705f509d2ba44446ddeafc1c18a336a82a9", client.PostedData.Seal);
            Assert.AreEqual(new Uri("https://www.github.com"), client.PostedUrl);
        }

        [TestMethod]
        public void GetPaymentHtml_HttpClientReturnsNull_ReturnsNull()
        {
            Kassa kassa = new Kassa(_configuration);
            TestHttpClient client = new TestHttpClient((string)null);

            string result = kassa.GetPaymentHtml(_request, client);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPaymentHtml_HttpClientReturnsEmptyArray_ReturnsNull()
        {
            Kassa kassa = new Kassa(_configuration);
            TestHttpClient client = new TestHttpClient(string.Empty);

            string result = kassa.GetPaymentHtml(_request, client);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetResponse_HttpClientIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("client", () =>
            {
                kassa.GetResponse((IHttpClient)null);
            });
        }

        [TestMethod]
        public void GetResponse_PostedDataIsInvalid_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);
            PaymentPostData postData = new PaymentPostData();
            TestHttpClient client = new TestHttpClient(postData);

            ExceptionAssert.Throws<InvalidOperationException>("The value for Data should not be null.", () =>
            {
                kassa.GetResponse(client);
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
            TestHttpClient client = new TestHttpClient(postData);

            ExceptionAssert.Throws<InvalidOperationException>($"The seal is invalid.{Environment.NewLine}Expected value: f5db08a8cfdc7245247e8ad08d88e12e96435ce1bd11c781ab170c80d6a4668d.", () =>
            {
                kassa.GetResponse(client);
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
            TestHttpClient client = new TestHttpClient(postData);

            IPaymentResponse response = kassa.GetResponse(client);

            Assert.IsNotNull(response);
            Assert.AreEqual(42.0m, response.Amount);
        }
    }
}
