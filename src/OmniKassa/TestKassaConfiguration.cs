// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the configuration of the test kassa for OmniKassa.
    /// </summary>
    public sealed class TestKassaConfiguration : KassaConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestKassaConfiguration"/> class.
        /// </summary>
        public TestKassaConfiguration()
        {
            MerchantId = "002020000000001";
            SecretKey = "002020000000001_KEY1";
            Url = new Uri("https://payment-webinit.simu.omnikassa.rabobank.nl/paymentServlet");
        }
    }
}