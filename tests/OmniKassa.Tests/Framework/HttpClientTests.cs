// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class HttpClientTests
    {
        [TestMethod]
        public void Constructor_NoArguments_GetPostDataReturnsNull()
        {
            HttpClient client = new HttpClient();

            Assert.IsNull(client.GetPostData());
        }

        [TestMethod]
        public void Constructor_WithPostData_GetPostDataReturnsValue()
        {
            PaymentPostData postData = new PaymentPostData();

            HttpClient client = new HttpClient(postData);

            Assert.AreEqual(postData, client.GetPostData());
        }

        [TestMethod]
        public void ConvertPostData_WithPostData_ReturnsFormUrlEncodedContent()
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