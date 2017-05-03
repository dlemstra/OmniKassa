// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System.Collections.Specialized;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    /// <content/>
    public partial class KassaTests
    {
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
    }
}