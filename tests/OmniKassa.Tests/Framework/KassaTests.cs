// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NET45

using System;
using System.Collections.Specialized;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    /// <content/>
    public partial class KassaTests
    {
        [TestMethod]
        public void GetPaymentHtml_RequestIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("request", () =>
            {
                kassa.GetPaymentHtml(new HttpClient(), null);
            });
        }

        [TestMethod]
        public void GetPaymentHtml_HttpClientIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("client", () =>
            {
                kassa.GetPaymentHtml(null, new PaymentRequest());
            });
        }

        [TestMethod]
        public void GetPaymentHtml_PaymentRequestIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("request", () =>
            {
                kassa.GetPaymentHtml(null);
            });
        }

        [TestMethod]
        public void GetPaymentHtml_RequestIsInvalid_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);
            PaymentRequest request = new PaymentRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The value for Amount should be higher than 0.", () =>
            {
                kassa.GetPaymentHtml(new HttpClient(), request);
            });
        }

        [TestMethod]
        public void GetPaymentHtml_HttpClientReturnsResponse_ReturnsResponse()
        {
            Kassa kassa = new Kassa(_configuration);
            TestHttpClient client = new TestHttpClient("TestHttpClient");

            string result = kassa.GetPaymentHtml(client, _request);

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

            string result = kassa.GetPaymentHtml(client, _request);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPaymentHtml_HttpClientReturnsEmptyArray_ReturnsNull()
        {
            Kassa kassa = new Kassa(_configuration);
            TestHttpClient client = new TestHttpClient(string.Empty);

            string result = kassa.GetPaymentHtml(client, _request);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetResponse_HttpRequestIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.ThrowsArgumentNullException("request", () =>
            {
                kassa.GetResponse((HttpRequest)null);
            });
        }

        [TestMethod]
        public void GetResponse_ResponseDataIsNull_ReturnsNull()
        {
            Kassa kassa = new Kassa(_configuration);

            IPaymentResponse response = kassa.GetResponse((NameValueCollection)null);

            Assert.IsNull(response);
        }

        [TestMethod]
        public void GetResponse_ResponseDataIsValid_ReturnsResponse()
        {
            Kassa kassa = new Kassa(_configuration);

            NameValueCollection responseData = new NameValueCollection()
            {
                { "Data", $"merchantId={_configuration.MerchantId}|amount=4200" },
                { "InterfaceVersion", "HP_1.0" },
                { "Seal", "0e14ed66182e64d1eed8623d946032648dafa6043f87fc7e4fb8eb2e40469781" }
            };

            IPaymentResponse response = kassa.GetResponse(responseData);

            Assert.IsNotNull(response);
            Assert.AreEqual(42m, response.Amount);
        }

        [TestMethod]
        public void GetResponse_PostedDataIsInvalid_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            ExceptionAssert.Throws<InvalidOperationException>("The value for Data should not be null.", () =>
            {
                kassa.GetResponse(new HttpRequest("foo", "https://bar.com", string.Empty));
            });
        }
    }
}

#endif