// <copyright file="KassaConfigurationTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class KassaConfigurationTests
    {
        public void Constructor_PropertiesAreInitialized()
        {
            KassaConfiguration configuration = new KassaConfiguration();

            Assert.AreEqual(1, configuration.KeyVersion);
            Assert.IsNull(configuration.MerchantId);
            Assert.IsNull(configuration.SecretKey);
            Assert.AreEqual(new Uri("https://payment-webinit.simu.omnikassa.rabobank.nl/paymentServlet"), configuration.Url);
        }
    }
}
