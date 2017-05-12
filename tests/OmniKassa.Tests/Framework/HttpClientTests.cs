// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NET45

using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class HttpClientTests
    {
        [TestMethod]
        public void ConvertPostData_WithPostData_ReturnsNameValueCollection()
        {
            PaymentPostData postData = new PaymentPostData()
            {
                Data = "1",
                InterfaceVersion = "2",
                Seal = "3"
            };

            NameValueCollection content = HttpClient.ConvertPostData(postData);

            Assert.IsNotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual("1", content["Data"]);
            Assert.AreEqual("2", content["InterfaceVersion"]);
            Assert.AreEqual("3", content["Seal"]);
        }
    }
}

#endif