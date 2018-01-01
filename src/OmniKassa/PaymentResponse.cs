// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using System.Globalization;

namespace OmniKassa
{
    internal sealed class PaymentResponse : IPaymentResponse
    {
        public decimal Amount { get; set; }

        public string AuthorisationId { get; set; }

        public int? CaptureDay { get; set; }

        public string CaptureMode { get; set; }

        public CurrencyCode CurrencyCode { get; set; }

        public int KeyVersion { get; set; }

        public string MaskedPan { get; set; }

        public string MerchantId { get; set; }

        public string OrderId { get; set; }

        public PaymentBrand PaymentBrand { get; set; }

        public ResponseCode Code { get; set; }

        public ResponseStatus Status
        {
            get
            {
                switch (Code)
                {
                    case ResponseCode.AwaitingStatusReport:
                        return ResponseStatus.AwaitingStatusReport;
                    case ResponseCode.Cancelled_17:
                    case ResponseCode.Cancelled_90:
                        return ResponseStatus.Cancelled;
                    case ResponseCode.Expired:
                        return ResponseStatus.Expired;
                    case ResponseCode.Referral:
                        return ResponseStatus.Referral;
                    case ResponseCode.Refused_03:
                    case ResponseCode.Refused_05:
                    case ResponseCode.Refused_12:
                    case ResponseCode.Refused_14:
                    case ResponseCode.Refused_25:
                    case ResponseCode.Refused_30:
                    case ResponseCode.Refused_75:
                    case ResponseCode.Refused_89:
                        return ResponseStatus.Refused;
                    case ResponseCode.Successful:
                        return ResponseStatus.Successful;
                    default:
                        return ResponseStatus.Unknown;
                }
            }
        }

        public DateTime? TransactionDateTime { get; set; }

        public string TransactionReference { get; set; }

        public static PaymentResponse Create(string data)
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

        private static decimal ToAmount(string value)
        {
            int amount = ToInt(value);
            return amount / 100M;
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
                    Code = ToResponseCode(value);
                    break;
                case "paymentMeanBrand":
                    PaymentBrand = ToEnum(value, PaymentBrand.Unknown);
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
