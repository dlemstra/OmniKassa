// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class TestKassaConfigurationTests
    {
        public void Constructor_PropertiesAreInitialized()
        {
            TestKassaConfiguration configuration = new TestKassaConfiguration();

            Assert.AreEqual(1, configuration.KeyVersion);
            Assert.AreEqual("002020000000001", configuration.MerchantId);
            Assert.AreEqual("002020000000001_KEY1", configuration.SecretKey);
            Assert.AreEqual(new Uri("https://payment-webinit.simu.omnikassa.rabobank.nl/paymentServlet"), configuration.Url);
        }
    }
}
