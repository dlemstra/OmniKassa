// <copyright file="PaymentResponse.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;
using System.Globalization;

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the response that was received from OmniKassa.
    /// </summary>
    public sealed class PaymentResponse : IPaymentResponse
    {
        /// <summary>
        /// Gets or sets the final amount of a transaction (debit or credit) or amount of an operation(refund, cancellation, etc.).
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the dentifier of the authorisation provided by the acquirer.Configured by the merchant/webshop for manual authorisation.
        /// </summary>
        public string AuthorisationId { get; set; }

        /// <summary>
        /// Gets or sets the number of days after authorisation of a credit card transaction after which automatic validation of the transaction follows.
        /// </summary>
        public int? CaptureDay { get; set; }

        /// <summary>
        /// Gets or sets the mode that can be used to indicate that the user of the Rabo OmniKassa dashboard must manually validate credit card
        /// transactions after the automatic authorisation of this transaction. (This is in contrast to the standard credit card transaction processing
        /// procedure, in which validation is automatic after authorisation.)
        /// </summary>
        public string CaptureMode { get; set; }

        /// <summary>
        /// Gets or sets the currency of the amount.
        /// </summary>
        public CurrencyCode CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets version number of the secret key. Can be found on the Rabo OmniKassa Downloadsite.
        /// </summary>
        public int KeyVersion { get; set; }

        /// <summary>
        /// Gets or sets the hidden Primary Account Number.
        /// </summary>
        public string MaskedPan { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the merchant/webshop.
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Gets or sets an open field that can be used to link the identification of the order in the webshop to the payment in the Rabo OmniKassa.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the brand name of payment method the customer has selected.
        /// </summary>
        public PaymentBrand PaymentMeanBrand { get; set; }

        /// <summary>
        /// Gets or sets the response code for a payment request.
        /// </summary>
        public ResponseCode ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the transaction time. If the payment is sent to the acquirer for authorisation: date/time in the Rabo OmniKassa server
        /// at which the payment is sent to the acquirer, in the merchant/webshop's time zone. Otherwise: date and time at which the Rabo OmniKassa
        /// response codeis generated on the Rabo OmniKassa server.
        /// </summary>
        public DateTime? TransactionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the transaction.
        /// </summary>
        public string TransactionReference { get; set; }

        internal static PaymentResponse Create(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            PaymentResponse response = null;

            foreach (string item in data.Split('|'))
            {
                int index = item.IndexOf('=');
                if (index < 1 || index + 1 == item.Length)
                    continue;

                string key = item.Substring(0, index);
                string value = item.Substring(index + 1);

                if (response == null)
                    response = new PaymentResponse();

                response.SetValue(key, value);
            }

            if (response != null)
                response.CorrectAmount();

            return response;
        }

        private static int ToAmount(string value)
        {
            int amount = ToInt(value);
            return amount / 100;
        }

        private static CurrencyCode ToCurrencyCode(string value)
        {
            int currencyCode = ToInt(value);
            return EnumHelper.Parse(currencyCode, CurrencyCode.Unknown);
        }

        private static DateTime ToDateTime(string value)
        {
            return DateTime.Parse(value, null, DateTimeStyles.RoundtripKind);
        }

        private static TEnum ToEnum<TEnum>(string value, TEnum defaultValue)
          where TEnum : struct, IConvertible
        {
            return EnumHelper.Parse(value, defaultValue);
        }

        private static int ToInt(string value)
        {
            return int.Parse(value);
        }

        private static ResponseCode ToResponseCode(string value)
        {
            switch (value)
            {
                case "00":
                    return ResponseCode.Successful;
                case "02":
                    return ResponseCode.Referral;
                case "03":
                    return ResponseCode.Refused_03;
                case "05":
                    return ResponseCode.Refused_05;
                case "12":
                    return ResponseCode.Refused_12;
                case "14":
                    return ResponseCode.Refused_14;
                case "17":
                    return ResponseCode.Cancelled_17;
                case "25":
                    return ResponseCode.Refused_25;
                case "30":
                    return ResponseCode.Refused_30;
                case "60":
                    return ResponseCode.AwaitingStatusReport;
                case "75":
                    return ResponseCode.Refused_75;
                case "89":
                    return ResponseCode.Refused_89;
                case "90":
                    return ResponseCode.Cancelled_90;
                case "97":
                    return ResponseCode.Expired;
                default:
                    return ResponseCode.Unknown;
            }
        }

        private void CorrectAmount()
        {
            if (CurrencyCode == CurrencyCode.JapaneseYen)
                Amount = Amount * 100;
        }

        private void SetValue(string key, string value)
        {
            switch (key)
            {
                case "amount":
                    Amount = ToAmount(value);
                    break;
                case "authorisationId":
                    AuthorisationId = value;
                    break;
                case "captureDay":
                    CaptureDay = ToInt(value);
                    break;
                case "captureMode":
                    CaptureMode = value;
                    break;
                case "currencyCode":
                    CurrencyCode = ToCurrencyCode(value);
                    break;
                case "merchantId":
                    MerchantId = value;
                    break;
                case "keyVersion":
                    KeyVersion = ToInt(value);
                    break;
                case "maskedPan":
                    MaskedPan = value;
                    break;
                case "orderId":
                    OrderId = value;
                    break;
                case "responseCode":
                    ResponseCode = ToResponseCode(value);
                    break;
                case "paymentMeanBrand":
                    PaymentMeanBrand = ToEnum(value, PaymentBrand.Unknown);
                    break;
                case "transactionDateTime":
                    TransactionDateTime = ToDateTime(value);
                    break;
                case "transactionReference":
                    TransactionReference = value;
                    break;
            }
        }
    }
}
