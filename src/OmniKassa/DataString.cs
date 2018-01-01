// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace OmniKassa
{
    internal class DataString
    {
        private readonly StringBuilder _builder;
        private readonly IKassaConfiguration _configuration;
        private readonly IPaymentRequest _request;

        private DataString(IKassaConfiguration configuration, IPaymentRequest request)
        {
            _builder = new StringBuilder();
            _configuration = configuration;
            _request = request;
        }

        public static string Create(IKassaConfiguration configuration, IPaymentRequest request)
        {
            Debug.Assert(configuration != null);
            Debug.Assert(request != null);

            DataString dataString = new DataString(configuration, request);
            return dataString.Create();
        }

        private string Create()
        {
            AddMerchantId();
            AddKeyVersion();
            AddAmount();
            AddCurrencyCode();
            AddNormalReturnUrl();
            AddTransactionReference();

            AddOrderId();
            AddAutomaticResponseUrl();

            AddCustomerLanguage();
            AddPaymentBrands();
            AddExpirationDate();
            AddCaptureDay();
            AddCaptureMode();

            return _builder.ToString();
        }

        private void AddAmount()
        {
            _builder.Append("|amount=");
            _builder.Append(_request.AmountAsNumber());
        }

        private void AddAutomaticResponseUrl()
        {
            if (_request.AutomaticResponseUrl == null)
                return;

            _builder.Append("|automaticResponseUrl=");
            _builder.Append(_request.AutomaticResponseUrl);
        }

        private void AddCaptureDay()
        {
            if (_request.CaptureDay == null)
                return;

            string day = ((int)_request.CaptureDay).ToString("00", CultureInfo.InvariantCulture);
            _builder.Append("|captureDay=");
            _builder.Append(day);
        }

        private void AddCaptureMode()
        {
            if (string.IsNullOrEmpty(_request.CaptureMode))
                return;

            _builder.Append("|captureMode=");
            _builder.Append(_request.CaptureMode);
        }

        private void AddCurrencyCode()
        {
            _builder.Append("|currencyCode=");

            string code = ((int)_request.CurrencyCode).ToString("000", CultureInfo.InvariantCulture);
            _builder.Append(code);
        }

        private void AddCustomerLanguage()
        {
            if (_request.Language == null)
                return;

            _builder.Append("|customerLanguage=");
            _builder.Append(_request.Language.ToString());
        }

        private void AddExpirationDate()
        {
            if (_request.ExpirationDate == null)
                return;

            _builder.Append("|expirationDate=");
            _builder.Append(_request.ExpirationDate.Value.ToString("s", CultureInfo.InvariantCulture));
        }

        private void AddKeyVersion()
        {
            _builder.Append("|keyVersion=");
            _builder.Append(_configuration.KeyVersion);
        }

        private void AddMerchantId()
        {
            _builder.Append("merchantId=");
            _builder.Append(_configuration.MerchantId);
        }

        private void AddNormalReturnUrl()
        {
            _builder.Append("|normalReturnUrl=");
            _builder.Append(_request.ReturnUrl);
        }

        private void AddOrderId()
        {
            if (string.IsNullOrEmpty(_request.OrderId))
                return;

            _builder.Append("|orderId=");
            _builder.Append(_request.OrderId);
        }

        private void AddPaymentBrands()
        {
            if (_request.PaymentBrands == null)
                return;

            IEnumerator<PaymentBrand> enumerator = _request.PaymentBrands.GetEnumerator();
            if (!enumerator.MoveNext())
                return;

            _builder.Append("|paymentMeanBrandList=");
            _builder.Append(enumerator.Current.ToString());
            while (enumerator.MoveNext())
            {
                _builder.Append(",");
                _builder.Append(enumerator.Current.ToString());
            }
        }

        private void AddTransactionReference()
        {
            _builder.Append("|transactionReference=");
            _builder.Append(_request.TransactionReference);
        }
    }
}
