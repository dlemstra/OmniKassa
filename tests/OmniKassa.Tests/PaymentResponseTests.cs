// <copyright file="PaymentResponseTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class PaymentResponseTests
    {
        [TestMethod]
        public void Create_DataIsNull_ReturnsNull()
        {
            var result = PaymentResponse.Create(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_DataIsEmpty_ReturnsNull()
        {
            var result = PaymentResponse.Create(string.Empty);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_DataIsFoo_ReturnsNull()
        {
            var result = PaymentResponse.Create("Foo");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_DataKeyIsEmpty_ReturnsNull()
        {
            var result = PaymentResponse.Create("=test");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_DataValueIsEmpty_ReturnsNull()
        {
            var result = PaymentResponse.Create("test=");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_DataWithUnknownKey_ReturnsEmptyResponse()
        {
            var result = PaymentResponse.Create("foo=bar");

            Assert.IsNotNull(result);
            Assert.AreEqual(0m, result.Amount);
            Assert.AreEqual(null, result.AuthorisationId);
            Assert.AreEqual(null, result.CaptureDay);
            Assert.AreEqual(null, result.CaptureMode);
            Assert.AreEqual(CurrencyCode.Unknown, result.CurrencyCode);
            Assert.AreEqual(0, result.KeyVersion);
            Assert.AreEqual(null, result.MaskedPan);
            Assert.AreEqual(null, result.MerchantId);
            Assert.AreEqual(null, result.OrderId);
            Assert.AreEqual(PaymentType.Unknown, result.PaymentMeanBrand);
            Assert.AreEqual((ResponseCode)0, result.ResponseCode);
            Assert.AreEqual(null, result.TransactionDateTime);
            Assert.AreEqual(null, result.TransactionReference);
        }

        [TestMethod]
        public void Create_DataWithMultipleValUes_PropertiesAreSet()
        {
            var result = PaymentResponse.Create("amount=500|keyVersion=6|maskedPan=mask3d");

            Assert.IsNotNull(result);
            Assert.AreEqual(5.0m, result.Amount);
            Assert.AreEqual(6, result.KeyVersion);
            Assert.AreEqual("mask3d", result.MaskedPan);
        }

        [TestMethod]
        public void Create_DataWithAmount_AmountIsSet()
        {
            var result = PaymentResponse.Create("amount=500");

            Assert.IsNotNull(result);
            Assert.AreEqual(5.0m, result.Amount);
        }

        [TestMethod]
        public void Create_DataWithAmountInYen_AmountIsNotDevided()
        {
            var result = PaymentResponse.Create("currencyCode=392|amount=500");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.JapaneseYen, result.CurrencyCode);
            Assert.AreEqual(500m, result.Amount);
        }

        [TestMethod]
        public void Create_DataWithAuthorisationId_AuthorisationIdIsSet()
        {
            var result = PaymentResponse.Create("authorisationId=Authorisation");

            Assert.IsNotNull(result);
            Assert.AreEqual("Authorisation", result.AuthorisationId);
        }

        [TestMethod]
        public void Create_DataWithCaptureDay_CaptureDayIsSet()
        {
            var result = PaymentResponse.Create("captureDay=42");

            Assert.IsNotNull(result);
            Assert.AreEqual(42, result.CaptureDay);
        }

        [TestMethod]
        public void Create_DataWithCaptureMode_CaptureModeIsSet()
        {
            var result = PaymentResponse.Create("captureMode=Mode");

            Assert.IsNotNull(result);
            Assert.AreEqual("Mode", result.CaptureMode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToAmericanDollar_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=840");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.AmericanDollar, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToAustralianDollar_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=036");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.AustralianDollar, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToCanadianDollar_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=124");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.CanadianDollar, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToDanishCrown_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=208");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.DanishCrown, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToEuro_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=978");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.Euro, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToJapaneseYen_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=392");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.JapaneseYen, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToNorwegianCrown_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=578");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.NorwegianCrown, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToPoundSterling_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=826");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.PoundSterling, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToSwedishCrown_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=752");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.SwedishCrown, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToSwissFranc_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=756");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.SwissFranc, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithCurrencyCodeSetToUnknownValue_CurrencyCodeIsSet()
        {
            var result = PaymentResponse.Create("currencyCode=42");

            Assert.IsNotNull(result);
            Assert.AreEqual(CurrencyCode.Unknown, result.CurrencyCode);
        }

        [TestMethod]
        public void Create_DataWithKeyVersion_KeyVersionIsSet()
        {
            var result = PaymentResponse.Create("keyVersion=2");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.KeyVersion);
        }

        [TestMethod]
        public void Create_DataWithMaskedPan_MaskedPanIsSet()
        {
            var result = PaymentResponse.Create("maskedPan=m*sk*d");

            Assert.IsNotNull(result);
            Assert.AreEqual("m*sk*d", result.MaskedPan);
        }

        [TestMethod]
        public void Create_DataWithMerchantId_MerchantIdIsSet()
        {
            var result = PaymentResponse.Create("merchantId=Merchant");

            Assert.IsNotNull(result);
            Assert.AreEqual("Merchant", result.MerchantId);
        }

        [TestMethod]
        public void Create_DataWithOrderId_OrderIdIsSet()
        {
            var result = PaymentResponse.Create("orderId=Order");

            Assert.IsNotNull(result);
            Assert.AreEqual("Order", result.OrderId);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandSetToACCEPTGIRO_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=ACCEPTGIRO");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.ACCEPTGIRO, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandSetToBCMC_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=BCMC");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.BCMC, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandSetToIDEAL_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=IDEAL");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.IDEAL, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandSetToINCASSO_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=INCASSO");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.INCASSO, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandSetToMAESTRO_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=MAESTRO");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.MAESTRO, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandSetToMASTERCARD_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=MASTERCARD");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.MASTERCARD, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandSetToREMBOURS_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=REMBOURS");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.REMBOURS, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandToSetVISA_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=VISA");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.VISA, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandSetToVPAY_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=VPAY");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.VPAY, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithPaymentMeanBrandSetToUnknownValue_PaymentMeanBrandIsSet()
        {
            var result = PaymentResponse.Create("paymentMeanBrand=FOO");

            Assert.IsNotNull(result);
            Assert.AreEqual(PaymentType.Unknown, result.PaymentMeanBrand);
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToAwaitingStatusReport_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=60");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.AwaitingStatusReport, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsFalse(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToCancelled_17_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=17");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Cancelled_17, result.ResponseCode);
            Assert.IsTrue(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsFalse(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToCancelled_90_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=90");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Cancelled_90, result.ResponseCode);
            Assert.IsTrue(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsFalse(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToExpired_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=97");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Expired, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsFalse(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToReferral_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=02");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Referral, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsFalse(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToRefused_03_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=03");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Refused_03, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsTrue(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToRefused_05_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=05");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Refused_05, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsTrue(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToRefused_12_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=12");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Refused_12, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsTrue(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToRefused_14_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=14");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Refused_14, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsTrue(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToRefused_25_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=25");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Refused_25, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsTrue(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToRefused_30_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=30");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Refused_30, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsTrue(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToRefused_75_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=75");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Refused_75, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsTrue(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToRefused_89_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=89");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Refused_89, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsTrue(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToSuccessful_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=00");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Successful, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsFalse(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithResponseCodeSetToUnknownValue_ResponseCodeIsSet()
        {
            var result = PaymentResponse.Create("responseCode=FOO");

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseCode.Unknown, result.ResponseCode);
            Assert.IsFalse(ResponseCode.Cancelled.HasFlag(result.ResponseCode));
            Assert.IsFalse(ResponseCode.Refused.HasFlag(result.ResponseCode));
        }

        [TestMethod]
        public void Create_DataWithTransactionDateTime_TransactionDateTimeIsSet()
        {
            var result = PaymentResponse.Create("transactionDateTime=2010-05-10T14:12:45Z");

            Assert.IsNotNull(result);
            Assert.AreEqual(new DateTime(2010, 05, 10, 14, 12, 45), result.TransactionDateTime);
            Assert.AreEqual(DateTimeKind.Utc, result.TransactionDateTime.Value.Kind);
        }

        [TestMethod]
        public void Create_DataWithTransactionReference_TransactionReferenceIsSet()
        {
            var result = PaymentResponse.Create("transactionReference=Transaction");

            Assert.IsNotNull(result);
            Assert.AreEqual("Transaction", result.TransactionReference);
        }
    }
}
