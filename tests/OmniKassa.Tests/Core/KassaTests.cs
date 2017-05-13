// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NETCOREAPP1_1

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    /// <content/>
    public partial class KassaTests
    {
        [TestMethod]
        public async Task GetPaymentHtml_RequestIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("request", async () =>
            {
                await kassa.GetPaymentHtml(new HttpClient(), null);
            });
        }

        [TestMethod]
        public async Task GetPaymentHtml_HttpClientIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("client", async () =>
            {
                await kassa.GetPaymentHtml(null, new PaymentRequest());
            });
        }

        [TestMethod]
        public async Task GetPaymentHtml_PaymentRequestIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("request", async () =>
            {
                await kassa.GetPaymentHtml(null);
            });
        }

        [TestMethod]
        public async Task GetPaymentHtml_RequestIsInvalid_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);
            PaymentRequest request = new PaymentRequest();

            await ExceptionAssert.ThrowsAsync<InvalidOperationException>("The value for Amount should be higher than 0.", async () =>
            {
                await kassa.GetPaymentHtml(new HttpClient(), request);
            });
        }

        [TestMethod]
        public async Task GetPaymentHtml_HttpClientReturnsResponse_ReturnsResponse()
        {
            Kassa kassa = new Kassa(_configuration);
            TestHttpClient client = new TestHttpClient("TestHttpClient");

            string result = await kassa.GetPaymentHtml(client, _request);

            Assert.AreEqual("TestHttpClient", result);
            Assert.AreEqual("HP_1.0", client.PostedData.InterfaceVersion);
            Assert.AreEqual("merchantId=123456789012345|keyVersion=1|amount=100|currencyCode=208|normalReturnUrl=https://www.github.com/|transactionReference=1234", client.PostedData.Data);
            Assert.AreEqual("903c94c085a024859f4e30322c87f705f509d2ba44446ddeafc1c18a336a82a9", client.PostedData.Seal);
            Assert.AreEqual(new Uri("https://www.github.com"), client.PostedUrl);
        }

        [TestMethod]
        public async Task GetPaymentHtml_HttpClientReturnsNull_ReturnsNull()
        {
            Kassa kassa = new Kassa(_configuration);
            TestHttpClient client = new TestHttpClient(null);

            string result = await kassa.GetPaymentHtml(client, _request);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetPaymentHtml_HttpClientReturnsEmptyString_ReturnsNull()
        {
            Kassa kassa = new Kassa(_configuration);
            TestHttpClient client = new TestHttpClient(string.Empty);

            string result = await kassa.GetPaymentHtml(client, _request);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public async Task GetResponse_HttpRequestIsNull_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("request", async () =>
            {
                await kassa.GetResponse((HttpRequest)null);
            });
        }

        [TestMethod]
        public void GetResponse_ResponseDataIsNull_ReturnsNull()
        {
            Kassa kassa = new Kassa(_configuration);

            IPaymentResponse response = kassa.GetResponse((IFormCollection)null);

            Assert.IsNull(response);
        }

        [TestMethod]
        public void GetResponse_ResponseDataIsValid_ReturnsResponse()
        {
            Kassa kassa = new Kassa(_configuration);

            FormCollection responseData = new FormCollection(new Dictionary<string, StringValues>
            {
                { "Data", new StringValues($"merchantId={_configuration.MerchantId}|amount=4200") },
                { "InterfaceVersion", new StringValues("HP_1.0") },
                { "Seal", new StringValues("0e14ed66182e64d1eed8623d946032648dafa6043f87fc7e4fb8eb2e40469781") }
            });

            IPaymentResponse response = kassa.GetResponse(responseData);

            Assert.IsNotNull(response);
            Assert.AreEqual(42m, response.Amount);
        }

        [TestMethod]
        public async Task GetResponse_PostedDataIsInvalid_ThrowsException()
        {
            Kassa kassa = new Kassa(_configuration);

            await ExceptionAssert.ThrowsAsync<InvalidOperationException>("The value for Data should not be null.", async () =>
            {
                await kassa.GetResponse(new TestHttpRequest());
            });
        }
    }
}

#endif