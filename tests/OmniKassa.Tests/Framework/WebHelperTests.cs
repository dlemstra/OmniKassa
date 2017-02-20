// <copyright file="WebHelperTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class WebHelperTests
    {
        [TestMethod]
        public void Constructor_NoArguments_GetPostDataReturnsNull()
        {
            WebHelper webHelper = new WebHelper();

            Assert.IsNull(webHelper.GetPostData());
        }

        [TestMethod]
        public void Constructor_WithPostData_GetPostDataReturnsValue()
        {
            PaymentPostData postData = new PaymentPostData();

            WebHelper webHelper = new WebHelper(postData);

            Assert.AreEqual(postData, webHelper.GetPostData());
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

            NameValueCollection content = WebHelper.ConvertPostData(postData);

            Assert.IsNotNull(content);
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual("1", content["Data"]);
            Assert.AreEqual("2", content["InterfaceVersion"]);
            Assert.AreEqual("3", content["Seal"]);
        }
    }
}