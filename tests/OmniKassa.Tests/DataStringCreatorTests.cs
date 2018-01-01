// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class DataStringCreatorTests
    {
        [TestMethod]
        public void Create_EmptyObjects_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest();

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_MerchantIsSet_ValidString()
        {
            var configuration = new KassaConfiguration()
            {
                MerchantId = "Merchant"
            };
            var request = new PaymentRequest();

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=Merchant|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_KeyVersionIsSet_ValidString()
        {
            var configuration = new KassaConfiguration()
            {
                KeyVersion = 15
            };
            var request = new PaymentRequest();

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=15|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_AmountIsSet_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                Amount = 42.4262m
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=4242|currencyCode=000|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToAmericanDollar_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.AmericanDollar
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=840|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToAustralianDollar_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.AustralianDollar
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=036|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToCanadianDollar_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.CanadianDollar
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=124|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToDanishCrown_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.DanishCrown
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=208|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToEuro_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.Euro
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=978|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToJapaneseYen_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.JapaneseYen
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=392|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToNorwegianCrown_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.NorwegianCrown,
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=578|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToPoundSterling_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.PoundSterling
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=826|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToSwedishCrown_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.SwedishCrown
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=752|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_CurrencyCodeIsSetToSwissFranc_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CurrencyCode = CurrencyCode.SwissFranc
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=756|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_NormalReturnUrlIsSet_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                ReturnUrl = new Uri("https://www.github.com")
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=https://www.github.com/|transactionReference=", result);
        }

        [TestMethod]
        public void Create_TransactionReferenceIsSet_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                TransactionReference = "Reference"
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=Reference", result);
        }

        [TestMethod]
        public void Create_OrderIdIsSet_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                OrderId = "Order"
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=|orderId=Order", result);
        }

        [TestMethod]
        public void Create_AutomaticResponseUrlIsSet_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                AutomaticResponseUrl = new Uri("https://www.github.com")
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=|automaticResponseUrl=https://www.github.com/", result);
        }

        [TestMethod]
        public void Create_LanguageIsSet_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                Language = LanguageCode.CS
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=|customerLanguage=CS", result);
        }

        [TestMethod]
        public void Create_PaymentBrandsIsEmpty_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                PaymentBrands = Enumerable.Empty<PaymentBrand>()
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=", result);
        }

        [TestMethod]
        public void Create_PaymentBrandsIsSetWithOneValue_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                PaymentBrands = new PaymentBrand[] { PaymentBrand.IDEAL }
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=|paymentMeanBrandList=IDEAL", result);
        }

        [TestMethod]
        public void Create_PaymentBrandsIsSetWithMultipleValues_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                PaymentBrands = new PaymentBrand[] { PaymentBrand.IDEAL, PaymentBrand.MASTERCARD, PaymentBrand.VISA }
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=|paymentMeanBrandList=IDEAL,MASTERCARD,VISA", result);
        }

        [TestMethod]
        public void Create_ExpirationDateIsSet_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                ExpirationDate = new DateTime(2010, 5, 10, 16, 10, 15)
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=|expirationDate=2010-05-10T16:10:15", result);
        }

        [TestMethod]
        public void Create_CaptureDayIsSet_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CaptureDay = 4
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=|captureDay=04", result);
        }

        [TestMethod]
        public void Create_CaptureModeIsSet_ValidString()
        {
            var configuration = new KassaConfiguration();
            var request = new PaymentRequest()
            {
                CaptureMode = "VALIDATION"
            };

            var result = DataString.Create(configuration, request);

            Assert.AreEqual("merchantId=|keyVersion=1|amount=0|currencyCode=000|normalReturnUrl=|transactionReference=|captureMode=VALIDATION", result);
        }
    }
}
